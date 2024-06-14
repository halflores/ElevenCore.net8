using AutoMapper;
using Eleven.Data.Entidad;
using Eleven.Service.Modelo.Eleven;
using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;

namespace Eleven.Service.MappingConfigurations.SAPMapping
{
    public class WithholdingTaxDataProfile : Profile
    {
        public WithholdingTaxDataProfile()
        {
            CreateMap<MdoWithholdingTaxData, WithholdingTaxData>()
                .ForMember(dest => dest.WTAmount, opt => opt.MapFrom(src => src.WTAmount))
                .ForMember(dest => dest.WTCode, opt => opt.MapFrom(src => src.WTCode))
                .ReverseMap();
        }
    }
}
