using System;
using Eleven.Service.Modelo.Eleven;
using Eleven.Service.Modelo.SAP;
using Eleven.Service.Repositorio.Interfaz;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SAPBo.Data.Entidad;
using SAPBo.Data.Repositorio.Interfaz;
using Utils.Helper;

namespace Eleven.Service.Repositorio.Implementacion
{
    public class DocumentService : IDocumentService
    {
        IBoDocumentRepository _boDocumentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentService> _logger;
        public DocumentService(IBoDocumentRepository boDocumentRepository, IMapper mapper, ILogger<DocumentService> logger) 
        { 
            _boDocumentRepository = boDocumentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<MdoBatchResponse>> CreateOfertaOrderAsync(MdoDocument modelo, MdoSLConexion boLogin)
        {
            try
            {
                int intQuotation = Convert.ToInt32(DocumentType.TipoDocumento.QUOTATIONS);
                const decimal IT = 0.03M;

                List<DocumentLine> ofLines = new List<DocumentLine>();
                List<DocumentLine> ovLines = new List<DocumentLine>();

                Login login = new Login()
                {
                    SLConexion = boLogin.SLConexion,
                    UserName = boLogin.UserName
                };

                foreach (MdoDocumentLine item in modelo.DocumentLines)
                {
                    DocumentLine ofertaVentaLine = new DocumentLine()
                    {
                        ItemCode = item.ItemCode!,
                        Quantity = item.Quantity,
                        WhsCode = item.WhsCode,
                        LineNum = item.LineNum
                    };
                    ofLines.Add(ofertaVentaLine);
                }

                Document ofertaVenta = new Document()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    DocumentLines = ofLines
                };

                #region CREAR ORDEN DE VENTA
                //este funciona en factura
                foreach (MdoDocumentLine item in modelo.DocumentLines)
                {
                    decimal precioTotal = Math.Round((decimal)(item.Quantity * item.Price!)! - (decimal)item.Discount, 2, MidpointRounding.AwayFromZero);
                    decimal lineTotal = item.Quantity == -1 ? Math.Round((decimal)(precioTotal * IT * -1), 2, MidpointRounding.AwayFromZero) : Math.Round((decimal)(precioTotal * IT), 2, MidpointRounding.AwayFromZero);
                    decimal descuentoUnitario = 0;
                    decimal descuentoUnitarioPorcentaje = 0;
                    decimal precioAfterVAT = 0;
                    if (item.RequiereSerie && item.Quantity > 1)
                    {
                        // esto ocurre cuando se debe de registrar numero de serie por item 
                        // entonces, se debe de extender la cantidad a uno y controlar el descuento
                    }
                    else
                    {
                        if (item.Discount > 0)
                        {
                            // se redondea a 6 decimales, porque en SAP esta definido 6 digitos decimales para el campo de discPrcnt
                            descuentoUnitarioPorcentaje = Math.Round(Convert.ToDecimal(((decimal)item.Discount * 100 / (decimal)(item.Quantity * item.Price!)!)), 6);
                            descuentoUnitario = Math.Round((descuentoUnitarioPorcentaje * (decimal)item.Price) / 100, 2, MidpointRounding.AwayFromZero);
                            precioAfterVAT = (decimal)item.Price! - descuentoUnitario;
                        }
                        else
                        {
                            precioAfterVAT = (decimal)item.Price!;
                        }
                    }

                    DocumentLine ordenVentaLine = new DocumentLine()
                    {
                        BaseEntry = "$1",
                        BaseLine = item.LineNum,
                        ItemCode = item.ItemCode!,
                        Quantity = item.Quantity,
                        WhsCode = item.WhsCode,
                        LineNum = item.LineNum,
                        UoMEntry = item.UoMEntry,
                        BaseType = intQuotation,
                        TaxCode = item.TaxCode,
                        Price = item.Price,
                        U_PrecioBase = item.Price,
                        U_PrecioDescontado = (double)precioTotal,
                        U_DescuentoPOS = item.Discount,
                        PriceAfterVAT = (double)precioAfterVAT,
                        DiscountPercent = (double)descuentoUnitarioPorcentaje,
                        DocumentLineAdditionalExpenses = new List<DocumentLineAdditionalExpense>()
                        {
                            new DocumentLineAdditionalExpense()
                            {
                                ExpenseCode = 1,
                                LineTotal = lineTotal
                            }
                        },
                    };
                    ovLines.Add(ordenVentaLine);
                }

                Document ordenVenta = new Document()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    PaymentGroupCode = -1,
                    DocumentLines = ovLines,
                    WithholdingTaxDataCollection = new List<WithholdingTaxData>()
                    {
                        new WithholdingTaxData()
                        {
                            WTAmount = ovLines.Sum(x=> x.DocumentLineAdditionalExpenses.Sum(y => y.LineTotal)),
                        }
                    }
                };
                decimal diferencia = (decimal)modelo.DocumentLines.Sum(x => (x.Quantity * x.Price) - x.Discount) - (decimal)ordenVenta.DocumentLines.Sum(x => x.PriceAfterVAT * x.Quantity);
                if (diferencia != 0)
                {
                    balanceadorDelMontoDetalle(ref ordenVenta, diferencia);
                }
                #endregion

                List<BatchResponse> results = await _boDocumentRepository.CreateOfertaOrderAsync(ofertaVenta, ordenVenta, login);

                List<MdoBatchResponse> boBatchResponses = new List<MdoBatchResponse>();
                results.ForEach(x =>
                {
                    MdoBatchResponse boBatchResponse = new MdoBatchResponse()
                    {
                        ContentID = x.ContentID,
                        DocEntry = x.DocEntry,
                        Location = x.Location,
                        StatusCode = x.StatusCode,
                        Description = x.Description,
                    };
                    boBatchResponses.Add(boBatchResponse);
                });

                return boBatchResponses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateOfertaOrderAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }

        }

        public async Task<List<MdoBatchResponse>> CreateOfertaOrderFacturaAsync(MdoDocument modelo, MdoSLConexion boLogin)
        {
            try
            {
                int intQuotation = Convert.ToInt32(DocumentType.TipoDocumento.QUOTATIONS);
                int intOrder = Convert.ToInt32(DocumentType.TipoDocumento.ORDER);
                const decimal IT = 0.03M;

                List<DocumentLine> ofLines = new List<DocumentLine>();
                List<DocumentLine> ovLines = new List<DocumentLine>();
                List<DocumentLine> faLines = new List<DocumentLine>();

                Login login = new Login()
                {
                    SLConexion = boLogin.SLConexion,
                    UserName = boLogin.UserName
                };

                #region CREAR OFERTA DE VENTA
                foreach (MdoDocumentLine item in modelo.DocumentLines)
                {
                    DocumentLine ofertaVentaLine = new DocumentLine()
                    {
                        ItemCode = item.ItemCode!,
                        Quantity = item.Quantity,
                        WhsCode = item.WhsCode,
                        LineNum = item.LineNum,
                        UoMEntry = item.UoMEntry,
                        Price = item.Price,
                        PriceAfterVAT = item.PriceAfterVAT,
                        TaxCode = item.TaxCode,
                    };
                    ofLines.Add(ofertaVentaLine);
                }

                Document ofertaVenta = new Document()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    DocumentLines = ofLines
                };
                #endregion

                #region CREAR ORDEN DE VENTA
                foreach (DocumentLine item in ofLines)
                {
                    DocumentLine ordenVentaLine = new DocumentLine()
                    {
                        BaseEntry = "$1",
                        BaseLine = item.LineNum,
                        BaseType = intQuotation,
                        WhsCode = item.WhsCode,
                        TaxCode = item.TaxCode,
                        PriceAfterVAT = item.PriceAfterVAT
                    };
                    ovLines.Add(ordenVentaLine);
                }

                Document ordenVenta = new Document()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    PaymentGroupCode = -1,
                    DocumentLines = ovLines
                };
                #endregion

                #region CREAR FACTURA DE VENTA
                foreach (DocumentLine item in ofLines)
                {
                    decimal precioTotal = Math.Round((decimal)(item.Quantity * item.Price!)!, 2, MidpointRounding.AwayFromZero);
                    DocumentLine facturaVentaLine = new DocumentLine()
                    {
                        BaseEntry = "$2",
                        BaseLine = item.LineNum,
                        BaseType = intOrder,
                        WhsCode = item.WhsCode,
                        TaxCode = item.TaxCode,
                        PriceAfterVAT = item.PriceAfterVAT,
                        DocumentLineAdditionalExpenses = new List<DocumentLineAdditionalExpense>()
                        {
                            new DocumentLineAdditionalExpense()
                            {
                                ExpenseCode = 1,
                                LineTotal = item.Quantity == -1 ? Math.Round((precioTotal * IT * -1), 2, MidpointRounding.AwayFromZero) : Math.Round(precioTotal * IT, 2, MidpointRounding.AwayFromZero)
                            }
                        },
                    };
                    faLines.Add(facturaVentaLine);
                }

                Document facturaVenta = new Document()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    PaymentGroupCode = -1,
                    Indicator = 4, //Factura cliente
                    DocumentLines = faLines,
                    WithholdingTaxDataCollection = new List<WithholdingTaxData>()
                    {
                        new WithholdingTaxData()
                        {
                            WTAmount = faLines.Sum(x=> x.DocumentLineAdditionalExpenses.Sum(y => y.LineTotal)),
                        }
                    }
                };
                #endregion

                List<BatchResponse> results = await _boDocumentRepository.CreateOfertaOrderInvoiceAsync(ofertaVenta, ordenVenta, facturaVenta, login);

                List<MdoBatchResponse> boBatchResponses = new List<MdoBatchResponse>();
                results.ForEach(x =>
                {
                    MdoBatchResponse boBatchResponse = new MdoBatchResponse()
                    {
                        ContentID = x.ContentID,
                        DocEntry = x.DocEntry,
                        Location = x.Location,
                        StatusCode = x.StatusCode,
                        Description = x.Description,
                    };
                    boBatchResponses.Add(boBatchResponse);
                });

                return boBatchResponses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateOfertaOrderFacturaAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task<List<MdoBatchResponse>> CreateOfertaOrderFacturaEntregaAsync(MdoDocument modelo, MdoSLConexion boLogin)
        {
            try
            {
                int intQuotation = Convert.ToInt32(DocumentType.TipoDocumento.QUOTATIONS);
                int intOrder = Convert.ToInt32(DocumentType.TipoDocumento.ORDER);
                int intInvoice = Convert.ToInt32(DocumentType.TipoDocumento.INVOICE);
                const decimal IT = 0.03M;

                List<DocumentLine> ofLines = new List<DocumentLine>();
                List<DocumentLine> ovLines = new List<DocumentLine>();
                List<DocumentLine> faLines = new List<DocumentLine>();
                List<DocumentLine> enLines = new List<DocumentLine>();

                Login login = new Login()
                {
                    SLConexion = boLogin.SLConexion,
                    UserName = boLogin.UserName
                };

                #region CREAR OFERTA DE VENTA
                foreach (MdoDocumentLine item in modelo.DocumentLines)
                {
                    DocumentLine ofertaVentaLine = new DocumentLine()
                    {
                        ItemCode = item.ItemCode!,
                        Quantity = item.Quantity,
                        WhsCode = item.WhsCode,
                        LineNum = item.LineNum,
                        UoMEntry = item.UoMEntry,
                        Price = item.Price,
                        PriceAfterVAT = item.PriceAfterVAT,
                        TaxCode = item.TaxCode,
                        Discount = item.Discount
                    };
                    ofLines.Add(ofertaVentaLine);
                }

                Document ofertaVenta = new Document()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    DocumentLines = ofLines
                };
                #endregion

                #region CREAR ORDEN DE VENTA
                foreach (MdoDocumentLine item in modelo.DocumentLines)
                {
                    decimal precioTotal = Math.Round((decimal)(item.Quantity * item.Price!)! - (decimal)item.Discount, 2, MidpointRounding.AwayFromZero);
                    decimal lineTotal = item.Quantity == -1 ? Math.Round((decimal)(precioTotal * IT * -1), 2, MidpointRounding.AwayFromZero) : Math.Round((decimal)(precioTotal * IT), 2, MidpointRounding.AwayFromZero);
                    decimal descuentoUnitario = 0;
                    decimal descuentoUnitarioPorcentaje = 0;
                    decimal precioAfterVAT = 0;
                    if (item.RequiereSerie && item.Quantity > 1)
                    {
                        // esto ocurre cuando se debe de registrar numero de serie por item 
                        // entonces, se debe de extender la cantidad a uno y controlar el descuento
                    }
                    else
                    {
                        if (item.Discount > 0)
                        {
                            // se redondea a 6 decimales, porque en SAP esta definido 6 digitos decimales para el campo de discPrcnt
                            descuentoUnitarioPorcentaje = Math.Round(Convert.ToDecimal(((decimal)item.Discount * 100 / (decimal)(item.Quantity * item.Price!)!)), 6);
                            descuentoUnitario = Math.Round((descuentoUnitarioPorcentaje * (decimal)item.Price) / 100, 2, MidpointRounding.AwayFromZero);
                            precioAfterVAT = (decimal)item.Price! - descuentoUnitario;
                        }
                        else
                        {
                            precioAfterVAT = (decimal)item.Price!;
                        }
                    }

                    DocumentLine ordenVentaLine = new DocumentLine()
                    {
                        BaseEntry = "$1",
                        FreeText = "CONTADO",
                        BackOrder = "tYES",
                        BaseLine = item.LineNum,
                        ItemCode = item.ItemCode!,
                        Quantity = item.Quantity,
                        WhsCode = item.WhsCode,
                        LineNum = item.LineNum,
                        UoMEntry = item.UoMEntry,
                        BaseType = intQuotation,
                        TaxCode = item.TaxCode,
                        Price = item.Price,
                        U_PrecioBase = item.Price,
                        U_PrecioDescontado = (double)precioTotal,
                        U_DescuentoPOS = item.Discount,
                        PriceAfterVAT = (double)precioAfterVAT,
                        DiscountPercent = (double)descuentoUnitarioPorcentaje,
                        DocumentLineAdditionalExpenses = new List<DocumentLineAdditionalExpense>()
                        {
                            new DocumentLineAdditionalExpense()
                            {
                                ExpenseCode = 1,
                                LineTotal = lineTotal
                            }
                        },
                    };
                    ovLines.Add(ordenVentaLine);
                }

                Document ordenVenta = new Document()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    PaymentGroupCode = -1,
                    Indicator = 4,
                    Series = 142,
                    ReserveInvoice = "tYES",
                    DocType = "dDocument_Items",
                    U_LB_TipoTransaccion = 1,
                    U_LB_ModalidadTransa = 1,
                    U_LB_Bancarizacion = "N",
                    DocumentLines = ovLines,
                    WithholdingTaxDataCollection = new List<WithholdingTaxData>()
                    {
                        new WithholdingTaxData()
                        {
                            WTAmount = ovLines.Sum(x=> x.DocumentLineAdditionalExpenses.Sum(y => y.LineTotal)),
                        }
                    }
                };
                decimal diferencia = (decimal)modelo.DocumentLines.Sum(x => (x.Quantity * x.Price) - x.Discount)! - (decimal)ordenVenta.DocumentLines.Sum(x => x.PriceAfterVAT * x.Quantity)!;
                if (diferencia != 0)
                {
                    balanceadorDelMontoDetalle(ref ordenVenta, Math.Round(diferencia, 2, MidpointRounding.AwayFromZero));
                }
                #endregion

                #region CREAR FACTURA DE VENTA
                foreach (DocumentLine item in ordenVenta.DocumentLines)
                {
                    DocumentLine facturaVentaLine = new DocumentLine()
                    {
                        BaseEntry = "$2",
                        FreeText = item.FreeText,
                        BackOrder = item.BackOrder,
                        BaseLine = item.LineNum,
                        BaseType = intOrder,
                        ItemCode = item.ItemCode!,
                        Quantity = item.Quantity,
                        WhsCode = item.WhsCode,
                        LineNum = item.LineNum,
                        UoMEntry = item.UoMEntry,
                        TaxCode = item.TaxCode,
                        Price = item.Price,
                        U_PrecioBase = item.U_PrecioBase,
                        U_PrecioDescontado = item.U_PrecioDescontado,
                        U_DescuentoPOS = item.U_DescuentoPOS,
                        PriceAfterVAT = item.PriceAfterVAT,
                        DiscountPercent = item.DiscountPercent,
                        DocumentLineAdditionalExpenses = item.DocumentLineAdditionalExpenses.Select(x => new DocumentLineAdditionalExpense 
                                                                                                { 
                                                                                                    ExpenseCode= x.ExpenseCode,
                                                                                                    LineTotal = x.LineTotal
                                                                                                }).ToList(),
                    };
                    faLines.Add(facturaVentaLine);
                }

                Document facturaVenta = new Document()
                {
                    CardCode = ordenVenta.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    PaymentGroupCode = -1,
                    Indicator = 4, //Factura cliente
                    Series = ordenVenta.Series,
                    ReserveInvoice = "tYES",
                    DocType = "dDocument_Items",
                    U_LB_TipoTransaccion = 1,
                    U_LB_ModalidadTransa = 1,
                    U_LB_Bancarizacion = "N",
                    DocumentLines = faLines,
                    WithholdingTaxDataCollection = ordenVenta.WithholdingTaxDataCollection.Select(x => new WithholdingTaxData
                    {
                        WTCode= "IT",
                        WTAmount = x.WTAmount,
                    }).ToList()
                };
                #endregion

                #region CREAR ENTREGA
                foreach (DocumentLine item in faLines)
                {
                    DocumentLine entregaVentaLine = new DocumentLine()
                    {
                        BaseEntry = "$3",
                        BaseLine = item.BaseLine,
                        BaseType = intInvoice,
                        WhsCode = item.WhsCode,
                        TaxCode = item.TaxCode,
                    };
                    enLines.Add(entregaVentaLine);
                }

                Document entregaVenta = new Document()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    PaymentGroupCode = -1,
                    DocumentLines = enLines
                };
                #endregion

                List<BatchResponse> results = await _boDocumentRepository.CreateOfertaOrderInvoiceDeliveryAsync(ofertaVenta, ordenVenta, facturaVenta, entregaVenta, login);

                List<MdoBatchResponse> boBatchResponses = new List<MdoBatchResponse>();
                results.ForEach(x =>
                {
                    MdoBatchResponse boBatchResponse = new MdoBatchResponse()
                    {
                        ContentID = x.ContentID,
                        DocEntry = x.DocEntry,
                        Location = x.Location,
                        StatusCode = x.StatusCode,
                        Description = x.Description,
                    };
                    boBatchResponses.Add(boBatchResponse);
                });

                return boBatchResponses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateOfertaOrderFacturaEntregaAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        private void balanceadorDelMontoDetalle(ref Document eDocumento, decimal diferencia)
        {
            decimal IT = 0.03M;
            if (eDocumento.DocumentLines.Exists(x => x.Quantity == 1))
            {
                DocumentLine line = eDocumento.DocumentLines.Where(x => x.Quantity == 1).OrderByDescending(x => x.PriceAfterVAT * x.Quantity).FirstOrDefault()!;
                line.PriceAfterVAT += (double)diferencia;
            }
            else
            {
                DocumentLine line = eDocumento.DocumentLines.OrderByDescending(x => x.PriceAfterVAT * x.Quantity).FirstOrDefault()!;
                line.Quantity -= 1;

                //ENTIDADES.VentaDetalle detalleBase = eVenta.VentaDetalle.Find(x => x.ProductoCodigo.Equals(line.ItemCode));
                DocumentLine newLine = new DocumentLine
                {
                    LineNum = eDocumento.DocumentLines.Max(x => x.LineNum) + 1,

                    ItemCode = line.ItemCode,
                    ItemDescription = line.ItemDescription,
                    Quantity = 1,
                    //WarehouseCode = line.WarehouseCode,
                    UoMEntry = line.UoMEntry,
                    PriceAfterVAT = line.PriceAfterVAT + (double)diferencia,

                    //U_PrecioDescontado = (detalleBase.PrecioTotal / Math.Abs(detalleBase.Cantidad)).ToRedondear(2).ToDouble(),
                    //U_PrecioBase = detalleBase.PrecioBase.ToDouble(),
                    //U_ClaCom = detalleBase.Clacom,
                    //FreeText = CondicionVenta.CONTADO.ToString(),
                    EntregaAutomatica = line.EntregaAutomatica,

                    //SIGNACION DE CENTROS DE COSTO PARA TODOS LOS FLUJOS
                    CostingCode = line.CostingCode,
                    CostingCode2 = line.CostingCode2,
                    CostingCode3 = line.CostingCode3,

                    COGSCostingCode = line.COGSCostingCode,
                    COGSCostingCode2 = line.COGSCostingCode2,
                    COGSCostingCode3 = line.COGSCostingCode3
                };

                eDocumento.DocumentLines.Add(newLine);
            }

            #region RECALCULO IMPUESTOS
            foreach (var item in eDocumento.DocumentLines)
            {
                var expenses = item.DocumentLineAdditionalExpenses.FirstOrDefault();
                if (expenses != null)
                    expenses.LineTotal = Math.Round((decimal)(item.PriceAfterVAT * item.Quantity) * IT, 2, MidpointRounding.AwayFromZero);
                else
                    item.DocumentLineAdditionalExpenses.Add(new DocumentLineAdditionalExpense
                    {
                        ExpenseCode = 1,
                        LineTotal = Math.Round((decimal)(item.PriceAfterVAT * item.Quantity) * IT, 2, MidpointRounding.AwayFromZero)
                    });
            }

            eDocumento.WithholdingTaxDataCollection.FirstOrDefault().WTAmount = eDocumento.DocumentLines.Sum(l => l.DocumentLineAdditionalExpenses.Sum(i => i.LineTotal));
            #endregion
        }

        public async Task<List<MdoBatchResponse>> CreateOfertaOrderFacturaPagoEntregaAsync(MdoDocument modelo, MdoSLConexion boLogin)
        {
            // en este caso se crea una reconciliación automanica por SAP, en este caso no se ve la reconciliación 
            try
            {
                int intQuotation = Convert.ToInt32(DocumentType.TipoDocumento.QUOTATIONS);
                int intOrder = Convert.ToInt32(DocumentType.TipoDocumento.ORDER);
                int intInvoice = Convert.ToInt32(DocumentType.TipoDocumento.INVOICE);
                const decimal IT = 0.03M;

                List<DocumentLine> ofLines = new List<DocumentLine>();
                List<DocumentLine> ovLines = new List<DocumentLine>();
                List<DocumentLine> faLines = new List<DocumentLine>();
                List<DocumentLine> enLines = new List<DocumentLine>();

                Login login = new Login()
                {
                    SLConexion = boLogin.SLConexion,
                    UserName = boLogin.UserName
                };

                #region CREAR OFERTA DE VENTA
                foreach (MdoDocumentLine item in modelo.DocumentLines)
                {
                    DocumentLine ofertaVentaLine = new DocumentLine()
                    {
                        ItemCode = item.ItemCode!,
                        Quantity = item.Quantity,
                        WhsCode = item.WhsCode,
                        LineNum = item.LineNum,
                        UoMEntry = item.UoMEntry,
                        Price = item.Price,
                        PriceAfterVAT = item.PriceAfterVAT,
                        TaxCode = item.TaxCode,
                    };
                    ofLines.Add(ofertaVentaLine);
                }

                Document ofertaVenta = new Document()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    DocumentLines = ofLines
                };
                #endregion

                #region CREAR ORDEN DE VENTA
                foreach (DocumentLine item in ofLines)
                {
                    DocumentLine ordenVentaLine = new DocumentLine()
                    {
                        BaseEntry = "$1",
                        BaseLine = item.LineNum,
                        BaseType = intQuotation,
                        WhsCode = item.WhsCode,
                        TaxCode = item.TaxCode,
                        PriceAfterVAT = item.PriceAfterVAT
                    };
                    ovLines.Add(ordenVentaLine);
                }

                Document ordenVenta = new Document()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    PaymentGroupCode = -1,
                    DocumentLines = ovLines
                };
                #endregion

                #region CREAR FACTURA DE VENTA
                decimal sumTotal = 0;
                foreach (DocumentLine item in ofLines)
                {
                    decimal precioTotal = Math.Round((decimal)(item.Quantity * item.Price!)!, 2, MidpointRounding.AwayFromZero);
                    decimal lineTotal = item.Quantity == -1 ? Math.Round((precioTotal * IT * -1), 2, MidpointRounding.AwayFromZero) : Math.Round(precioTotal * IT, 2, MidpointRounding.AwayFromZero);
                    sumTotal += precioTotal;
                    DocumentLine facturaVentaLine = new DocumentLine()
                    {
                        BaseEntry = "$2",
                        BaseLine = item.LineNum,
                        BaseType = intOrder,
                        WhsCode = item.WhsCode,
                        TaxCode = item.TaxCode,
                        PriceAfterVAT = item.PriceAfterVAT,
                        DocumentLineAdditionalExpenses = new List<DocumentLineAdditionalExpense>()
                        {
                            new DocumentLineAdditionalExpense()
                            {
                                ExpenseCode = 1,
                                LineTotal = lineTotal
                            }
                        },
                    };
                    faLines.Add(facturaVentaLine);
                }

                Document facturaVenta = new Document()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    PaymentGroupCode = -1,
                    Indicator = 4, //Factura cliente
                    DocumentLines = faLines,
                    WithholdingTaxDataCollection = new List<WithholdingTaxData>()
                    {
                        new WithholdingTaxData()
                        {
                            WTAmount = faLines.Sum(x=> x.DocumentLineAdditionalExpenses.Sum(y => y.LineTotal)),
                        }
                    }
                };
                #endregion

                #region CREAR PAGO VENTA
                Payment pagoVenta = new Payment()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocType = "rCustomer",
                    ControlAccount = "220301004",
                    CashAccount = "110101008",
                    CashSum = (double)sumTotal,
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
                #endregion

                #region CREAR ENTREGA
                foreach (DocumentLine item in ofLines)
                {
                    DocumentLine entregaVentaLine = new DocumentLine()
                    {
                        BaseEntry = "$3",
                        BaseLine = item.LineNum,
                        BaseType = intInvoice,
                        WhsCode = item.WhsCode,
                        TaxCode = item.TaxCode,
                        PriceAfterVAT = item.PriceAfterVAT
                    };
                    enLines.Add(entregaVentaLine);
                }

                Document entregaVenta = new Document()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    PaymentGroupCode = -1,
                    DocumentLines = enLines
                };
                #endregion

                List<BatchResponse> results = await _boDocumentRepository.CreateOfertaOrderInvoicePaymentDeliveryAsync(ofertaVenta, ordenVenta, facturaVenta, pagoVenta, entregaVenta, login);

                List<MdoBatchResponse> boBatchResponses = new List<MdoBatchResponse>();
                results.ForEach(x =>
                {
                    MdoBatchResponse boBatchResponse = new MdoBatchResponse()
                    {
                        ContentID = x.ContentID,
                        DocEntry = x.DocEntry,
                        Location = x.Location,
                        StatusCode = x.StatusCode,
                        Description = x.Description,
                    };
                    boBatchResponses.Add(boBatchResponse);
                });

                return boBatchResponses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateOfertaOrderFacturaPagoEntregaAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task<MdoDocument> CrearFacturaAsync(MdoDocument modelo, MdoSLConexion boLogin)
        {
            try
            {
                int intQuotation = Convert.ToInt32(DocumentType.TipoDocumento.QUOTATIONS);
                int intOrder = Convert.ToInt32(DocumentType.TipoDocumento.ORDER);
                const decimal IT = 0.03M;

                List<DocumentLine> faLines = new List<DocumentLine>();

                Login login = new Login()
                {
                    SLConexion = boLogin.SLConexion,
                    UserName = boLogin.UserName
                };

                foreach (MdoDocumentLine item in modelo.DocumentLines)
                {
                    DocumentLine facturaVentaLine = new DocumentLine()
                    {
                        ItemCode = item.ItemCode!,
                        Quantity = item.Quantity,
                        WhsCode = item.WhsCode,
                        TaxCode = item.TaxCode,
                        Price = item.Price,
                        PriceAfterVAT = item.PriceAfterVAT,
                        DocumentLineAdditionalExpenses = new List<DocumentLineAdditionalExpense>() {
                            new DocumentLineAdditionalExpense()
                            {
                                ExpenseCode = 1,
                                LineTotal = item.Quantity == -1 ? Math.Round(((decimal)(item.Quantity * item.Price!) * IT * -1), 2, MidpointRounding.AwayFromZero) : Math.Round((decimal)(item.Quantity! * item.Price!) * IT, 2, MidpointRounding.AwayFromZero)
                            }
                        },
                    };
                    faLines.Add(facturaVentaLine);
                }

                Document facturaVenta = new Document()
                {
                    CardCode = modelo.CardCode!,
                    DocDate = DateTime.Now,
                    DocDueDate = DateTime.Now,
                    PaymentGroupCode = -1,
                    Indicator = 4, //Factura cliente
                    DocumentLines = faLines,
                };

                Document document = await _boDocumentRepository.CreateInvoiceAsync(facturaVenta, login);

                return _mapper.Map<MdoDocument>(document);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CrearFacturaAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }
        //public async Task<List<BoBatchResponse>> CreateOfertaOrderFacturaAsync(BoDocument modelo)
        //{
        //    try
        //    {
        //        int intQuotation = Convert.ToInt32(Helper.TipoDocumento.QUOTATIONS);
        //        int intLine = 0;
        //        List<DocumentLine> ofLines = new List<DocumentLine>();
        //        List<DocumentLine> ovLines = new List<DocumentLine>();
        //        List<DocumentLine> ivLines = new List<DocumentLine>();

        //        #region CREAR OFERTA DE VENTA
        //        foreach (BoDocumentLine item in modelo.DocumentLines)
        //        {
        //            DocumentLine ofertaVentaLine = new DocumentLine()
        //            {
        //                ItemCode = item.ItemCode!,
        //                Quantity = item.Quantity,
        //                WhsCode = item.WhsCode,
        //                LineNum = intLine,
        //            };
        //            ofLines.Add(ofertaVentaLine);
        //        }

        //        Document ofertaVenta = new Document()
        //        {
        //            CardCode = modelo.CardCode!,
        //            DocDate = DateTime.Now,
        //            DocDueDate = DateTime.Now,
        //            DocumentLines = ofLines
        //        };
        //        #endregion

        //        #region CREAR ORDEN DE VENTA
        //        foreach (DocumentLine item in ofLines)
        //        {
        //            DocumentLine ordenVentaLine = new DocumentLine()
        //            {
        //                BaseEntry = "$1",
        //                BaseLine = item.LineNum,
        //                BaseType = intQuotation
        //            };
        //            ovLines.Add(ordenVentaLine);
        //        }

        //        Document ordenVenta = new Document()
        //        {
        //            CardCode = modelo.CardCode!,
        //            DocDate = DateTime.Now,
        //            DocDueDate = DateTime.Now,
        //            PaymentGroupCode = -1,
        //            DocumentLines = ovLines
        //        };
        //        #endregion

        //        #region CREAR FACTURA DE RESERVA
        //        foreach (DocumentLine item in ovLines)
        //        {
        //            DocumentLine facturaReservaLine = new DocumentLine()
        //            {
        //                BaseEntry = "$2",
        //                BaseLine = item.LineNum,
        //                BaseType = intQuotation
        //            };
        //            ivLines.Add(facturaReservaLine);
        //        }

        //        Document facturaReserva = new Document()
        //        {
        //            CardCode = modelo.CardCode!,
        //            DocDate = DateTime.Now,
        //            DocDueDate = DateTime.Now,
        //            PaymentGroupCode = -1,
        //            DocumentLines = ivLines
        //        };
        //        #endregion
        //        List<BatchResponse> results = await _boDocumentRepository.CreateOfertaOrderFacturaAsync(ofertaVenta, ordenVenta, facturaReserva);

        //        List<BoBatchResponse> boBatchResponses = new List<BoBatchResponse>();
        //        results.ForEach(x =>
        //        {
        //            BoBatchResponse boBatchResponse = new BoBatchResponse()
        //            {
        //                ContentID = x.ContentID,
        //                DocEntry = x.DocEntry,
        //                Location = x.Location,
        //                StatusCode = x.StatusCode,
        //            };
        //            boBatchResponses.Add(boBatchResponse);
        //        });

        //        return boBatchResponses;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException.Write(ex);
        //        throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
        //    }

        //}

    }
}
