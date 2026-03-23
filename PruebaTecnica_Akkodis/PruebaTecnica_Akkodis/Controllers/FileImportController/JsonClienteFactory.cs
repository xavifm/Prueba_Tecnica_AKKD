using Newtonsoft.Json;
using PruebaTecnica_Akkodis.Controllers.FileImportController.DTO;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PruebaTecnica_Akkodis.Controllers.FileImportController
{
    public class JsonClienteFactory : IClienteFactory
    {
        public void Add(string filePath, Cliente cliente)
        {
            var clientes = Import(filePath);

            if (clientes.Any(c => c.DNI == cliente.DNI))
                return;

            clientes.Add(cliente);
            Save(filePath, clientes);
        }

        public void Delete(string filePath, string dni)
        {
            var clientes = Import(filePath);

            var cliente = clientes.FirstOrDefault(c => c.DNI == dni);
            if (cliente == null)
                return;

            clientes.Remove(cliente);
            Save(filePath, clientes);
        }

        public List<Cliente> Import(string filePath)
        {
            string json = File.ReadAllText(filePath);
            List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(json);

            return clientes ?? new List<Cliente>();
        }

        private void Save(string filePath, List<Cliente> clientes)
        {
            string json = JsonConvert.SerializeObject(clientes, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
