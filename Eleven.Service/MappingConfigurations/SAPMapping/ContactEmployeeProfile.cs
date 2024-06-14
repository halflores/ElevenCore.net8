using AutoMapper;
using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;

namespace Eleven.Service.MappingConfigurations.SAPMapping
{
    public class ContactEmployeeProfile : Profile
    {
        public ContactEmployeeProfile()
        {
            CreateMap<MdoContactEmployee, ContactEmployee>()
            .ForMember(dest => dest.InternalCode, opt => opt.MapFrom(src => src.InternalCode))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.PrimerNombre))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.SegundoNombre))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.ApellidoPaterno))
            .ForMember(dest => dest.U_Apellido_Materno, opt => opt.MapFrom(src => src.ApellidoMaterno))
            .ForMember(dest => dest.U_Apellido_de_casada, opt => opt.MapFrom(src => src.ApellidoCasada))
            .ForMember(dest => dest.MobilePhone, opt => opt.MapFrom(src => src.NumeroMovil))
            .ForMember(dest => dest.E_Mail, opt => opt.MapFrom(src => src.Correo))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.FechaNacimiento ))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Genero))
            .ForMember(dest => dest.U_ID_Clever, opt => opt.MapFrom(src => src.U_ID_Clever))
            .ForMember(dest => dest.U_Codigo_Dismaclub, opt => opt.MapFrom(src => src.CodigoMaestroClub))
            .ForMember(dest => dest.U_Tipo_Documento, opt => opt.MapFrom(src => src.TipoDocumentoID))
            .ForMember(dest => dest.U_Estado_Civil, opt => opt.MapFrom(src => src.EstadoCivilID))
            .ForMember(dest => dest.U_Expedido, opt => opt.MapFrom(src => src.Expedido))
            .ForMember(dest => dest.Phone2, opt => opt.MapFrom(src => src.Phone2))
            .ReverseMap();
        }
    }
}
