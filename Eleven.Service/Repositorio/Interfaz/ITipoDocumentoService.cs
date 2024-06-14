using Eleven.Data.Entidad;
using Eleven.Service.Modelo.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Repositorio.Interfaz
{
    public interface ITipoDocumentoService : IServiceAsync<TipoDocumento, MdoTipoDocumento>
    {
    }
}
