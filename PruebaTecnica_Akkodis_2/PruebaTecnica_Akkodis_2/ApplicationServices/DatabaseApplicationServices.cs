using PruebaTecnica_Akkodis_2.CrossCutting.DTO;
using PruebaTecnica_Akkodis_2.CrossCutting.Validator;
using PruebaTecnica_Akkodis_2.Managers.FileImportManager;

namespace PruebaTecnica_Akkodis_2.ApplicationServices
{
    /// <summary>
    /// Servicio de aplicación encargado de gestionar la lógica de negocio
    /// relacionada con los clientes. Actúa como intermediario entre la capa
    /// de presentación (API) y la capa de acceso a datos basada en fichero.
    /// </summary>
    public class DatabaseApplicationServices
    {
        private readonly string _filePath;

        /// <summary>
        /// Inicializa el servicio con la ruta del fichero de persistencia.
        /// </summary>
        /// <param name="filePath">Ruta del fichero JSON donde se almacenan los clientes.</param>
        public DatabaseApplicationServices(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Obtiene la factoría de clientes correspondiente.
        /// En este caso, siempre se utiliza la implementación basada en JSON.
        /// </summary>
        /// <returns>
        /// Instancia de <see cref="IClienteFactory"/> para trabajar con el almacenamiento.
        /// </returns>
        public IClienteFactory LoadFactory()
        {
            IClienteFactory importer = new JsonClienteFactory();
            return importer;
        }

        /// <summary>
        /// Recupera todos los clientes almacenados en el fichero.
        /// </summary>
        /// <returns>
        /// Lista de clientes.
        /// </returns>
        public async Task<List<Cliente>> GetAllClientes()
        {
            IClienteFactory importer = LoadFactory();

            if (importer == null)
                return new List<Cliente>();

            return await importer.Import(_filePath);
        }


        /// <summary>
        /// Obtiene un cliente concreto a partir de su DNI.
        /// </summary>
        /// <param name="dni">DNI del cliente a buscar.</param>
        /// <returns>
        /// Cliente encontrado o null si no existe.
        /// </returns>
        public async Task<Cliente?> GetClienteByDni(string dni)
        {
            IClienteFactory importer = LoadFactory();

            if (importer == null || string.IsNullOrWhiteSpace(dni))
                return null;

            var clientes = await importer.Import(_filePath);
            return clientes.FirstOrDefault(c => c.DNI == dni);
        }

        /// <summary>
        /// Añade un nuevo cliente tras validar sus datos y comprobar que no existe duplicidad.
        /// </summary>
        /// <param name="cliente">Cliente a añadir.</param>
        /// <returns>
        /// Tupla que indica si la operación ha sido correcta y, en caso de error,
        /// el mensaje descriptivo correspondiente.
        /// </returns>
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

        /// <summary>
        /// Elimina un cliente existente a partir de su DNI.
        /// </summary>
        /// <param name="dni">DNI del cliente a eliminar.</param>
        /// <returns>
        /// True si el cliente se elimina correctamente; false si no existe o hay error.
        /// </returns>
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