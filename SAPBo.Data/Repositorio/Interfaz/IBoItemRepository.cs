using SAPBo.Data.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Repositorio.Interfaz
{
    public interface IBoItemRepository
    {
        Task<Item> GetItemByIdAsync(string itemCode, Login login);
        Task<List<Item>> GetAllItemsAsync(Login login);

    }
}
