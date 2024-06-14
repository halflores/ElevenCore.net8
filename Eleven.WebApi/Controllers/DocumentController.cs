using Eleven.Service.Repositorio.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Eleven.Service.Modelo.SAP;
using Microsoft.AspNetCore.Authorization;
using Eleven.WebApi.Helper;
using Eleven.Service.Repositorio.Implementacion;
using SAPBo.Data.Entidad;

namespace Eleven.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        IDocumentService _documentService;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IDocumentService documentService, ILogger<DocumentController> logger)
        {
            _documentService = documentService;
            _logger = logger;
        }
        // GET: api/<DocumentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DocumentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DocumentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DocumentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DocumentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //[HttpPost]
        //[Route("CreateOfertaOrderAsync")]
        //public async Task<IActionResult> CreateOfertaOrderAsync([FromBody] MdoDocument document)
        //{
        //    if (HttpContext.Session.IsAvailable && HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee") != null)
        //    {
        //        MdoEmpleado? empleado = HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee");

        //        MdoSLConexion boLogin = new MdoSLConexion()
        //        {
        //            SLConexion = empleado!.Plantilla.Sucursal.SucursalUserName,
        //            UserName = empleado.Nombre
        //        };
        //        MdoDocument? boDocument = document;

        //        List<MdoBatchResponse> results = await _documentService.CreateOfertaOrderAsync(boDocument, boLogin);
        //        return Ok(results);
        //    }
        //    return BadRequest();
        //}

        //[HttpPost]
        //[Route("CreateOfertaOrdenFacturaAsync")]
        //public async Task<IActionResult> CreateOfertaOrdenFacturaAsync([FromBody] MdoDocument document)
        //{
        //    if (HttpContext.Session.IsAvailable && HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee") != null)
        //    {
        //        MdoEmpleado? empleado = HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee");

        //        MdoSLConexion boLogin = new MdoSLConexion()
        //        {
        //            SLConexion = empleado!.Plantilla.Sucursal.SucursalUserName,
        //            UserName = empleado.Nombre
        //        };

        //        List<MdoBatchResponse> results = await _documentService.CreateOfertaOrderFacturaAsync(document, boLogin);
        //        return Ok(results);
        //    }
        //    return BadRequest();
        //}

        [HttpPost]
        [Route("CreateOfertaOrdenFacturaEntregaAsync")]
        public async Task<IActionResult> CreateOfertaOrdenFacturaEntregaAsync([FromBody] MdoDocument document)
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

                    List<MdoBatchResponse> results = await _documentService.CreateOfertaOrderFacturaEntregaAsync(document, boLogin);
                    return Ok(results);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateOfertaOrdenFacturaEntregaAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }

        }

        //[HttpPost]
        //[Route("CreateOfertaOrderFacturaPagoEntregaAsync")]
        //public async Task<IActionResult> CreateOfertaOrderFacturaPagoEntregaAsync([FromBody] MdoDocument document)
        //{
        //    if (HttpContext.Session.IsAvailable && HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee") != null)
        //    {
        //        MdoEmpleado? empleado = HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee");

        //        MdoSLConexion boLogin = new MdoSLConexion()
        //        {
        //            SLConexion = empleado!.Plantilla.Sucursal.SucursalUserName,
        //            UserName = empleado.Nombre
        //        };

        //        List<MdoBatchResponse> results = await _documentService.CreateOfertaOrderFacturaPagoEntregaAsync(document, boLogin);
        //        return Ok(results);
        //    }
        //    return BadRequest();
        //}

        //[HttpPost]
        //[Route("CrearFacturaAsync")]
        //public async Task<IActionResult> CrearFacturaAsync([FromBody] MdoDocument document)
        //{
        //    if (HttpContext.Session.IsAvailable && HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee") != null)
        //    {
        //        MdoEmpleado? empleado = HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee");

        //        MdoSLConexion boLogin = new MdoSLConexion()
        //        {
        //            SLConexion = empleado!.Plantilla.Sucursal.SucursalUserName,
        //            UserName = empleado.Nombre
        //        };

        //        document = await _documentService.CrearFacturaAsync(document, boLogin);
        //        return Ok(document);
        //    }
        //    return BadRequest();
        //}
    }
}
