using Eleven.Service.Modelo.SAP;
using Eleven.Service.Repositorio.Interfaz;
using Microsoft.Extensions.Logging;
using SAPBo.Data.Entidad;
using SAPBo.Data.Repositorio.Interfaz;

namespace Eleven.Service.Repositorio.Implementacion
{
    public class ItemService : IItemService
    {
        private readonly IBoItemRepository _itemRepositoryBo;
        private readonly IBoDocumentRepository _documentRepositoryBo;
        private readonly ILogger<ItemService> _logger;
        public ItemService(IBoItemRepository itemRepositoryBo, IBoDocumentRepository documentRepositoryBo, ILogger<ItemService> logger)
        {
            _itemRepositoryBo = itemRepositoryBo;
            _documentRepositoryBo = documentRepositoryBo;
            _logger = logger;
        }
        public async Task<MdoItem> GetItemByIdAsync(string itemCode, MdoSLConexion boLogin)
        {
            try
            {
                Login login = new Login() { 
                    SLConexion = boLogin.SLConexion,
                    UserName = boLogin.UserName
                };

                Item item = await _itemRepositoryBo.GetItemByIdAsync(itemCode, login);

                MdoItem model = new MdoItem()
                {
                    BarCode = item.BarCode,
                    ItemCode = item.ItemCode,
                    ItemName = item.ItemName,
                    U_Conjunto = item.U_Conjunto,
                };

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetItemByIdAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }
        public async Task<List<MdoItem>> GetAllItemsAsync(MdoSLConexion boLogin)
        {
            try
            {
                Login login = new Login()
                {
                    SLConexion = boLogin.SLConexion,
                    UserName = boLogin.UserName
                };

                List<Item> items = await _itemRepositoryBo.GetAllItemsAsync(login);
                List<MdoItem> itemsModel = new List<MdoItem>();

                foreach (var item in items)
                {
                    MdoItem model = new MdoItem()
                    {
                        BarCode = item.BarCode,
                        ItemCode = item.ItemCode,
                        ItemName = item.ItemName,
                        U_Conjunto = item.U_Conjunto,
                    };
                    itemsModel.Add(model);
                }

                return itemsModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllItemsAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }

        }

    }
}
