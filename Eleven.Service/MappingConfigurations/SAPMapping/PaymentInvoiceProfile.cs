using AutoMapper;
using Eleven.Data.Entidad;
using Eleven.Service.Modelo.Eleven;
using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;

namespace Eleven.Service.MappingConfigurations.SAPMapping
{
    public class PaymentInvoiceProfile : Profile
    {
        public PaymentInvoiceProfile()
        {
            CreateMap<MdoPaymentInvoice, PaymentInvoice>()
                .ForMember(dest => dest.DocEntry, opt => opt.MapFrom(src => src.DocEntry))
                .ForMember(dest => dest.InstallmentId, opt => opt.MapFrom(src => src.InstallmentId))
                .ForMember(dest => dest.InvoiceType, opt => opt.MapFrom(src => src.InvoiceType))
                .ForMember(dest => dest.LineNum, opt => opt.MapFrom(src => src.LineNum))
                .ForMember(dest => dest.SumApplied, opt => opt.MapFrom(src => src.SumApplied))
                .ReverseMap();
        }
    }
}
