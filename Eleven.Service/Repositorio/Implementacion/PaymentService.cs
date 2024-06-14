using Eleven.Service.Modelo.SAP;
using Eleven.Service.Repositorio.Interfaz;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SAPBo.Data.Entidad;
using SAPBo.Data.Repositorio.Interfaz;

namespace Eleven.Service.Repositorio.Implementacion
{
    public class PaymentService: IPaymentService
    {
        private readonly IBoPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;
        public PaymentService(IBoPaymentRepository paymentRepository, IMapper mapper, ILogger<EmployeeService> logger) 
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MdoPayment> CreatePaymentAsync(MdoPayment modelo, MdoSLConexion boLogin)
        {
            try
            {
                Login login = new Login()
                {
                    SLConexion = boLogin.SLConexion,
                    UserName = boLogin.UserName
                };

                Payment payment = new Payment()
                {
                    DocType = modelo.DocType,
                    CardCode = modelo.CardCode,
                    ControlAccount = modelo.ControlAccount,
                    CashAccount= modelo.CashAccount,
                    CashSum= modelo.CashSum,
                    U_TipoPago = modelo.U_TipoPago,
                    U_CajaControl = modelo.U_CajaControl,
                    U_OrigenAplicacion = modelo.U_OrigenAplicacion,
                    DocCurrency = modelo.DocCurrency,    
                    PaymentInvoices = modelo.PaymentInvoices.Select(x => new PaymentInvoice {
                        DocEntry = x.DocEntry,
                        InstallmentId = x.InstallmentId,
                        InvoiceType = x.InvoiceType,
                        LineNum = x.LineNum,
                        SumApplied = x.SumApplied
                    }).ToList(),
                    PaymentCreditCards = modelo.PaymentCreditCards.Select(x => new PaymentCreditCard { 
                        CardValidUntil = x.CardValidUntil,
                        CreditAcct = x.CreditAcct,
                        CreditCard = x.CreditCard,
                        CreditCardNumber = x.CreditCardNumber,
                        CreditSum = x.CreditSum,
                        LineNum= x.LineNum,
                        OwnerIdNum= x.OwnerIdNum,
                        OwnerPhone = x.OwnerPhone,
                        PaymentMethodCode= x.PaymentMethodCode,
                        VoucherNum = x.VoucherNum 
                    }).ToList()
                };

                Payment resPayment = await _paymentRepository.CreatePaymentAsync(payment, login);
                return _mapper.Map<MdoPayment>(resPayment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreatePaymentAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }
    }
}
