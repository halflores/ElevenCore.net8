using SAPBo.Data.Entidad;

namespace SAPBo.Data.Repositorio.Interfaz
{
    public interface IBoDocumentRepository
    {
        Task<List<BatchResponse>> CreateOfertaOrderAsync(Document oferta, Document order, Login login);
        Task<List<BatchResponse>> CreateOfertaOrderInvoiceAsync(Document oferta, Document order, Document invoice, Login login);
        Task<List<BatchResponse>> CreateOfertaOrderInvoiceDeliveryAsync(Document oferta, Document order, Document invoice, Document delivery, Login login);
        Task<List<BatchResponse>> CreateOfertaOrderInvoicePaymentDeliveryAsync(Document oferta, Document order, Document invoice, Payment payment, Document delivery, Login login);
        Task<Document> CreateInvoiceAsync(Document invoice, Login login);
    }
}
