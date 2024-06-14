using AutoMapper;
using Eleven.Data.Entidad;
using Eleven.Service.Modelo.Eleven;
using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;

namespace Eleven.Service.MappingConfigurations.SAPMapping
{
    public class PaymentCreditCardProfile : Profile
    {
        public PaymentCreditCardProfile()
        {
            CreateMap<MdoPaymentCreditCard, PaymentCreditCard>()
                .ForMember(dest => dest.CreditCard, opt => opt.MapFrom(src => src.CreditCard))
                .ForMember(dest => dest.CreditCardNumber, opt => opt.MapFrom(src => src.CreditCardNumber))
                .ForMember(dest => dest.CardValidUntil, opt => opt.MapFrom(src => src.CardValidUntil))
                .ForMember(dest => dest.CreditAcct, opt => opt.MapFrom(src => src.CreditAcct))
                .ForMember(dest => dest.CreditSum, opt => opt.MapFrom(src => src.CreditSum))
                .ForMember(dest => dest.LineNum, opt => opt.MapFrom(src => src.LineNum))
                .ForMember(dest => dest.OwnerIdNum, opt => opt.MapFrom(src => src.OwnerIdNum))
                .ForMember(dest => dest.OwnerPhone, opt => opt.MapFrom(src => src.OwnerPhone))
                .ForMember(dest => dest.PaymentMethodCode, opt => opt.MapFrom(src => src.PaymentMethodCode))
                .ForMember(dest => dest.VoucherNum, opt => opt.MapFrom(src => src.VoucherNum))
                .ReverseMap();
        }
    }
}
