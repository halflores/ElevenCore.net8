using AutoMapper;
using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;

namespace Eleven.Service.MappingConfigurations.SAPMapping
{
    public class BusinessPartnersProfile : Profile
    {
        public BusinessPartnersProfile()
        {
            CreateMap<MdoBusinessPartners, BusinessPartners>()
            .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.CardType))
            .ForMember(dest => dest.CardCode, opt => opt.MapFrom(src => src.CardCode))
            .ForMember(dest => dest.CardName, opt => opt.MapFrom(src => src.CardName))
            .ForMember(dest => dest.GroupCode, opt => opt.MapFrom(src => src.GrupoCliente))
            .ForMember(dest => dest.Series, opt => opt.MapFrom(src => src.SerieCliente))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
            .ForMember(dest => dest.FederalTaxID, opt => opt.MapFrom(src => src.NroDocumento))
            .ForMember(dest => dest.Cellular, opt => opt.MapFrom(src => src.MovilNro))
            .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Correo))
            .ForMember(dest => dest.Territory, opt => opt.MapFrom(src => src.Territory))
            .ForMember(dest => dest.ContactPerson, opt => opt.MapFrom(src => src.ContactPerson))
            .ForMember(dest => dest.SalesPersonCode, opt => opt.MapFrom(src => src.SalesPersonCode))
            .ForMember(dest => dest.SubjectToWithholdingTax, opt => opt.MapFrom(src => src.SubjectToWithholdingTax))
            .ForMember(dest => dest.WTCode, opt => opt.MapFrom(src => src.WTCode))
            .ForMember(dest => dest.U_Tipo_Documento, opt => opt.MapFrom(src => src.TipoDocumentoID))
            .ForMember(dest => dest.U_COMPLEFE, opt => opt.MapFrom(src => src.U_COMPLEFE))
            .ForMember(dest => dest.ContactEmployees, opt => opt.MapFrom(src => src.ContactEmployees))
            .ForMember(dest => dest.BPAddresses, opt => opt.MapFrom(src => src.BPAddresses))
            .ReverseMap();

        }
    }
}
