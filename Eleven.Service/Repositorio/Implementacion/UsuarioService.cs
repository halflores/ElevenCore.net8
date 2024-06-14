using AutoMapper;
using Eleven.Data.Entidad;
using Eleven.Data.Repositorio.Implementacion;
using Eleven.Data.Repositorio.Interfaz;
using Eleven.Service.Modelo.Eleven;
using Eleven.Service.Repositorio.Interfaz;
using Microsoft.Extensions.Logging;
using SAPBo.Data.Repositorio.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Repositorio.Implementacion
{
    public class UsuarioService : ServiceAsync<Usuario, MdoUsuario>, IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly IUsuarioRepository _usrRepository;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(IRepository<Usuario> usuarioRepository, IMapper mapper, ILogger<UsuarioService> logger, IUsuarioRepository usrRepository)
            :base (usuarioRepository, mapper, logger) 
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _logger = logger;
            _usrRepository = usrRepository;
        }
                
        public async Task<MdoUsuario?> FindByNameAsync(string login) 
        {
            try
            {
                MdoUsuario mdoUsuario = new MdoUsuario();
                if (login == null)
                {
                    throw new ArgumentNullException("no es posible hacer login");
                }

                Usuario? usuario = await _usuarioRepository.GetFirstAsync(x => x.Login == login);
                if (usuario == null)
                    return null;
                else
                    mdoUsuario = _mapper.Map<MdoUsuario>(usuario);

                return mdoUsuario;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FindByNameAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        
        }
        public async Task<MdoUsuario> Create(MdoUsuario MdoUsuario)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario = _mapper.Map<Usuario>(MdoUsuario);
                usuario.RegPassword = MdoUsuario.Password;
                usuario = await _usrRepository.Create(usuario);
                MdoUsuario = _mapper.Map<MdoUsuario>(usuario);
                return MdoUsuario;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

    }
}
