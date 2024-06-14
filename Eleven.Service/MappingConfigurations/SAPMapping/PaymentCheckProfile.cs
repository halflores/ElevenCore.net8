using AutoMapper;
using Eleven.Data.Entidad;
using Eleven.Service.Modelo.Eleven;
using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;
using System.Security.Cryptography;

namespace Eleven.Service.MappingConfigurations.SAPMapping
{
    public class PaymentCheckProfile : Profile
    {
        public PaymentCheckProfile()
        {
            CreateMap<MdoPaymentCheck, PaymentCheck>()
                .ForMember(dest => dest.AccounttNum, opt => opt.MapFrom(src => src.AccounttNum))
                .ForMember(dest => dest.BankCode, opt => opt.MapFrom(src => src.BankCode))
                .ForMember(dest => dest.CheckNumber, opt => opt.MapFrom(src => src.CheckNumber))
                .ForMember(dest => dest.CheckSum, opt => opt.MapFrom(src => src.CheckSum))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.CountryCode))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.FiscalID, opt => opt.MapFrom(src => src.FiscalID))
                .ForMember(dest => dest.OriginallyIssuedBy, opt => opt.MapFrom(src => src.OriginallyIssuedBy))
                .ForMember(dest => dest.Trnsfrable, opt => opt.MapFrom(src => src.Trnsfrable))
                .ReverseMap();
        }

    }
}
