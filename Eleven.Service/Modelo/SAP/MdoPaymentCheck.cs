using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoPaymentCheck
    {
        public DateTime DueDate { get; set; }
        public double? CheckSum { get; set; }
        public string CountryCode { get; set; } = "BO";
        public string BankCode { get; set; }
        public string AccounttNum { get; set; }
        public string Trnsfrable { get; set; } = "tYES";
        public int? CheckNumber { get; set; }
        public string OriginallyIssuedBy { get; set; }
        public string FiscalID { get; set; }
        public string Details { get; set; }
    }
}
