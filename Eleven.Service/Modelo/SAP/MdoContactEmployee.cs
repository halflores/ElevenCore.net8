using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoContactEmployee
    {
        public int? InternalCode { get; set; }
        public string Name { get; set; } = null!;
        public string PrimerNombre { get; set; } = null!;
        public string SegundoNombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string? ApellidoCasada { get; set; }
        public string? NumeroMovil { get; set; }
        public string? Correo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Genero { get; set; }
        public int? U_ID_Clever { get; set; }
        public string? CodigoMaestroClub { get; set; }
        public int TipoDocumentoID { get; set; }
        public int EstadoCivilID { get; set; }
        public string? Expedido { get; set; }
        public string? Phone2 { get; set; }
    }
}
