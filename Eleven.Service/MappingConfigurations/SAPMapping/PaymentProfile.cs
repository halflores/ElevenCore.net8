using AutoMapper;
using Eleven.Data.Entidad;
using Eleven.Service.Modelo.Eleven;
using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;

namespace Eleven.Service.MappingConfigurations.SAPMapping
{
    public class PaymentProfile: Profile
    {
        public PaymentProfile()
        {
            CreateMap<MdoPayment, Payment>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.CardCode, opt => opt.MapFrom(src => src.CardCode))
                .ForMember(dest => dest.CashAccount, opt => opt.MapFrom(src => src.CashAccount))
                .ForMember(dest => dest.CashSum, opt => opt.MapFrom(src => src.CashSum))
                .ForMember(dest => dest.CheckAccount, opt => opt.MapFrom(src => src.CheckAccount))
                .ForMember(dest => dest.ControlAccount, opt => opt.MapFrom(src => src.ControlAccount))
                .ForMember(dest => dest.CounterReference, opt => opt.MapFrom(src => src.CounterReference))
                .ForMember(dest => dest.DocCurrency, opt => opt.MapFrom(src => src.DocCurrency))
                .ForMember(dest => dest.DocDate, opt => opt.MapFrom(src => src.DocDate))
                .ForMember(dest => dest.DocEntry, opt => opt.MapFrom(src => src.DocEntry))
                .ForMember(dest => dest.DocNum, opt => opt.MapFrom(src => src.DocNum))
                .ForMember(dest => dest.DocType, opt => opt.MapFrom(src => src.DocType))
                .ForMember(dest => dest.JournalRemarks, opt => opt.MapFrom(src => src.JournalRemarks))
                .ForMember(dest => dest.PaymentAccounts, opt => opt.MapFrom(src => src.PaymentAccounts))
                .ForMember(dest => dest.PaymentChecks, opt => opt.MapFrom(src => src.PaymentChecks))
                .ForMember(dest => dest.PaymentInvoices, opt => opt.MapFrom(src => src.PaymentInvoices))
                .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks))
                .ForMember(dest => dest.PaymentChecks, opt => opt.MapFrom(src => src.PaymentChecks))
                .ForMember(dest => dest.PaymentCreditCards, opt => opt.MapFrom(src => src.PaymentCreditCards))
                .ForMember(dest => dest.Series, opt => opt.MapFrom(src => src.Series))
                .ForMember(dest => dest.TaxDate, opt => opt.MapFrom(src => src.TaxDate))
                .ForMember(dest => dest.TransferAccount, opt => opt.MapFrom(src => src.TransferAccount))
                .ForMember(dest => dest.TransferDate, opt => opt.MapFrom(src => src.TransferDate))
                .ForMember(dest => dest.TransferReference, opt => opt.MapFrom(src => src.TransferReference))
                .ForMember(dest => dest.TransferSum, opt => opt.MapFrom(src => src.TransferSum))
                .ForMember(dest => dest.U_CajaControl, opt => opt.MapFrom(src => src.U_CajaControl))
                .ForMember(dest => dest.U_LB_TipoDocumento, opt => opt.MapFrom(src => src.U_LB_TipoDocumento))
                .ForMember(dest => dest.U_NumeroPedido, opt => opt.MapFrom(src => src.U_NumeroPedido))
                .ForMember(dest => dest.U_OrigenAplicacion, opt => opt.MapFrom(src => src.U_OrigenAplicacion))
                .ForMember(dest => dest.U_PagoClever, opt => opt.MapFrom(src => src.U_PagoClever))
                .ForMember(dest => dest.U_Ref, opt => opt.MapFrom(src => src.U_Ref))
                .ForMember(dest => dest.U_TipoPago, opt => opt.MapFrom(src => src.U_TipoPago))
                .ReverseMap();
        }
    }
}
