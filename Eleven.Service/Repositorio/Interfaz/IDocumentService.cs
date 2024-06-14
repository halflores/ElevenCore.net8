using Eleven.Service.Modelo.SAP;

namespace Eleven.Service.Repositorio.Interfaz
{
    public interface IDocumentService
    {
        Task<List<MdoBatchResponse>> CreateOfertaOrderAsync(MdoDocument modelo, MdoSLConexion boLogin);
        Task<List<MdoBatchResponse>> CreateOfertaOrderFacturaAsync(MdoDocument modelo, MdoSLConexion boLogin);
        Task<List<MdoBatchResponse>> CreateOfertaOrderFacturaEntregaAsync(MdoDocument modelo, MdoSLConexion boLogin);
        Task<List<MdoBatchResponse>> CreateOfertaOrderFacturaPagoEntregaAsync(MdoDocument modelo, MdoSLConexion boLogin);
        Task<MdoDocument> CrearFacturaAsync(MdoDocument modelo, MdoSLConexion boLogin);
    }
}
