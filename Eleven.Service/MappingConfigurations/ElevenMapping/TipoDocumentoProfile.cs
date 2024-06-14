using AutoMapper;
using Eleven.Data.Entidad;
using Eleven.Service.Modelo.SAP;

namespace Eleven.Service.MappingConfigurations.Eleven
{
    public class TipoDocumentoProfile : Profile
    {
        public TipoDocumentoProfile()
        {
            CreateMap<MdoTipoDocumento, TipoDocumento>()
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.IdSIN, opt => opt.MapFrom(src => src.IdSIN))
                .ForMember(dest => dest.DescripcionSIN, opt => opt.MapFrom(src => src.DescripcionSIN))
                .ForMember(dest => dest.Habilitado, opt => opt.MapFrom(src => src.Habilitado))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

        }
    }
}
