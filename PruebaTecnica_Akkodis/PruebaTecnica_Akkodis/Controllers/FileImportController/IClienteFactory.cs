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
        List<Cliente> Import(string filePath);
        void Add(string filePath, Cliente cliente);
        void Delete(string filePath, string dni);
    }
}
