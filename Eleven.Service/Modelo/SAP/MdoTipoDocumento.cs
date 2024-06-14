using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoTipoDocumento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public int? IdSIN { get; set; }
        public string? DescripcionSIN { get; set; }
        public bool Habilitado { get; set; }
    }
}
