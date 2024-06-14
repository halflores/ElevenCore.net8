using Eleven.Service.Modelo.SAP;
using Eleven.Service.Repositorio.Interfaz;
using SAPBo.Data.Entidad;
using SAPBo.Data.Repositorio.Interfaz;

namespace Eleven.Service.Repositorio.Implementacion
{
    public class CategoryService : ICategoryService
    {
        private readonly IBoItemRepository _itemRepositoryBo;
        private readonly IBoCategoriaRepository _categorieRepositoryBo;

        public CategoryService(IBoItemRepository itemRepositoryBo, IBoCategoriaRepository categorieRepositoryBo)
        {
            _itemRepositoryBo = itemRepositoryBo;
            _categorieRepositoryBo = categorieRepositoryBo;
        }

        public async Task<List<MdoCategory>> GetAllCategoriesAsync()
        {
            try
            {
                List<MdoCategory> itemsModel = new List<MdoCategory>();

                foreach (var item in await _categorieRepositoryBo.GetAllCategoriesAsync())
                {
                    MdoCategory model = new MdoCategory()
                    {
                        ItmsGrpNam = item.ItmsGrpNam,
                        ItmsGrpCod = item.ItmsGrpCod,
                    };
                    itemsModel.Add(model);
                }

                return itemsModel;
            }
            catch (Exception ex)
            {
                //LogException.Write(ex);
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }
    }
}
