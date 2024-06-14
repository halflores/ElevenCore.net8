using Eleven.Service.Modelo.Eleven;
using Eleven.Service.Modelo.SAP;
using Eleven.Service.Repositorio.Interfaz;
using Eleven.WebApi.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;

namespace Eleven.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthenticateService _authenticateService;
        private readonly IConfiguration _configuration;
        private readonly IEmployeeService _employeeService;

        public AuthenticateController(ILogger<AuthenticateController> logger,
                                        IConfiguration configuration,
                                        IUsuarioService usuarioService,
                                        IAuthenticateService authenticateService,
                                        IEmployeeService employeeService)
        {
            _logger = logger;
            _configuration = configuration; 
            _usuarioService = usuarioService;
            _authenticateService = authenticateService;
            _employeeService = employeeService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] MdoLogin model)
        {
            try
            {
                _logger.LogInformation("login de usuario ...");

                var user = await _usuarioService.FindByNameAsync(model.Login);
                if (user != null && _authenticateService.CheckPassword(user, model.Password))
                {
                    //var userRoles = await userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Login),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    //foreach (var userRole in userRoles)
                    //{
                    //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    //}

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddMinutes(15), // tiempo en el que expira, modificar para un tiempo mayor
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                    _logger.LogInformation("login de usuario, verificar empleado id ...");

                    MdoEmpleado boEmpleado = await _employeeService.GetEmployeeAsync(user.EmpleadoSAP);
                    //var employee = new EmployeeDetails();
                    //employee.EmployeeId = user.UsuarioId.ToString();
                    //employee.DesignationId = user.EmpleadoSAP.ToString();

                    HttpContext.Session.SetObjectAsJson("Employee", boEmpleado);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }

        }

        // GET: api/<AuthenticateController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthenticateController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthenticateController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthenticateController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthenticateController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
