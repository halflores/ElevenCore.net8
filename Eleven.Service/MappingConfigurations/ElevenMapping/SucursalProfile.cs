using AutoMapper;
using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;

namespace Eleven.Service.MappingConfigurations.Eleven
{
    public class SucursalProfile : Profile
    {
        public SucursalProfile()
        {
            CreateMap<MdoSucursal, Sucursal>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo))
                .ForMember(dest => dest.Coordenada, opt => opt.MapFrom(src => src.Coordenada))
                .ForMember(dest => dest.EstadoId, opt => opt.MapFrom(src => src.EstadoId))
                .ForMember(dest => dest.AlmacenSucursalId, opt => opt.MapFrom(src => src.AlmacenSucursalId))
                .ForMember(dest => dest.AlmacenSucursalNombre, opt => opt.MapFrom(src => src.AlmacenSucursalNombre))
                .ForMember(dest => dest.AlmacenDistribucionId, opt => opt.MapFrom(src => src.AlmacenDistribucionId))
                .ForMember(dest => dest.AlmacenDistribucionNombre, opt => opt.MapFrom(src => src.AlmacenDistribucionNombre))
                .ForMember(dest => dest.AlmacenAlternoId, opt => opt.MapFrom(src => src.AlmacenAlternoId))
                .ForMember(dest => dest.AlmacenAlternoNombre, opt => opt.MapFrom(src => src.AlmacenAlternoNombre))
                .ForMember(dest => dest.Almacen05Id, opt => opt.MapFrom(src => src.Almacen05Id))
                .ForMember(dest => dest.Almacen05Nombre, opt => opt.MapFrom(src => src.Almacen05Nombre))
                .ForMember(dest => dest.ListaPrecioId, opt => opt.MapFrom(src => src.ListaPrecioId))
                .ForMember(dest => dest.ListaPrecioNombre, opt => opt.MapFrom(src => src.ListaPrecioNombre))
                .ForMember(dest => dest.PorcentajeDescuentoTienda, opt => opt.MapFrom(src => src.PorcentajeDescuentoTienda))
                .ForMember(dest => dest.ClienteGenericoCodigo, opt => opt.MapFrom(src => src.ClienteGenericoCodigo))
                .ForMember(dest => dest.ClienteGenericoNombre, opt => opt.MapFrom(src => src.ClienteGenericoNombre))
                .ForMember(dest => dest.DocificacionAutomaticaId, opt => opt.MapFrom(src => src.DocificacionAutomaticaId))
                .ForMember(dest => dest.DocificacionManualId, opt => opt.MapFrom(src => src.DocificacionManualId))
                .ForMember(dest => dest.DocificacionServicioAutomaticaId, opt => opt.MapFrom(src => src.DocificacionServicioAutomaticaId))
                .ForMember(dest => dest.DocificacionServicioManualId, opt => opt.MapFrom(src => src.DocificacionServicioManualId))
                .ForMember(dest => dest.SucursalUserName, opt => opt.MapFrom(src => src.SucursalUserName))
                .ForMember(dest => dest.SucursalPassword, opt => opt.MapFrom(src => src.SucursalPassword))
                .ForMember(dest => dest.UnidadNegocio, opt => opt.MapFrom(src => src.UnidadNegocio))
                .ForMember(dest => dest.UnidadRegional, opt => opt.MapFrom(src => src.UnidadRegional))
                .ForMember(dest => dest.UnidadDivision, opt => opt.MapFrom(src => src.UnidadDivision))
                .ForMember(dest => dest.CiudadId, opt => opt.MapFrom(src => src.CiudadId))
                .ForMember(dest => dest.CiudadNombre, opt => opt.MapFrom(src => src.CiudadNombre))
                .ForMember(dest => dest.MunicipioId, opt => opt.MapFrom(src => src.MunicipioId))
                .ForMember(dest => dest.MunicipioNombre, opt => opt.MapFrom(src => src.MunicipioNombre))
                .ForMember(dest => dest.EntregaAutomatica, opt => opt.MapFrom(src => src.EntregaAutomatica))
                .ForMember(dest => dest.ImprimeDirecto, opt => opt.MapFrom(src => src.ImprimeDirecto))
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
                .ForMember(dest => dest.SucursalId, opt => opt.MapFrom(src => src.SucursalId))
                .ForMember(dest => dest.Celular, opt => opt.MapFrom(src => src.Celular))
                .ForMember(dest => dest.DespachoRegional, opt => opt.MapFrom(src => src.DespachoRegional))
                .ForMember(dest => dest.CajaControlCodigoBs, opt => opt.MapFrom(src => src.CajaControlCodigoBs))
                .ForMember(dest => dest.CajaControlDescripcionBs, opt => opt.MapFrom(src => src.CajaControlDescripcionBs))
                .ForMember(dest => dest.CajaControlCodigoSu, opt => opt.MapFrom(src => src.CajaControlCodigoSu))
                .ForMember(dest => dest.CajaControlDescripcionSu, opt => opt.MapFrom(src => src.CajaControlDescripcionSu))
                .ForMember(dest => dest.LongTail, opt => opt.MapFrom(src => src.LongTail))
                .ForMember(dest => dest.DevolucionGeneral, opt => opt.MapFrom(src => src.DevolucionGeneral))
                .ReverseMap();
        }
    }
}
