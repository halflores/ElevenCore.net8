using Eleven.Service.Repositorio.Interfaz;
using Utils.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Eleven.Service.Modelo.SAP;
using Eleven.Service.Repositorio.Implementacion;
using Eleven.WebApi.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eleven.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        // GET: api/<PaymentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PaymentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PaymentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MdoPayment payment)
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
                    payment = await _paymentService.CreatePaymentAsync(payment, boLogin); // SLConnection object
                    return Ok(payment);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }


        }

        // PUT api/<PaymentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PaymentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
