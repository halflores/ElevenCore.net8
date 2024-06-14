using SAPBo.Data.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoPlantilla
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string SucursalId { get; set; } = null!;
        public string CuentaCajaId { get; set; } = null!;
        public string CuentaCajaNombre { get; set; } = null!;
        public MdoSucursal Sucursal { get; set; } = null!;
        public double TC { get; set; }
    }
}
