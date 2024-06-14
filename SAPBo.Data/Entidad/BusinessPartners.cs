using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Entidad
{
    public class BusinessPartners
    {
        public BusinessPartners() {
            ContactEmployees = new List<ContactEmployee>();
            BPAddresses = new List<BPAddress>();    
        }   
        public string CardType { get; set; } = "cCustomer";
        public string CardCode { get; set; } = null!;
        public string CardName { get; set; } = null!;
        public int? GroupCode { get; set; }
        public int? Series { get; set; }
        public string Currency { get; set; } = "BS";
        public string? FederalTaxID { get; set; }
        public string? Cellular { get; set; }
        public string? EmailAddress { get; set; }
        public int? Territory { get; set; }
        public string? ContactPerson { get; set; }
        public int? SalesPersonCode { get; set; }
        public string SubjectToWithholdingTax { get; set; } = "boYES";
        public string WTCode { get; set; } = "IT";
        public int? U_Tipo_Documento { get; set; }
        public string? U_COMPLEFE { get; set; }
        public string? U_TIPDOC { get; set; }
        public List<ContactEmployee> ContactEmployees { get; set; }
        public List<BPAddress> BPAddresses { get; set; }

    }
}
