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
    public class TipoDocumentoRepository : Repository<TipoDocumento>, ITipoDocumentoRepository
    {

        protected readonly RepositoryPatternContext _repositoryPatternContext;
        private readonly ILogger<TipoDocumentoRepository> _logger;
        public TipoDocumentoRepository(RepositoryPatternContext repositoryPatternContext, ILogger<TipoDocumentoRepository> logger) : base(repositoryPatternContext, logger)
        {
            _repositoryPatternContext = repositoryPatternContext;
            _logger = logger;
        }

        //public IQueryable<TipoDocumento> TodoAsync()
        //{
        //    //Expression<Func<TEntity, object>>[]
        //    try
        //    {
        //        //List<TEntity> entities = null;
        //        return RepositoryPatternContext.TipoDocumento.AsQueryable();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception($"Couldn't retrieve entities: {ex.Message}");
        //    }
        //}
    }
}
