using B1SLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SAPBo.Data.Entidad;
using SAPBo.Data.Repositorio.Interfaz;
using System.Text.Json.Serialization;
using System.Text.Json;
using Utils.Helper;
using static System.Runtime.CompilerServices.RuntimeHelpers;


namespace SAPBo.Data.Repositorio.Implementacion
{
    public class BoDocumentRepository : IBoDocumentRepository
    {
        private readonly Dictionary<string, SLConnection> _slConnections;
        public BoDocumentRepository(Dictionary<string, SLConnection> slConnections)
        {
            _slConnections = slConnections;
        }

        public async Task<List<BatchResponse>> CreateOfertaOrderAsync(Document oferta, Document order, Login login)
        {
            try
            {
                List<BatchResponse> batchResponses = new List<BatchResponse>();

                #region CREAR OFERTA DE VENTA, modelo a tomar
                //var ofertaVenta = new
                //{
                //    CardCode = oferta.CardCode,
                //    DocDueDate = oferta.DocDueDate,
                //    DocumentLines = oferta.DocumentLines
                //                     .Select(x => new
                //                     {
                //                         ItemCode = x.ItemCode,
                //                         Quantity = x.Quantity,
                //                         WhsCode = x.WhsCode,
                //                         LineNum = x.LineNum,
                //                     }).ToList()
                //};

                //var objQuotations = new SLBatchRequest(
                //    HttpMethod.Post,
                //    "Quotations",
                //    ofertaVenta,
                //    1); // Content-ID for this entity
                #endregion

                #region CREAR ORDEN DE VENTA, modelo a tomar
                var orderVenta = new
                {
                    CardCode = order.CardCode,
                    DocDueDate = order.DocDueDate,
                    PaymentGroupCode = -1,
                    Indicator = 4,
                    Series = 142,
                    ReserveInvoice = "tYES",
                    DocType = "dDocument_Items",
                    U_LB_TipoTransaccion = 1,
                    U_LB_ModalidadTransa = 1,
                    U_LB_Bancarizacion = "N",
                    WithholdingTaxDataCollection = order.WithholdingTaxDataCollection.Select(x => new
                    {
                        WTCode = x.WTCode,
                        WTAmount = x.WTAmount
                    }).ToList(),
                    DocumentLines = order.DocumentLines
                                     .Select(x => new
                                     {
                                         FreeText = "CONTADO",
                                         //BaseEntry = x.BaseEntry,
                                         ItemCode = x.ItemCode,
                                         //BaseLine = x.BaseLine,
                                         //BaseType = x.BaseType,
                                         WarehouseCode = x.WhsCode,
                                         //Price = x.Price,
                                         U_PrecioBase = x.Price,
                                         U_PrecioDescontado= x.U_PrecioDescontado,
                                         U_DescuentoPOS = x.U_DescuentoPOS,
                                         TaxCode = "IVA",
                                         Quantity = x.Quantity,
                                         LineNum = x.LineNum,
                                         UoMEntry = x.UoMEntry,
                                         PriceAfterVAT = x.PriceAfterVAT,
                                         DiscountPercent = x.DiscountPercent,
                                         //Discount = x.Discount,
                                         BackOrder = "tYES",
                                         //SerialNumbers = [],
                                         EntregaAutomatica = false,
                                         TypeSale = 1,
                                         TypeProduct = 1,
                                         DocumentLineAdditionalExpense = x.DocumentLineAdditionalExpenses.Select(y => new
                                         {
                                             ExpenseCode = y.ExpenseCode,
                                             LineTotal = y.LineTotal,
                                         }).FirstOrDefault()
                                     }).ToList()
                };

                var objOrders = new SLBatchRequest(
                    HttpMethod.Post,
                    "Invoices",
                    orderVenta,
                    1);
                #endregion

                //Document createdBP = await _slConnections[login.SLConexion]
                //                            .Request("Invoices")
                //                            .PostAsync<Document>(orderVenta);

                Tuple<HttpResponseMessage[], string> results = await _slConnections[login.SLConexion].PostBatchAsync(objOrders);

                foreach (HttpResponseMessage resultItem in results.Item1)
                {
                    if (Convert.ToInt32(resultItem.StatusCode) == 201)
                    {
                        string urlLocation = resultItem.Headers.GetValues("Location").SingleOrDefault()!;
                        string docEntry = Functions.GetDocEntry(urlLocation!)!;

                        batchResponses.Add(new BatchResponse { 
                                                StatusCode = Convert.ToInt32(resultItem.StatusCode),
                                                //ContentID = Convert.ToInt32(resultItem.Headers.Select(x => x.Key == "").SingleOrDefault()),
                                                Location = urlLocation,
                                                DocEntry = Convert.ToInt32(docEntry),
                                                Description = "ÉXITO" 
                        });
                    }
                    else
                    {
                        batchResponses.Add(new BatchResponse
                        {
                            StatusCode = Convert.ToInt32(resultItem.StatusCode),
                            //ContentID = Convert.ToInt32(resultItem.Headers.GetValues("Content-ID").SingleOrDefault()),
                            Location = @"—Bien, Houston, hemos tenido un problema aquí.",
                            DocEntry = 0,
                            Description = results.Item2
                        });
                    }

                }
                return batchResponses;
            }
            catch (Exception ex)
            {
                //LogException.Write(ex);
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task<List<BatchResponse>> CreateOfertaOrderInvoiceAsync(Document oferta, Document order, Document invoice, Login login)
        {
            try
            {
                List<BatchResponse> batchResponses = new List<BatchResponse>();

                #region CREAR OFERTA DE VENTA, modelo a tomar
                var ofertaVenta = new
                {
                    CardCode = oferta.CardCode,
                    DocDueDate = oferta.DocDueDate,
                    DocumentLines = oferta.DocumentLines
                                     .Select(x => new
                                     {
                                         ItemCode = x.ItemCode,
                                         Quantity = x.Quantity,
                                         WarehouseCode = x.WhsCode,
                                         LineNum = x.LineNum,
                                         UoMEntry = x.UoMEntry,
                                         Price = x.Price,
                                         TaxCode = x.TaxCode,
                                         PriceAfterVAT = x.PriceAfterVAT
                                     }).ToList()
                };

                var objQuotations = new SLBatchRequest(
                    HttpMethod.Post,
                    "Quotations",
                    ofertaVenta,
                    1); // Content-ID for this entity
                #endregion

                #region CREAR ORDEN DE VENTA, modelo a tomar
                var orderVenta = new
                {
                    CardCode = order.CardCode,
                    DocDueDate = order.DocDueDate,
                    PaymentGroupCode = order.PaymentGroupCode,
                    WithholdingTaxDataCollection = order.WithholdingTaxDataCollection.Select(x => new
                                                                                        {
                                                                                            WTCode = x.WTCode,
                                                                                            WTAmount = x.WTAmount
                                                                                        }).ToList(),
                    DocumentLines = order.DocumentLines
                                     .Select(x => new
                                     {
                                         BaseEntry = x.BaseEntry,
                                         BaseLine = x.BaseLine,
                                         BaseType = x.BaseType,
                                         WarehouseCode = x.WhsCode,
                                         TaxCode = x.TaxCode,
                                         PriceAfterVAT = x.PriceAfterVAT,
                                         DocumentLineAdditionalExpense = x.DocumentLineAdditionalExpenses.Select(y => new
                                         {
                                             ExpenseCode = y.ExpenseCode,
                                             LineTotal = y.LineTotal,
                                         }).FirstOrDefault()
                                     }).ToList()
                };

                var objOrders = new SLBatchRequest(
                    HttpMethod.Post,
                    "Orders",
                    orderVenta,
                    2);
                #endregion

                #region CREAR FACTURA VENTA, modelo a tomar
                var invoiceVenta = new
                {
                    CardCode = invoice.CardCode,
                    DocDueDate = invoice.DocDueDate,
                    PaymentGroupCode = invoice.PaymentGroupCode,
                    Indicator = 4,
                    Series = 142,
                    ReserveInvoice = "tYES",
                    DocType = "dDocument_Items",
                    WithholdingTaxDataCollection = invoice.WithholdingTaxDataCollection.Select(x => new 
                                                                                        {
                                                                                            WTCode = x.WTCode,
                                                                                            WTAmount = x.WTAmount
                                                                                        }).ToList(), 
                    DocumentLines = invoice.DocumentLines
                                     .Select(x => new
                                     {
                                         BaseEntry = x.BaseEntry,
                                         BaseLine = x.BaseLine,
                                         BaseType = x.BaseType,
                                         TaxCode = x.TaxCode,
                                         WarehouseCode = x.WhsCode,
                                         PriceAfterVAT = x.PriceAfterVAT,
                                         DocumentLineAdditionalExpense = x.DocumentLineAdditionalExpenses.Select(y => new 
                                                                                                                {
                                                                                                                     ExpenseCode = y.ExpenseCode,
                                                                                                                     LineTotal = y.LineTotal,
                                                                                                                 }).FirstOrDefault()
                                     }).ToList()
                };

                var objInvoice = new SLBatchRequest(
                    HttpMethod.Post,
                    "Invoices",
                    invoiceVenta,
                    3);
                #endregion

                Tuple<HttpResponseMessage[], string> results = await _slConnections[login.SLConexion].PostBatchAsync(objQuotations, objOrders, objInvoice);

                foreach (HttpResponseMessage resultItem in results.Item1)
                {
                    if (Convert.ToInt32(resultItem.StatusCode) == 201)
                    {
                        string urlLocation = resultItem.Headers.GetValues("Location").SingleOrDefault()!;
                        string docEntry = Functions.GetDocEntry(urlLocation!)!;

                        batchResponses.Add(new BatchResponse
                        {
                            StatusCode = Convert.ToInt32(resultItem.StatusCode),
                            //ContentID = Convert.ToInt32(resultItem.Headers.Select(x => x.Key == "").SingleOrDefault()),
                            Location = urlLocation,
                            DocEntry = Convert.ToInt32(docEntry),
                            Description = "ÉXITO"
                        });
                    }
                    else
                    {
                        batchResponses.Add(new BatchResponse
                        {
                            StatusCode = Convert.ToInt32(resultItem.StatusCode),
                            //ContentID = Convert.ToInt32(resultItem.Headers.GetValues("Content-ID").SingleOrDefault()),
                            Location = @"—Bien, Houston, hemos tenido un problema aquí.",
                            DocEntry = 0,
                            Description = results.Item2
                        });
                    }

                }
                return batchResponses;
            }
            catch (Exception ex)
            {
                //LogException.Write(ex);
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task<List<BatchResponse>> CreateOfertaOrderInvoiceDeliveryAsync(Document oferta, Document order, Document invoice, Document delivery, Login login)
        {
            try
            {
                List<BatchResponse> batchResponses = new List<BatchResponse>();

                #region CREAR OFERTA DE VENTA, modelo a tomar
                var ofertaVenta = new
                {
                    CardCode = oferta.CardCode,
                    DocDueDate = oferta.DocDueDate,
                    //PaymentGroupCode = order.PaymentGroupCode,
                    DocType = "dDocument_Items",
                    //U_LB_TipoTransaccion = order.U_LB_TipoTransaccion,
                    //U_LB_ModalidadTransa = order.U_LB_ModalidadTransa,
                    //U_LB_Bancarizacion = order.U_LB_Bancarizacion,
                    //WithholdingTaxDataCollection = order.WithholdingTaxDataCollection.Select(x => new
                    //{
                    //    WTCode = x.WTCode,
                    //    WTAmount = x.WTAmount
                    //}).ToList(),
                    DocumentLines = order.DocumentLines
                                     .Select(x => new
                                     {
                                         //FreeText = x.FreeText,
                                         ItemCode = x.ItemCode,
                                         WarehouseCode = x.WhsCode,
                                         //Price = x.Price, no se habilita, porque descuadra el resultado
                                         U_PrecioBase = x.Price,
                                         U_PrecioDescontado = x.U_PrecioDescontado,
                                         U_DescuentoPOS = x.U_DescuentoPOS,
                                         //TaxCode = x.TaxCode,
                                         Quantity = x.Quantity,
                                         LineNum = x.LineNum,
                                         UoMEntry = x.UoMEntry,
                                         PriceAfterVAT = x.PriceAfterVAT,
                                         DiscountPercent = x.DiscountPercent,
                                         //Discount = x.Discount,no se habilita, porque descuadra el resultado
                                         BackOrder = x.BackOrder,
                                         //SerialNumbers = [],
                                         EntregaAutomatica = false,
                                         TypeSale = 1,
                                         TypeProduct = 1,
                                         //DocumentLineAdditionalExpense = x.DocumentLineAdditionalExpenses.Select(y => new
                                         //{
                                         //    ExpenseCode = y.ExpenseCode,
                                         //    LineTotal = y.LineTotal,
                                         //}).FirstOrDefault()
                                     }).ToList()
                };

                var objQuotations = new SLBatchRequest(
                    HttpMethod.Post,
                    "Quotations",
                    ofertaVenta,
                    1); // Content-ID for this entity
                #endregion

                #region CREAR ORDEN DE VENTA, modelo a tomar
                var orderVenta = new
                {
                    CardCode = order.CardCode,
                    DocDueDate = order.DocDueDate,
                    DocType = "dDocument_Items",
                    //PaymentGroupCode = order.PaymentGroupCode,
                    //Indicator = order.Indicator, solo se habilita en invoice
                    //Series = order.Series, solo se habilita en invoice
                    //ReserveInvoice = order.ReserveInvoice, solo se habilita en invoice
                    //DocType = order.DocType, solo se habilita en invoice
                    //U_LB_TipoTransaccion = order.U_LB_TipoTransaccion,
                    //U_LB_ModalidadTransa = order.U_LB_ModalidadTransa,
                    //U_LB_Bancarizacion = order.U_LB_Bancarizacion,
                    //WithholdingTaxDataCollection = order.WithholdingTaxDataCollection.Select(x => new
                    //{
                    //    WTCode = x.WTCode,
                    //    WTAmount = x.WTAmount
                    //}).ToList(),
                    DocumentLines = order.DocumentLines
                                     .Select(x => new
                                     {
                                         //FreeText = x.FreeText,
                                         BaseEntry = "$1", // x.BaseEntry,
                                         BaseLine = x.BaseLine,
                                         BaseType = 23, //x.BaseType,
                                         //////////////////////////////////////ItemCode = x.ItemCode,
                                         //////////////////////////////////////WarehouseCode = x.WhsCode,
                                         //Price = x.Price, no se habilita, porque descuadra el resultado
                                         //////////////////////////////////////U_PrecioBase = x.Price,
                                         //////////////////////////////////////U_PrecioDescontado = x.U_PrecioDescontado,
                                         //////////////////////////////////////U_DescuentoPOS = x.U_DescuentoPOS,
                                         TaxCode = x.TaxCode,
                                         //////////////////////////////////////Quantity = x.Quantity,
                                         //////////////////////////////////////LineNum = x.LineNum,
                                         //////////////////////////////////////UoMEntry = x.UoMEntry,
                                         //////////////////////////////////////PriceAfterVAT = x.PriceAfterVAT,
                                         //////////////////////////////////////DiscountPercent = x.DiscountPercent,
                                         //Discount = x.Discount,no se habilita, porque descuadra el resultado
                                         //BackOrder = x.BackOrder, 
                                         //SerialNumbers = [],
                                         //////////////////////////////////////EntregaAutomatica = false,
                                         //////////////////////////////////////TypeSale = 1,
                                         //////////////////////////////////////TypeProduct = 1,
                                         //DocumentLineAdditionalExpense = x.DocumentLineAdditionalExpenses.Select(y => new
                                         //{
                                         //    ExpenseCode = y.ExpenseCode,
                                         //    LineTotal = y.LineTotal,
                                         //}).FirstOrDefault()
                                     }).ToList()
                };

                var objOrders = new SLBatchRequest(
                    HttpMethod.Post,
                    "Orders",
                    orderVenta,
                    2);
                #endregion

                #region CREAR FACTURA VENTA, modelo a tomar
                var invoiceVenta = new
                {
                    CardCode = invoice.CardCode,
                    DocDueDate = invoice.DocDueDate,
                    PaymentGroupCode = invoice.PaymentGroupCode,
                    Indicator = invoice.Indicator,
                    Series = invoice.Series,
                    ReserveInvoice = invoice.ReserveInvoice,
                    DocType = invoice.DocType,
                    U_LB_TipoTransaccion = invoice.U_LB_TipoTransaccion,
                    U_LB_ModalidadTransa = invoice.U_LB_ModalidadTransa,
                    U_LB_Bancarizacion = invoice.U_LB_Bancarizacion,
                    WithholdingTaxDataCollection = invoice.WithholdingTaxDataCollection.Select(x => new
                    {
                        WTCode = x.WTCode,
                        WTAmount = x.WTAmount
                    }).ToList(),
                    DocumentLines = invoice.DocumentLines
                                     .Select(x => new
                                     {
                                         BaseEntry = "$2", //x.BaseEntry,
                                         BaseLine = x.BaseLine,
                                         BaseType = 17, //x.BaseType,
                                         //////////////////////////////////////ItemCode = x.ItemCode,
                                         //////////////////////////////////////FreeText = x.FreeText,
                                         //////////////////////////////////////WarehouseCode = x.WhsCode,
                                         //Price = x.Price, no se habilita, porque descuadra el resultado
                                         //////////////////////////////////////U_PrecioBase = x.Price,
                                         //////////////////////////////////////U_PrecioDescontado = x.U_PrecioDescontado,
                                         //////////////////////////////////////U_DescuentoPOS = x.U_DescuentoPOS,
                                         //////////////////////////////////////TaxCode = x.TaxCode,
                                         //////////////////////////////////////Quantity = x.Quantity,
                                         //////////////////////////////////////LineNum = x.LineNum,
                                         //////////////////////////////////////UoMEntry = x.UoMEntry,
                                         //////////////////////////////////////PriceAfterVAT = x.PriceAfterVAT,
                                         //////////////////////////////////////DiscountPercent = x.DiscountPercent,
                                         //Discount = x.Discount,no se habilita, porque descuadra el resultado
                                         //BackOrder = x.BackOrder,
                                         //SerialNumbers = [],
                                         ////////////////////////////////////////EntregaAutomatica = false,
                                         ////////////////////////////////////////TypeSale = 1,
                                         ////////////////////////////////////////TypeProduct = 1,
                                         DocumentLineAdditionalExpense = x.DocumentLineAdditionalExpenses.Select(y => new
                                         {
                                             ExpenseCode = y.ExpenseCode,
                                             LineTotal = y.LineTotal,
                                         }).FirstOrDefault()
                                     }).ToList()
                };

                var objInvoice = new SLBatchRequest(
                    HttpMethod.Post,
                    "Invoices",
                    invoiceVenta,
                    3);
                #endregion

                #region CREAR ENTREGA
                var deliveryVenta = new
                {
                    CardCode = delivery.CardCode,
                    DocDueDate = delivery.DocDueDate,
                    PaymentGroupCode = delivery.PaymentGroupCode,
                    DocumentLines = delivery.DocumentLines
                                     .Select(x => new
                                     {
                                         BaseEntry = x.BaseEntry,
                                         BaseLine = x.BaseLine,
                                         BaseType = x.BaseType,
                                         //WarehouseCode = x.WhsCode,
                                         //TaxCode = x.TaxCode,
                                         //PriceAfterVAT = x.PriceAfterVAT
                                     }).ToList()
                };

                var objDelivery = new SLBatchRequest(
                    HttpMethod.Post,
                    "DeliveryNotes",
                    deliveryVenta,
                    4);
                #endregion

                Tuple<HttpResponseMessage[], string> results = await _slConnections[login.SLConexion].PostBatchAsync(objQuotations, objOrders, objInvoice, objDelivery);

                foreach (HttpResponseMessage resultItem in results.Item1)
                {
                    if (Convert.ToInt32(resultItem.StatusCode) == 201)
                    {
                        string urlLocation = resultItem.Headers.GetValues("Location").SingleOrDefault()!;
                        string docEntry = Functions.GetDocEntry(urlLocation!)!;

                        batchResponses.Add(new BatchResponse
                        {
                            StatusCode = Convert.ToInt32(resultItem.StatusCode),
                            //ContentID = Convert.ToInt32(resultItem.Headers.Select(x => x.Key == "").SingleOrDefault()),
                            Location = urlLocation,
                            DocEntry = Convert.ToInt32(docEntry),
                            Description = "ÉXITO"
                        });

                        //if (urlLocation.Contains("Orders"))
                        //{
                        //    var createdBP = await _slConnections[login.SLConexion]
                        //    .Request("Invoices")
                        //    .PostAsync<Document>(invoiceVenta);

                        //    if (createdBP != null)
                        //    {
                        //        int inc = 0;
                        //        //createdBP.WithholdingTaxDataCollection = invoice.WithholdingTaxDataCollection.Select(x => new
                        //        //{
                        //        //    WTCode = x.WTCode,
                        //        //    WTAmount = x.WTAmount
                        //        //}).ToList();
                        //        invoice.DocEntry = createdBP.DocEntry;
                        //        invoice.WithholdingTaxDataCollection = null;
                        //        invoice.DocumentLines.ForEach(i => { i.BaseEntry = docEntry; i.BaseType = 17; i.BaseLine = inc++;});

                        //        await _slConnections[login.SLConexion]
                        //        .Request("Invoices", createdBP!.DocEntry!)
                        //        .PatchAsync(invoice);

                        //        batchResponses.Add(new BatchResponse
                        //        {
                        //            StatusCode = Convert.ToInt32(resultItem.StatusCode),
                        //            //ContentID = Convert.ToInt32(resultItem.Headers.Select(x => x.Key == "").SingleOrDefault()),
                        //            Location = urlLocation,
                        //            DocEntry = createdBP.DocEntry,
                        //            Description = "ÉXITO"
                        //        });
                        //    }
                        //}

                    }
                    else
                    {
                        batchResponses.Add(new BatchResponse
                        {
                            StatusCode = Convert.ToInt32(resultItem.StatusCode),
                            //ContentID = Convert.ToInt32(resultItem.Headers.GetValues("Content-ID").SingleOrDefault()),
                            Location = @"—Bien, Houston, hemos tenido un problema aquí.",
                            DocEntry = 0,
                            Description = results.Item2
                        });
                    }

                }
                return batchResponses;
            }
            catch (Exception ex)
            {
                //LogException.Write(ex);
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task<List<BatchResponse>> CreateOfertaOrderInvoicePaymentDeliveryAsync(Document oferta, Document order, Document invoice, Payment payment, Document delivery, Login login)
        {
            try
            {
                List<BatchResponse> batchResponses = new List<BatchResponse>();

                #region CREAR OFERTA DE VENTA, modelo a tomar
                var ofertaVenta = new
                {
                    CardCode = oferta.CardCode,
                    DocDueDate = oferta.DocDueDate,
                    DocumentLines = oferta.DocumentLines
                                     .Select(x => new
                                     {
                                         ItemCode = x.ItemCode,
                                         Quantity = x.Quantity,
                                         WarehouseCode = x.WhsCode,
                                         LineNum = x.LineNum,
                                         UoMEntry = x.UoMEntry,
                                         Price = x.Price,
                                         TaxCode = x.TaxCode,
                                         PriceAfterVAT = x.PriceAfterVAT
                                     }).ToList()
                };

                var objQuotations = new SLBatchRequest(
                    HttpMethod.Post,
                    "Quotations",
                    ofertaVenta,
                    1); // Content-ID for this entity
                #endregion

                #region CREAR ORDEN DE VENTA, modelo a tomar
                var orderVenta = new
                {
                    CardCode = order.CardCode,
                    DocDueDate = order.DocDueDate,
                    PaymentGroupCode = order.PaymentGroupCode,
                    DocumentLines = order.DocumentLines
                                     .Select(x => new
                                     {
                                         BaseEntry = x.BaseEntry,
                                         BaseLine = x.BaseLine,
                                         BaseType = x.BaseType,
                                         WarehouseCode = x.WhsCode,
                                         TaxCode = x.TaxCode,
                                         PriceAfterVAT = x.PriceAfterVAT
                                     }).ToList()
                };

                var objOrders = new SLBatchRequest(
                    HttpMethod.Post,
                    "Orders",
                    orderVenta,
                    2);
                #endregion

                #region CREAR FACTURA VENTA, modelo a tomar
                var invoiceVenta = new
                {
                    CardCode = invoice.CardCode,
                    DocDueDate = invoice.DocDueDate,
                    PaymentGroupCode = invoice.PaymentGroupCode,
                    Indicator = 4,
                    Series = 142,
                    ReserveInvoice = "tYES",
                    DocType = "dDocument_Items",
                    WithholdingTaxDataCollection = invoice.WithholdingTaxDataCollection.Select(x => new
                    {
                        WTCode = x.WTCode,
                        WTAmount = x.WTAmount
                    }).ToList(),
                    DocumentLines = invoice.DocumentLines
                                     .Select(x => new
                                     {
                                         BaseEntry = x.BaseEntry,
                                         BaseLine = x.BaseLine,
                                         BaseType = x.BaseType,
                                         TaxCode = x.TaxCode,
                                         WarehouseCode = x.WhsCode,
                                         PriceAfterVAT = x.PriceAfterVAT,
                                         DocumentLineAdditionalExpense = x.DocumentLineAdditionalExpenses.Select(y => new
                                         {
                                             ExpenseCode = y.ExpenseCode,
                                             LineTotal = y.LineTotal,
                                         }).FirstOrDefault()
                                     }).ToList()
                };

                var objInvoice = new SLBatchRequest(
                    HttpMethod.Post,
                    "Invoices",
                    invoiceVenta,
                    3);
                #endregion

                #region CREAR PAGO
                var paymentVenta = new
                {
                    CardCode = payment.CardCode!,
                    DocDate = payment.DocDate,
                    DocType = payment.DocType,
                    ControlAccount = "220301004",
                    CashAccount = "110101008",
                    CashSum = payment.CashSum,
                    U_TipoPago = 2,
                    U_CajaControl = "110101008",
                    U_OrigenAplicacion = 1,
                    DocCurrency = "BS",
                    PaymentInvoices = new List<PaymentInvoice>()
                    {
                        new PaymentInvoice()
                        {
                            DocEntry= "$3",
                        }
                    }
                };

                var objPayment = new SLBatchRequest(
                    HttpMethod.Post,
                    "IncomingPayments",
                    paymentVenta,
                    4);
                #endregion

                #region CREAR ENTREGA
                var deliveryVenta = new
                {
                    CardCode = delivery.CardCode,
                    DocDueDate = delivery.DocDueDate,
                    PaymentGroupCode = delivery.PaymentGroupCode,
                    DocumentLines = delivery.DocumentLines
                                     .Select(x => new
                                     {
                                         BaseEntry = x.BaseEntry,
                                         BaseLine = x.BaseLine,
                                         BaseType = x.BaseType,
                                         WarehouseCode = x.WhsCode,
                                         TaxCode = x.TaxCode,
                                         PriceAfterVAT = x.PriceAfterVAT
                                     }).ToList()
                };

                var objDelivery = new SLBatchRequest(
                    HttpMethod.Post,
                    "DeliveryNotes",
                    deliveryVenta,
                    5);
                #endregion

                Tuple<HttpResponseMessage[], string> results = await _slConnections[login.SLConexion].PostBatchAsync(objQuotations, objOrders, objInvoice, objPayment, objDelivery);

                foreach (HttpResponseMessage resultItem in results.Item1)
                {
                    if (Convert.ToInt32(resultItem.StatusCode) == 201)
                    {
                        string urlLocation = resultItem.Headers.GetValues("Location").SingleOrDefault()!;
                        string docEntry = Functions.GetDocEntry(urlLocation!)!;

                        batchResponses.Add(new BatchResponse
                        {
                            StatusCode = Convert.ToInt32(resultItem.StatusCode),
                            //ContentID = Convert.ToInt32(resultItem.Headers.Select(x => x.Key == "").SingleOrDefault()),
                            Location = urlLocation,
                            DocEntry = Convert.ToInt32(docEntry),
                            Description = "ÉXITO"
                        });
                    }
                    else
                    {
                        batchResponses.Add(new BatchResponse
                        {
                            StatusCode = Convert.ToInt32(resultItem.StatusCode),
                            //ContentID = Convert.ToInt32(resultItem.Headers.GetValues("Content-ID").SingleOrDefault()),
                            Location = @"—Bien, Houston, hemos tenido un problema aquí.",
                            DocEntry = 0,
                            Description = results.Item2
                        });
                    }

                }
                return batchResponses;
            }
            catch (Exception ex)
            {
                //LogException.Write(ex);
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task<Document> CreateInvoiceAsync(Document invoice, Login login)
        {
            try
            {
                List<BatchResponse> batchResponses = new List<BatchResponse>();

                #region CREAR FACTURA VENTA, modelo a tomar
                var invoiceVenta = new
                {
                    CardCode = invoice.CardCode,
                    DocDueDate = invoice.DocDueDate,
                    PaymentGroupCode = invoice.PaymentGroupCode,
                    Indicator = 4,
                    Series = 142,
                    ReserveInvoice = "tYES",
                    DocType = "dDocument_Items",
                    WithholdingTaxDataCollection = invoice.WithholdingTaxDataCollection.Select(x => new
                    {
                        WTCode = x.WTCode,
                        WTAmount = x.WTAmount
                    }).ToList(),
                    DocumentLines = invoice.DocumentLines
                                     .Select(x => new
                                     {
                                         BaseEntry = x.BaseEntry,
                                         BaseLine = x.BaseLine,
                                         BaseType = x.BaseType,
                                         TaxCode = x.TaxCode,
                                         WarehouseCode = x.WhsCode,
                                         PriceAfterVAT = x.PriceAfterVAT,
                                         DocumentLineAdditionalExpense = x.DocumentLineAdditionalExpenses.Select(y => new
                                         {
                                             ExpenseCode = y.ExpenseCode,
                                             LineTotal = y.LineTotal,
                                         }).FirstOrDefault()
                                     }).ToList()
                };
                #endregion

                Document results = await _slConnections[login.SLConexion].Request("Invoices")
                                                                         .PostAsync<Document>(invoiceVenta);

                return results;

            }
            catch (Exception ex)
            {
                //LogException.Write(ex);
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        //public async Task<List<BatchResponse>> CreateOfertaOrderFacturaAsync(Document oferta, Document order, Document invoice)
        //{
        //    try
        //    {
        //        List<BatchResponse> batchResponses = new List<BatchResponse>();

        //        //OFERTA DE VENTA
        //        var objQuotations = new SLBatchRequest(
        //            HttpMethod.Post,
        //            "Quotations",
        //            oferta,
        //            1); // Content-ID for this entity

        //        //ORDEN DE VENTA
        //        var objOrders = new SLBatchRequest(
        //            HttpMethod.Post,
        //            "Orders",
        //            order,
        //            2);

        //        //FACTURA
        //        var objInvoices = new SLBatchRequest(
        //            HttpMethod.Post,
        //            "Invoices",
        //            order,
        //            3);

        //        HttpResponseMessage[] results = await _serviceLayer.PostBatchAsync(objQuotations, objOrders, objInvoices);

        //        foreach (HttpResponseMessage resultItem in results)
        //        {
        //            if (Convert.ToInt32(resultItem.StatusCode) == 201)
        //            {
        //                string urlLocation = resultItem.Headers.GetValues("Location").SingleOrDefault()!;
        //                string docEntry = Functions.GetDocEntry(urlLocation!)!;

        //                batchResponses.Add(new BatchResponse
        //                {
        //                    StatusCode = Convert.ToInt32(resultItem.StatusCode),
        //                    ContentID = Convert.ToInt32(resultItem.Headers.GetValues("Content-ID").SingleOrDefault()),
        //                    Location = urlLocation,
        //                    DocEntry = Convert.ToInt32(docEntry)
        //                });
        //            }
        //            else
        //            {
        //                batchResponses.Add(new BatchResponse
        //                {
        //                    StatusCode = Convert.ToInt32(resultItem.StatusCode),
        //                    ContentID = Convert.ToInt32(resultItem.Headers.GetValues("Content-ID").SingleOrDefault()),
        //                    Location = @"—Bien, Houston, hemos tenido un problema aquí.",
        //                    DocEntry = 0
        //                });
        //            }

        //        }
        //        return batchResponses;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException.Write(ex);
        //        throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
        //    }
        //}

    }
}
