using PruebaTecnica_Akkodis_2.CrossCutting.DTO;

namespace PruebaTecnica_Akkodis_2.Managers.FileImportManager
{
    public interface IClienteFactory
    {
        /// <summary>
        /// Importa y devuelve la lista de clientes desde el fichero especificado.
        /// </summary>
        /// <param name="filePath">Ruta del fichero desde el que se leerán los datos.</param>
        /// <returns>
        /// Lista de objetos <see cref="Cliente"/> obtenidos del fichero.
        /// </returns>
        Task<List<Cliente>> Import(string filePath);

        /// <summary>
        /// Añade un nuevo cliente al fichero de almacenamiento.
        /// </summary>
        /// <param name="filePath">Ruta del fichero donde se guardará el cliente.</param>
        /// <param name="cliente">Cliente a añadir.</param>
        /// <returns>No devuelve ningún valor.</returns>
        Task Add(string filePath, Cliente cliente);

        /// <summary>
        /// Elimina un cliente del fichero de almacenamiento utilizando su DNI como identificador.
        /// </summary>
        /// <param name="filePath">Ruta del fichero donde se realizará la eliminación.</param>
        /// <param name="dni">DNI del cliente que se desea eliminar.</param>
        /// <returns>No devuelve ningún valor.</returns>
        Task Delete(string filePath, string dni);
    }
}
