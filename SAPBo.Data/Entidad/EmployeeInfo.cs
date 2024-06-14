using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Entidad
{
    public class EmployeeInfo
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string eMail { get; set; } = null!;
        public int? SalesPersonCode { get; set; }
        public int? Position { get; set; }
        public int? Branch { get; set; }
        public string U_PlantillaId { get; set; } = null!;  
    }
}
