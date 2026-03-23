using PruebaTecnica_Akkodis_2.CrossCutting.DTO;
using PruebaTecnica_Akkodis_2.CrossCutting.Validator;
using PruebaTecnica_Akkodis_2.Managers.FileImportManager;

namespace PruebaTecnica_Akkodis_2.ApplicationServices
{
    public class DatabaseApplicationServices
    {
        private readonly string _filePath;

        public DatabaseApplicationServices(string filePath)
        {
            _filePath = filePath;
        }

        public IClienteFactory LoadFactory()
        {
            IClienteFactory importer = new JsonClienteFactory();
            return importer;
        }

        public async Task<List<Cliente>> GetAllClientes()
        {
            IClienteFactory importer = LoadFactory();

            if (importer == null)
                return new List<Cliente>();

            return await importer.Import(_filePath);
        }

        public async Task<Cliente?> GetClienteByDni(string dni)
        {
            IClienteFactory importer = LoadFactory();

            if (importer == null || string.IsNullOrWhiteSpace(dni))
                return null;

            var clientes = await importer.Import(_filePath);
            return clientes.FirstOrDefault(c => c.DNI == dni);
        }

        public async Task<(bool Ok, string Error)> AddCliente(Cliente cliente)
        {
            string error = string.Empty;

            IClienteFactory importer = LoadFactory();

            if (importer == null)
            {
                error = "No se pudo cargar el importador JSON";
                return (false, error);
            }

            if (cliente == null)
            {
                error = "El cliente es nulo";
                return (false, error);
            }

            if (!ClienteValidator.Validate(cliente, out error))
                return (false, error);

            var clientes = await importer.Import(_filePath);

            if (clientes.Any(c => c.DNI == cliente.DNI))
            {
                error = "Ya existe un cliente con ese DNI";
                return (false, error);
            }

            await importer.Add(_filePath, cliente);
            return (true, string.Empty);
        }

        public async Task<bool> DeleteCliente(string dni)
        {
            IClienteFactory importer = LoadFactory();

            if (importer == null || string.IsNullOrWhiteSpace(dni))
                return false;

            var clientes = await importer.Import(_filePath);
            var cliente = clientes.FirstOrDefault(c => c.DNI == dni);

            if (cliente == null)
                return false;

            await importer.Delete(_filePath, dni);
            return true;
        }
    }
}