using Eleven.Data.Entidad;
using Eleven.Data.Repositorio.Implementacion;
using Eleven.Data.Repositorio.Interfaz;
using Eleven.Service.Repositorio.Interfaz;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using Eleven.Service.Modelo.SAP;

namespace Eleven.Service.Repositorio.Implementacion
{
    public class TipoDocumentoService : ServiceAsync<TipoDocumento, MdoTipoDocumento>, ITipoDocumentoService
    {

        private readonly IMapper _mapper;
        private readonly IRepository<TipoDocumento> _tipoDocumentoRepository;
        private readonly ILogger<TipoDocumentoService> _logger;
        private readonly ITipoDocumentoRepository _tpoDocumentoRepository;
        public TipoDocumentoService(IRepository<TipoDocumento> tipoDocumentoRepository, IMapper mapper, ILogger<TipoDocumentoService> logger, ITipoDocumentoRepository tpoDocumentoRepository)
            : base(tipoDocumentoRepository, mapper, logger)
        {
            _tipoDocumentoRepository = tipoDocumentoRepository;
            _mapper = mapper;
            _logger = logger;
            _tpoDocumentoRepository = tpoDocumentoRepository;
        }

    }
}
