using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Entidad
{
    public class SerialNumber
    {
        public string? ManufacturerSerialNumber { get; set; }
        public string? InternalSerialNumber { get; set; }
        public DateTime? ReceptionDate { get; set; }
        public DateTime? WarrantyStart { get; set; }
        public DateTime? WarrantyEnd { get; set; }
        public int? SystemSerialNumber { get; set; }
        public int? BaseLineNumber { get; set; }
        public double? Quantity { get; set; }
    }
}
