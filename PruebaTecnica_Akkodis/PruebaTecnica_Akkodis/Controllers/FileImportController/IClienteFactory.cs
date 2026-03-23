using PruebaTecnica_Akkodis.Controllers.FileImportController.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_Akkodis.Controllers.FileImportController
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
        List<Cliente> Import(string filePath);

        /// <summary>
        /// Añade un nuevo cliente al fichero de almacenamiento.
        /// </summary>
        /// <param name="filePath">Ruta del fichero donde se guardará el cliente.</param>
        /// <param name="cliente">Cliente a añadir.</param>
        /// <returns>No devuelve ningún valor.</returns>
        void Add(string filePath, Cliente cliente);

        /// <summary>
        /// Elimina un cliente del fichero de almacenamiento utilizando su DNI como identificador.
        /// </summary>
        /// <param name="filePath">Ruta del fichero donde se realizará la eliminación.</param>
        /// <param name="dni">DNI del cliente que se desea eliminar.</param>
        /// <returns>No devuelve ningún valor.</returns>
        void Delete(string filePath, string dni);
    }
}
