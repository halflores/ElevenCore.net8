using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Entidad
{
    public class BPAddress
    {
        public int? RowNum { get; set; }
        public string AddressType { get; set; } = null!;
        public string? AddressName { get; set; }
        public string? AddressName3 { get; set; }
        public string? Street { get; set; }
        public string? StreetNo { get; set; }
        public string? Block { get; set; }
        public string Country { get; set; } = "BO";
        public string? State { get; set; }
        public string TaxCode { get; set; } = "IVA";
        public string? BuildingFloorRoom { get; set; }
        public string? U_Coordenadas { get; set; }
        public string? U_Nro_Medidor_Luz { get; set; }
        public string? U_Manzana { get; set; }
        public string? U_Referencia { get; set; }
        public string? U_Unidad_Vecinal { get; set; }
        public string? U_Partido { get; set; }
        public string? U_Prefijo { get; set; }
        public string? AddressName2 { get; set; }
        public string? City { get; set; }
        public string? U_MunicipioID { get; set; }
    }
}
