using Newtonsoft.Json;
using PruebaTecnica_Akkodis_2.CrossCutting.DTO;

namespace PruebaTecnica_Akkodis_2.Managers.FileImportManager
{
    public class JsonClienteFactory : IClienteFactory
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task Add(string filePath, Cliente cliente)
        {
            await _semaphore.WaitAsync();
            try
            {
                var clientes = await Import(filePath);

                if (clientes.Any(c => c.DNI == cliente.DNI))
                    return;

                clientes.Add(cliente);
                await Save(filePath, clientes);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task Delete(string filePath, string dni)
        {
            await _semaphore.WaitAsync();
            try
            {
                var clientes = await Import(filePath);

                var cliente = clientes.FirstOrDefault(c => c.DNI == dni);
                if (cliente == null)
                    return;

                clientes.Remove(cliente);
                await Save(filePath, clientes);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<List<Cliente>> Import(string filePath)
        {
            if (!File.Exists(filePath))
            {
                await File.WriteAllTextAsync(filePath, "[]");
            }

            string json = await File.ReadAllTextAsync(filePath);

            List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(json);

            return clientes ?? new List<Cliente>();
        }

        private async Task Save(string filePath, List<Cliente> clientes)
        {
            string json = JsonConvert.SerializeObject(clientes, Formatting.Indented);
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}