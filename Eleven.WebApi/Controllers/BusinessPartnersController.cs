using Eleven.Service.Modelo.SAP;
using Eleven.Service.Repositorio.Implementacion;
using Eleven.Service.Repositorio.Interfaz;
using Eleven.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Eleven.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessPartnersController : ControllerBase
    {
        private readonly ILogger<BusinessPartnersController> _logger;
        private readonly IBusinessPartnersService _businessPartnersService;
        public BusinessPartnersController(IBusinessPartnersService businessPartnersService, ILogger<BusinessPartnersController> logger) 
        {
            _businessPartnersService = businessPartnersService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetBusinessPartnerByCardCodeAsync('{cardCode}')")]
        public async Task<IActionResult> GetBusinessPartnerByCardCodeAsync(string cardCode)
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
                    MdoBusinessPartners businessPartner = await _businessPartnersService.GetBusinessPartnerByCardCodeAsync(cardCode, boLogin); // SLConnection object
                    return Ok(businessPartner);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBusinessPartnerByCardCodeAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }

        }

        [HttpGet("GetBusinessPartnerByNroDocumentoAsync")]
        public async Task<IActionResult> GetBusinessPartnerByNroDocumentoAsync(string nroDocumento, string tpoDocumento)
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
                    MdoBusinessPartners businessPartner = await _businessPartnersService.GetBusinessPartnerByNroDocumentoAsync(nroDocumento, tpoDocumento, boLogin); // SLConnection object
                    return Ok(businessPartner);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBusinessPartnerByNroDocumentoAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }

        }

        [HttpPost]
        [Route("CreateBusinessPartnerAsync")]
        public async Task<IActionResult> CreateBusinessPartnerAsync([FromBody] MdoBusinessPartners businessPartner)
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
                    businessPartner.Empleado = empleado;
                    businessPartner = await _businessPartnersService.CreateBusinessPartnerAsync(businessPartner, boLogin); // SLConnection object
                    return Ok(businessPartner);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateBusinessPartnerAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }

        }

        [HttpPost]
        [Route("CreateBatchBusinessPartnerAsync")]
        public async Task<IActionResult> CreateBatchBusinessPartnerAsync([FromBody] List<MdoBusinessPartners> businessPartners)
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

                    List<MdoBatchResponse> response = await _businessPartnersService.CreateBatchBusinessPartnerAsync(businessPartners, empleado, boLogin); // SLConnection object
                    return Ok(response);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateBusinessPartnerAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }

        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET api/<BusinessPartnersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BusinessPartnersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BusinessPartnersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BusinessPartnersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
