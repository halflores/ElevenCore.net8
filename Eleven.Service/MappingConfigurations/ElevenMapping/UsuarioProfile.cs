using AutoMapper;
using Eleven.Data.Entidad;
using Eleven.Service.Modelo.Eleven;

namespace Eleven.Service.MappingConfigurations.Eleven
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<MdoUsuario, Usuario>()
                .ForMember(dest => dest.PerfilId, opt => opt.MapFrom(src => src.PerfilId))
                .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombres))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.RegPassword, opt => opt.Ignore())
                .ForMember(dest => dest.EmpleadoSAP, opt => opt.MapFrom(src => src.EmpleadoSAP))
                .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.Apellidos))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion))
                .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.Eliminado, opt => opt.MapFrom(src => src.Eliminado))
                .ForMember(dest => dest.FechaUltimoPassword, opt => opt.MapFrom(src => src.FechaUltimoPassword))
                .ForMember(dest => dest.Habilitado, opt => opt.MapFrom(src => src.Habilitado))
                .ForMember(dest => dest.Modificado, opt => opt.MapFrom(src => src.Modificado))
                .ForMember(dest => dest.PerfilId, opt => opt.MapFrom(src => src.PerfilId))
                .ForMember(dest => dest.RestablecerPassword, opt => opt.MapFrom(src => src.RestablecerPassword))
                .AfterMap((_, dest) =>
                {
                    dest.FechaCreacion = DateTime.Now;
                })
                .ReverseMap();

        }
    }
}
