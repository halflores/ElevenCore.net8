using Eleven.Service.Modelo.SAP;

namespace Eleven.Service.Repositorio.Interfaz
{
    public interface IItemService
    {
        Task<MdoItem> GetItemByIdAsync(string itemCode, MdoSLConexion boLogin);
        Task<List<MdoItem>> GetAllItemsAsync(MdoSLConexion boLogin);
    }
}
