using SAPBo.Data.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Repositorio.Interfaz
{
    public interface IBoBusinessPartnersRepository
    {
        Task<BusinessPartners> GetBusinessPartnerByNroDocumentoAsync(string nroDocumento, string tipoDocumento, Login login);
        Task<BusinessPartners> GetBusinessPartnerByCardCodeAsync(string cardCode, Login login);
        Task<BusinessPartners> CreateBusinessPartnerAsync(BusinessPartners bpEntidad, Login login);
        Task<List<BatchResponse>> CreateBatchBusinessPartnerAsync(List<BusinessPartners> bpEntidades, Login login);
    }
}
