using SAPBo.Data.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoEmpleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string EmpleadoNombre
        {
            get
            {
                return string.Format("{0} {1}", Nombre, Apellido);
            }
        }
        public string Correo { get; set; } = null!;
        public int VendedorId { get; set; }
        public string VendedorNombre { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public int? PositionId { get; set; }
        public int? BranchId { get; set; }
        public string PlantillaId { get; set; } = null!;
        public MdoPlantilla Plantilla { get; set; } = null!;
        public string? PositionName { get; set; }
        public string? DocumentoNro { get; set; }
        public string? DocumentoTipo { get; set; }
        public string? DocumentoExtension { get; set; }
        public void SetPlantillaSucursal(MdoSucursal eSucursal)
        {
            if (Plantilla != null && !string.IsNullOrEmpty(Plantilla.SucursalId))
            {
                Plantilla.SucursalId = eSucursal.Code;
                Plantilla.Sucursal = eSucursal;
            }
        }

        public MdoSucursal GetPlantillaSucursal()
        {
            return Plantilla.Sucursal;
        }
    }
}
