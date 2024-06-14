using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eleven.Data.Entidad
{
    public partial class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Login { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
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
        [NotMapped]
        public string RegPassword { get; set; } = null!;
    }
}
