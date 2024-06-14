using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoBPAddress
    {
        public int? RowNum { get; set; }
        public string AddressType { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Calle { get; set; }
        public string? CalleNro { get; set; }
        public string? Barrio { get; set; }
        public string Country { get; set; } = "BO";
        public int DepartamentoId { get; set; }
        public string TaxCode { get; set; } = "IVA";
        public string? BuildingFloorRoom { get; set; }
        public string? Edificio { get; set; }
        public string? EdificioDepartamento { get; set; }
        public string? Coordenadas { get; set; }
        public string? PersonaReferencia { get; set; }
        public string? NroMedidor { get; set; }
        public string? Mz { get; set; }
        public string? U_Referencia { get; set; }
        public string? UV { get; set; }
        public string? U_Partido { get; set; }
        public string? U_Prefijo { get; set; }
        public string? Avenida { get; set; }
        public string? City { get; set; }
        public string? U_MunicipioID { get; set; }
    }
}
