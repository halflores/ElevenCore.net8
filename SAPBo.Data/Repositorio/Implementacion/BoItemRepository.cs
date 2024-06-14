using B1SLayer;
using Microsoft.Extensions.Logging;
using SAPBo.Data.Entidad;
using SAPBo.Data.Repositorio.Interfaz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Repositorio.Implementacion
{
    public class BoItemRepository : IBoItemRepository
    {
        private readonly Dictionary<string, SLConnection> _slConnections;
        private readonly ILogger<BoItemRepository> _logger;
        public BoItemRepository(Dictionary<string, SLConnection> slConnections, ILogger<BoItemRepository> logger) {

            _slConnections = slConnections;
            _logger = logger;
        }

        public async Task<Item> GetItemByIdAsync(string itemCode, Login login)
        {
            List<ItemGroups> categorias = new List<ItemGroups>();
            try
            {
                Item? item = await _slConnections[login.SLConexion] // SLConnection object
                                   .Request("Items", itemCode) // Creation
                                   .Select("ItemCode, ItemName, ForeignName") // Configuration
                                   .GetAsync<Item>();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetItemByIdAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }        
        }

        public async Task<List<Item>> GetAllItemsAsync(Login login) 
        {
            try
            {
                List<Item> items = await _slConnections[login.SLConexion] // SLConnection object
                                            .Request("Items") // Creation
                                            .Select("ItemCode, ItemName, ForeignName") // Configuration
                                            .OrderBy("ItemName")
                                            .WithPageSize(150)
                                            .GetAsync<List<Item>>();
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetItemByIdAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

    }
}
