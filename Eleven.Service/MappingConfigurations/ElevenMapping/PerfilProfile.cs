using AutoMapper;
using Eleven.Data.Entidad;
using Eleven.Service.Modelo.Eleven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.MappingConfigurations.Eleven
{
    public class PerfilProfile : Profile
    {
        public PerfilProfile()
        {
            CreateMap<MdoPerfil, Perfil>()
                .ForMember(dest => dest.PerfilSap, opt => opt.MapFrom(src => src.PerfilSap))
                .ForMember(dest => dest.Venta, opt => opt.MapFrom(src => src.Venta))
                .ForMember(dest => dest.ModuloId, opt => opt.MapFrom(src => src.ModuloId))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Eliminado, opt => opt.MapFrom(src => src.Eliminado))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion))
                .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
                .ForMember(dest => dest.Habilitado, opt => opt.MapFrom(src => src.Habilitado))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .AfterMap((_, dest) =>
                {
                    dest.FechaCreacion = DateTime.Now;
                })
                .ReverseMap();
        }
    }
}
