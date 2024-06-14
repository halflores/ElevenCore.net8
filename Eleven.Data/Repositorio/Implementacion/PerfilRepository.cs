using Eleven.Data.Entidad;
using Eleven.Data.Repositorio.Interfaz;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Data.Repositorio.Implementacion
{
    public class PerfilRepository : Repository<Perfil>, IPerfilRepository
    {
        protected readonly RepositoryPatternContext _repositoryPatternContext;
        private readonly ILogger<PerfilRepository> _logger;
        public PerfilRepository(RepositoryPatternContext repositoryPatternContext, ILogger<PerfilRepository> logger) : base(repositoryPatternContext, logger)
        {
            _repositoryPatternContext = repositoryPatternContext;
            _logger = logger;
        }
    }
}
