using AutoMapper;
using Eleven.Data.Entidad;
using Eleven.Data.Repositorio.Interfaz;
using Eleven.Service.Modelo.Eleven;
using Eleven.Service.Repositorio.Interfaz;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Repositorio.Implementacion
{
    public class AuthenticateService : IAuthenticateService
    {

        private readonly IMapper _mapper;
        private readonly IAuthenticateRepository  _authenticateRepository;
        private readonly ILogger<AuthenticateService> _logger;

        public AuthenticateService(
                                    IMapper mapper, 
                                    ILogger<AuthenticateService> logger,
                                    IAuthenticateRepository authenticateRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _authenticateRepository = authenticateRepository;   
        }
        public bool CheckPassword(MdoUsuario usuario, string password)
        {
            try
            {
                bool res = _authenticateRepository.CheckPassword(usuario.Login, password);
                return res; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CheckPassword");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }
    }
}
