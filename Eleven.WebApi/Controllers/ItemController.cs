using Eleven.Service.Modelo.Eleven;
using Eleven.Service.Modelo.SAP;
using Eleven.Service.Repositorio.Implementacion;
using Eleven.Service.Repositorio.Interfaz;
using Eleven.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SAPBo.Data.Entidad;
using Utils.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Eleven.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        IItemService _itemService;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemService itemService, ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        // GET: api/<ItemController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<ItemController>/5
        [HttpGet]
        [Route("GetAsync('{ItemCode}')")]
        public async Task<IActionResult> GetAsync(string itemCode)
        {
            try
            {
                if (HttpContext.Session.IsAvailable && HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee") != null)
                {
                    MdoEmpleado? empleado = HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee");
                    MdoSLConexion boLogin = new MdoSLConexion()
                    {
                        SLConexion = empleado!.Plantilla.Sucursal.SucursalUserName,
                        UserName = empleado.Nombre
                    };
                    MdoItem item = await _itemService.GetItemByIdAsync(itemCode, boLogin); // SLConnection object
                    return Ok(item);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        // POST api/<ItemController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }

        [HttpGet()]
        [Route("GetAllItemsAsync")]
        public async Task<IActionResult> GetAllItemsAsync()
        {
            try
            {
                if (HttpContext.Session.IsAvailable && HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee") != null)
                {
                    MdoEmpleado? empleado = HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee");
                    MdoSLConexion boLogin = new MdoSLConexion()
                    {
                        SLConexion = empleado!.Plantilla.Sucursal.SucursalUserName,
                        UserName = empleado.Nombre
                    };
                    List<MdoItem> items = await _itemService.GetAllItemsAsync(boLogin); // SLConnection object
                    return Ok(items);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllItemsAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }


        }
    }
}
