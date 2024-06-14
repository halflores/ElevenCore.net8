using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Entidad
{
    public class ContactEmployee
    {
        public int? InternalCode { get; set; }
        public string Name { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? U_Apellido_Materno { get; set; }
        public string? U_Apellido_de_casada { get; set; }
        public string? MobilePhone { get; set; }
        public string? E_Mail { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public int? U_ID_Clever { get; set; }
        public string? U_Codigo_Dismaclub { get; set; }
        public string? U_Tipo_Documento { get; set; }
        public string U_Estado_Civil { get; set; } = null!;
        public string? U_Expedido { get; set; }
        public string? Phone2 { get; set; }
    }
}
