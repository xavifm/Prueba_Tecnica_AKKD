using PruebaTecnica_Akkodis_2.CrossCutting.DTO;

namespace PruebaTecnica_Akkodis_2.Managers.FileImportManager
{
    public interface IClienteFactory
    {
        Task<List<Cliente>> Import(string filePath);
        Task Add(string filePath, Cliente cliente);
        Task Delete(string filePath, string dni);
    }
}
