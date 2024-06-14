using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Entidad
{
    public class BatchResponse
    {
        public int StatusCode { get; set; }
        public int ContentID { get; set; }
        public string? Location { get; set; }
        public int? DocEntry { get; set; }
        public string? Description { get; set; }
    }
}
