using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoPaymentInvoice
    {
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public double? SumApplied { get; set; }
        public string InvoiceType { get; set; }
        public int? InstallmentId { get; set; }
    }
}
