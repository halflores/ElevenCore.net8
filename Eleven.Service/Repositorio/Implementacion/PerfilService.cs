using AutoMapper;
using Eleven.Data.Entidad;
using Eleven.Data.Repositorio.Implementacion;
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
    public class PerfilService : ServiceAsync<Perfil, MdoPerfil>, IPerfilService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Perfil> _perfilRepository;
        private readonly ILogger<PerfilService> _logger;

        public PerfilService(IRepository<Perfil> perfilRepository, IMapper mapper, ILogger<PerfilService> logger)
            : base(perfilRepository, mapper, logger)
        {
            _perfilRepository = perfilRepository;
            _mapper = mapper;
            _logger = logger;
        }
    }
}
