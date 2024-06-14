using Eleven.Service.Repositorio.Interfaz;
using Eleven.Service.Modelo.Eleven;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Eleven.WebApi.Helper;
using Eleven.Service.Modelo.SAP;

namespace Eleven.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService) 
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }  
        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuarioController>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public void Post([FromBody] MdoUsuario usuario)
        {
            try
            {
                _usuarioService.AddAsync(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        [HttpPost]
        [Route("Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] MdoUsuario mdoUsuario)
        {
            try
            {
                if (HttpContext.Session.IsAvailable && HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee") != null)
                {
                    MdoEmpleado? employeeDetails = HttpContext.Session.GetObjectFromJson<MdoEmpleado>("Employee");
                    _logger.LogInformation("Create user ...");
                    mdoUsuario = await _usuarioService.Create(mdoUsuario);
                    return Ok(mdoUsuario);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
