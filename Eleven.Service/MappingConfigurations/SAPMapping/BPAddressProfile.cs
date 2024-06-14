using AutoMapper;
using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;

namespace Eleven.Service.MappingConfigurations.SAPMapping
{
    public class BPAddressProfile : Profile
    {
        public BPAddressProfile()
        {
            CreateMap<MdoBPAddress, BPAddress>()
            .ForMember(dest => dest.RowNum, opt => opt.MapFrom(src => src.RowNum))
            .ForMember(dest => dest.AddressType, opt => opt.MapFrom(src => src.AddressType))
            .ForMember(dest => dest.AddressName, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.AddressName3, opt => opt.MapFrom(src => src.Telefono))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Calle))
            .ForMember(dest => dest.StreetNo, opt => opt.MapFrom(src => src.CalleNro))
            .ForMember(dest => dest.Block, opt => opt.MapFrom(src => src.Barrio))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.DepartamentoId))
            .ForMember(dest => dest.TaxCode, opt => opt.MapFrom(src => src.TaxCode))
            .ForMember(dest => dest.BuildingFloorRoom, opt => opt.MapFrom(src => src.BuildingFloorRoom))
            .ForMember(dest => dest.U_Coordenadas, opt => opt.MapFrom(src => src.Coordenadas))
            .ForMember(dest => dest.U_Nro_Medidor_Luz, opt => opt.MapFrom(src => src.NroMedidor))
            .ForMember(dest => dest.U_Manzana, opt => opt.MapFrom(src => src.Mz))
            .ForMember(dest => dest.U_Referencia, opt => opt.MapFrom(src => src.U_Referencia))
            .ForMember(dest => dest.U_Unidad_Vecinal, opt => opt.MapFrom(src => src.UV))
            .ForMember(dest => dest.U_Partido, opt => opt.MapFrom(src => src.U_Partido))
            .ForMember(dest => dest.AddressName2, opt => opt.MapFrom(src => src.Avenida))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.U_MunicipioID, opt => opt.MapFrom(src => src.U_MunicipioID))
            .ReverseMap();

        }
    }
}
