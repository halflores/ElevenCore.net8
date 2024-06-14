using Eleven.Service.Modelo.SAP;

namespace Eleven.Service.Repositorio.Interfaz
{
    public interface ICategoryService
    {
        Task<List<MdoCategory>> GetAllCategoriesAsync();
    }
}
