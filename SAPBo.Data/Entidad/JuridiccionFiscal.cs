using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Entidad
{
    public class JuridiccionFiscal
    {
        public int LineNumber { get; set; }
        public string? JurisdictionCode { get; set; }
        public double TaxAmount { get; set; }
    }
}
