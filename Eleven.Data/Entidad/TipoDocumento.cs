using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Data.Entidad
{
    public partial class TipoDocumento
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public int? IdSIN { get; set; }
        public string? DescripcionSIN { get; set; }
        public bool Habilitado { get; set; }
    }
}
