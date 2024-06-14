using SAPBo.Data.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoBusinessPartners
    {
        public string CardType { get; set; } = "cCustomer";
        public string? CardCode { get; set; }
        public string? CardName { get; set; }

        public string PrimerNombre { get; set; } = null!;
        public string SegundoNombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string? ApellidoCasada { get; set; }

        public string? NombreCompleto { get; set; }

        public int? GrupoCliente { get; set; }
        public int? SerieCliente { get; set; }
        public string Currency { get; set; } = "BS";
        public string? MovilNro { get; set; }
        public string? Correo { get; set; }
        public int? Territory { get; set; }
        public string? ContactPerson { get; set; }
        public int? SalesPersonCode { get; set; }
        public string SubjectToWithholdingTax { get; set; } = "boYES";
        public string WTCode { get; set; } = "IT";
        public int TipoDocumentoID { get; set; }
        public string NroDocumento { get; set; } = null!;
        public string? Complemento { get; set; }
        public string? U_COMPLEFE { get; set; }
        public string? U_TIPDOC { get; set; }
        public string FullName
        {
            get
            {
                string fullName = string.IsNullOrEmpty(PrimerNombre) ? "" : PrimerNombre.Trim().ToUpper();
                fullName = string.IsNullOrEmpty(SegundoNombre) ? fullName : string.Format("{0} {1}", fullName, SegundoNombre.Trim().ToUpper());
                fullName = string.IsNullOrEmpty(ApellidoPaterno) ? fullName : string.Format("{0} {1}", fullName, ApellidoPaterno.Trim().ToUpper());
                fullName = string.IsNullOrEmpty(ApellidoMaterno) ? fullName : string.Format("{0} {1}", fullName, ApellidoMaterno.Trim().ToUpper());
                fullName = string.IsNullOrEmpty(ApellidoCasada) ? fullName : string.Format("{0} de {1}", fullName, ApellidoCasada.Trim().ToUpper());
                NombreCompleto = fullName.Trim().Length > 0 ? fullName : NombreCompleto;
                return fullName.Trim();
            }
            set { }
        }
        public MdoEmpleado? Empleado { get; set; }
        public MdoTipoDocumento TipoDocumento { get; set; } = null!;
        public List<MdoContactEmployee>? ContactEmployees { get; set; }
        public List<MdoBPAddress>? BPAddresses { get; set; }
    }
}
