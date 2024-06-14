using AutoMapper;
using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;

namespace Eleven.Service.MappingConfigurations.Eleven
{
    public class PlantillaProfile : Profile
    {
        public PlantillaProfile()
        {
            CreateMap<MdoPlantilla, Plantilla>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CuentaCajaId, opt => opt.MapFrom(src => src.CuentaCajaId))
                .ForMember(dest => dest.CuentaCajaNombre, opt => opt.MapFrom(src => src.CuentaCajaNombre))
                .ForMember(dest => dest.SucursalId, opt => opt.MapFrom(src => src.SucursalId))
                .ForMember(dest => dest.TC, opt => opt.MapFrom(src => src.TC))
                .ForMember(dest => dest.Sucursal, opt => opt.MapFrom(src => src.Sucursal))
                .ReverseMap();
        }
    }
}
