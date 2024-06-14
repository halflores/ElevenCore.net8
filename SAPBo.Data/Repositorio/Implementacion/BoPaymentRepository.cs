using B1SLayer;
using Microsoft.Extensions.Logging;
using SAPBo.Data.Entidad;
using SAPBo.Data.Repositorio.Interfaz;
using System.Data;

namespace SAPBo.Data.Repositorio.Implementacion
{
    public class BoPaymentRepository : IBoPaymentRepository
    {
        private readonly Dictionary<string, SLConnection> _slConnections;
        private readonly ILogger<BoPaymentRepository> _logger;

        public BoPaymentRepository(Dictionary<string, SLConnection> slConnections, ILogger<BoPaymentRepository> logger)
        {
            _slConnections = slConnections;
            _logger = logger;
        }

        public async Task<Payment> CreatePaymentAsync(Payment ePago, Login login)
        {
            try
            {
                List<BatchResponse> batchResponses = new List<BatchResponse>();

                #region CREAR PAYMENT
                var payment = new
                {
                    DocType = ePago.DocType,
                    CardCode = ePago.CardCode,
                    ControlAccount = ePago.ControlAccount,
                    CashAccount = ePago.CashAccount,
                    CashSum = ePago.CashSum,
                    U_TipoPago = ePago.U_TipoPago,
                    U_CajaControl = ePago.U_CajaControl,
                    U_OrigenAplicacion = ePago.U_OrigenAplicacion,
                    DocCurrency = ePago.DocCurrency,
                    PaymentInvoices = ePago.PaymentInvoices
                                     .Select(x => new
                                     {
                                         LineNum = x.LineNum,
                                         DocEntry = x.DocEntry,
                                         SumApplied = x.SumApplied,
                                         InvoiceType = x.InvoiceType,
                                         InstallmentId = x.InstallmentId
                                     }).ToList(),
                    PaymentCreditCards = ePago.PaymentCreditCards
                                     .Select(x => new
                                     {
                                         CreditCard = x.CreditCard,
                                         CreditAcct = x.CreditAcct,
                                         CreditCardNumber = x.CreditCardNumber,
                                         CardValidUntil = x.CardValidUntil,
                                         VoucherNum = x.VoucherNum,
                                         OwnerIdNum = x.OwnerIdNum,
                                         CreditSum = x.CreditSum,
                                         PaymentMethodCode = x.PaymentMethodCode
                                     }).ToList(),
                    PaymentChecks = ePago.PaymentChecks
                                     .Select(x => new
                                     {
                                         DueDate = x.DueDate,
                                         CheckSum = x.CheckSum,
                                         CountryCode = x.CountryCode,
                                         BankCode = x.BankCode,
                                         AccounttNum = x.AccounttNum,
                                         Trnsfrable = x.Trnsfrable,
                                         CheckNumber = x.CheckNumber,
                                         OriginallyIssuedBy = x.OriginallyIssuedBy,
                                         FiscalID = x.FiscalID,
                                         Details = x.Details
                                     }).ToList()
                };
                #endregion

                Payment results = await _slConnections[login.SLConexion].Request("IncomingPayments").PostAsync<Payment>(ePago);

                //Tuple<HttpResponseMessage[], string> results = await _slConnections[login.SLConexion].PostBatchAsync(objPayment);

                //foreach (HttpResponseMessage resultItem in results.Item1)
                //{
                //    if (Convert.ToInt32(resultItem.StatusCode) == 201)
                //    {
                //        string urlLocation = resultItem.Headers.GetValues("Location").SingleOrDefault()!;
                //        string docEntry = Functions.GetDocEntry(urlLocation!)!;

                //        batchResponses.Add(new BatchResponse
                //        {
                //            StatusCode = Convert.ToInt32(resultItem.StatusCode),
                //            //ContentID = Convert.ToInt32(resultItem.Headers.Select(x => x.Key == "").SingleOrDefault()),
                //            Location = urlLocation,
                //            DocEntry = Convert.ToInt32(docEntry),
                //            Description = "ÉXITO"
                //        });
                //    }
                //    else
                //    {
                //        batchResponses.Add(new BatchResponse
                //        {
                //            StatusCode = Convert.ToInt32(resultItem.StatusCode),
                //            //ContentID = Convert.ToInt32(resultItem.Headers.GetValues("Content-ID").SingleOrDefault()),
                //            Location = @"—Bien, Houston, hemos tenido un problema aquí.",
                //            DocEntry = 0,
                //            Description = results.Item2
                //        });
                //    }

                //}
                
                
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreatePaymentAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

    }
}
