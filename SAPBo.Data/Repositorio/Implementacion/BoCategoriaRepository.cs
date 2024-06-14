using SAPBo.Data.Entidad;
using SAPBo.Data.Repositorio.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sap.Data.Hana;
using System.Data.SqlClient;
using SAPBo.B1HanaQuery;

namespace SAPBo.Data.Repositorio.Implementacion
{
    public class BoCategoriaRepository :IBoCategoriaRepository
    {

        HanaRequest.RowMapper<ItemGroups> categoriaMapper = (delegate (HanaDataReader reader)
        {
            ItemGroups oitb = new ItemGroups();

            oitb.ItmsGrpCod = Convert.ToInt32(reader[0]);
            oitb.ItmsGrpNam = Convert.ToString(reader[1])!;
            return oitb;
        });

        public async Task<List<ItemGroups>> GetAllCategoriesAsync()
        {
            List<ItemGroups> categorias = new List<ItemGroups>();
            string catalog = AppDomain.CurrentDomain.GetData("SBOCatalog")!.ToString()!;

            try
            {
                List<HanaParameter> parametros = new List<HanaParameter>();
                string sql = string.Format("\"{0}\".\"BL_GETCATEGORY\"", catalog);

                categorias = await HanaRequest.ExecuteToListAsync<ItemGroups>(sql, categoriaMapper, parametros);
                return categorias;
            }
            catch (Exception ex)
            {
                //LogException.Write(ex);
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }

        }

    }
}
