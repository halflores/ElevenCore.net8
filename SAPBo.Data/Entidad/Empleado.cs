using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Entidad
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public int VendedorId { get; set; }
        public string VendedorNombre { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public int? PositionId { get; set; }
        public int? BranchId { get; set; }
        public string PlantillaId { get; set; } = null!;
        public Plantilla Plantilla { get; set; } = null!;
        public string? PositionName { get; set; }
        public string? DocumentoNro { get; set; }
        public string? DocumentoTipo { get; set; }
        public string? DocumentoExtension { get; set; }
    }
}
