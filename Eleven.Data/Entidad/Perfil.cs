using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Data.Entidad
{
    public partial class Perfil
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;    
        public bool Habilitado { get; set; }
        public bool Eliminado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int? PerfilSap { get; set; }
        public bool Venta { get; set; }
        public int ModuloId { get; set; }

    }
}
