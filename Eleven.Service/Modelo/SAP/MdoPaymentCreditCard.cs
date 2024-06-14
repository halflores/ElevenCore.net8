using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoPaymentCreditCard
    {
        public int? LineNum { get; set; }
        public int? CreditCard { get; set; }
        public string CreditAcct { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime CardValidUntil { get; set; }
        public string VoucherNum { get; set; }
        public string OwnerIdNum { get; set; }
        public string OwnerPhone { get; set; }
        public double? CreditSum { get; set; }
        public int? PaymentMethodCode { get; set; }
    }
}
