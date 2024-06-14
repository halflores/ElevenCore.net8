using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Entidad
{
    public class Plantilla
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string SucursalId { get; set; } = null!;
        public string CuentaCajaId { get; set; } = null!;
        public string CuentaCajaNombre { get; set; } = null!;
        public Sucursal Sucursal { get; set; } = null!;
        public double TC { get; set; }
    }
}
