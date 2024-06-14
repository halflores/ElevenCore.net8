using Eleven.Service.Modelo.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Repositorio.Interfaz
{
    public interface IBusinessPartnersService
    {
        Task<MdoBusinessPartners> GetBusinessPartnerByCardCodeAsync(string cardCode, MdoSLConexion boLogin);
        Task<MdoBusinessPartners> GetBusinessPartnerByNroDocumentoAsync(string nroDocumento, string tipoDocumento, MdoSLConexion boLogin);
        Task<MdoBusinessPartners> CreateBusinessPartnerAsync(MdoBusinessPartners mdoBusinessPartners, MdoSLConexion boLogin);
        Task<List<MdoBatchResponse>> CreateBatchBusinessPartnerAsync(List<MdoBusinessPartners> mdoBPs, MdoEmpleado mdoEmpleado, MdoSLConexion boLogin);
    }
}
