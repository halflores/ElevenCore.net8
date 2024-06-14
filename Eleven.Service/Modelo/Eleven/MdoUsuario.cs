using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.Eleven
{
    public class MdoUsuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaUltimoPassword { get; set; }
        public bool RestablecerPassword { get; set; }
        public bool Habilitado { get; set; }
        public bool Eliminado { get; set; }
        public bool Modificado { get; set; }
        public int PerfilId { get; set; }
        public int EmpleadoSAP { get; set; }
    }
}
