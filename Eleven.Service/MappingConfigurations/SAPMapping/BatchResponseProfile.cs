using AutoMapper;
using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;

namespace Eleven.Service.MappingConfigurations.SAPMapping
{
    public class BatchResponseProfile : Profile
    {
        public BatchResponseProfile()
        {
            CreateMap<MdoBatchResponse, BatchResponse>()
            .ForMember(dest => dest.ContentID, opt => opt.MapFrom(src => src.ContentID))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DocEntry, opt => opt.MapFrom(src => src.DocEntry))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.StatusCode, opt => opt.MapFrom(src => src.StatusCode))
            .ReverseMap();
        }
    }
}
