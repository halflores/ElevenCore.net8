using AutoMapper;
using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;

namespace Eleven.Service.MappingConfigurations.Eleven
{
    public class EmpleadoProfile : Profile
    {
        public EmpleadoProfile()
        {
            CreateMap<MdoEmpleado, Empleado>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
                .ForMember(dest => dest.VendedorId, opt => opt.MapFrom(src => src.VendedorId))
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo))
                .ForMember(dest => dest.VendedorNombre, opt => opt.MapFrom(src => src.VendedorNombre))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.PositionId, opt => opt.MapFrom(src => src.PositionId))
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
                .ForMember(dest => dest.PlantillaId, opt => opt.MapFrom(src => src.PlantillaId))
                .ForMember(dest => dest.Plantilla, opt => opt.MapFrom(src => src.Plantilla))
                .ReverseMap();
        }
    }
}
