using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoPaymentAccount
    {
        public string AccountCode { get; set; }
        public double SumPaid { get; set; }
        public string Decription { get; set; }

        public string ProfitCenter { get; set; } // NEGOCIO
        public string ProfitCenter2 { get; set; } // REGIONAL
        public string ProfitCenter3 { get; set; } // DIVISION

    }
}
