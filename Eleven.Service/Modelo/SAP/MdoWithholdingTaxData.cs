using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoWithholdingTaxData
    {
        public string WTCode { get; set; } = "IT";
        public decimal WTAmount { get; set; }
    }
}
