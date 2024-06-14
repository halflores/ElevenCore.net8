using AutoMapper;
using Eleven.Data.Entidad;
using Eleven.Service.Modelo.Eleven;
using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;

namespace Eleven.Service.MappingConfigurations.SAPMapping
{
    public class PaymentAccountProfile : Profile
    {
        public PaymentAccountProfile()
        {
            CreateMap<MdoPaymentAccount, PaymentAccount>()
                .ForMember(dest => dest.AccountCode, opt => opt.MapFrom(src => src.AccountCode))
                .ForMember(dest => dest.Decription, opt => opt.MapFrom(src => src.Decription))
                .ForMember(dest => dest.ProfitCenter, opt => opt.MapFrom(src => src.ProfitCenter))
                .ForMember(dest => dest.ProfitCenter2, opt => opt.MapFrom(src => src.ProfitCenter2))
                .ForMember(dest => dest.ProfitCenter3, opt => opt.MapFrom(src => src.ProfitCenter3))
                .ForMember(dest => dest.SumPaid, opt => opt.MapFrom(src => src.SumPaid))
                .ReverseMap();
        }

    }
}
