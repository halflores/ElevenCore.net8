using Eleven.Service.Modelo.SAP;
using Eleven.Service.Repositorio.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Eleven.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ILogger<TipoDocumentoController> _logger;
        private readonly ITipoDocumentoService _tipoDocumentoService;
        public TipoDocumentoController(ILogger<TipoDocumentoController> logger, ITipoDocumentoService tipoDocumentoService)
        {
            _logger = logger;
            _tipoDocumentoService = tipoDocumentoService;
        }

        [HttpGet()]
        [Route("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<MdoTipoDocumento> _documentos = await _tipoDocumentoService.GetAllAsync();
                //List<MdoTipoDocumento> _documentos =  _tipoDocumentoService.GetAll(x => x.Id == 4).ToList();
                return Ok(_documentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }

        }

        // GET: api/<TipoDocumentoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TipoDocumentoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TipoDocumentoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TipoDocumentoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TipoDocumentoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
