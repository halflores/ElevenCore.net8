using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Entidad
{
    public class Sucursal
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public int? Telefono { get; set; }
        public string Correo { get; set; } = null!;

        public string? Coordenada { get; set; }

        public int? CiudadId { get; set; }

        public string CiudadNombre { get; set; } = null!;
        public int? MunicipioId { get; set; }
        public string? MunicipioNombre { get; set; }
        public int? EstadoId { get; set; }

        public string? AlmacenSucursalId { get; set; }
        public string? AlmacenAlternoId { get; set; }

        public string? AlmacenDistribucionId { get; set; }
        public string? Almacen05Id { get; set; }

        public int? ListaPrecioId { get; set; }
        public string? ClienteGenericoCodigo { get; set; }
        public int? DocificacionManualId { get; set; }
        public int? DocificacionAutomaticaId { get; set; }
        public int? DocificacionServicioAutomaticaId { get; set; }
        public int? DocificacionServicioManualId { get; set; }
        public string? AlmacenSucursalNombre { get; set; }
        public string? AlmacenAlternoNombre { get; set; }

        public string? AlmacenDistribucionNombre { get; set; }
        public string? Almacen05Nombre { get; set; }

        public string? ListaPrecioNombre { get; set; }
        public string? ClienteGenericoNombre { get; set; }
        public double PorcentajeDescuentoTienda { get; set; }

        public string SucursalUserName { get; set; } = null!;
        public string SucursalPassword { get; set; } = null!;


        public string? UnidadNegocio { get; set; }

        public string? UnidadRegional { get; set; }

        public string? UnidadDivision { get; set; }
        public bool EntregaAutomatica { get; set; }
        public bool ImprimeDirecto { get; set; }

        public int? BranchId { get; set; }
        public int SucursalId { get; set; }
        public int Celular { get; set; }
        public bool DespachoRegional { get; set; }

        public string? CajaControlCodigoBs { get; set; }
        public string? CajaControlDescripcionBs { get; set; }
        public string? CajaControlCodigoSu { get; set; }
        public string? CajaControlDescripcionSu { get; set; }
        public int LongTail { get; set; }
        public int DevolucionGeneral { get; set; }
    }

}
