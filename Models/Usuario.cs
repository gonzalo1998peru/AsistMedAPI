using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key] public int Id { get; set; }

        [Required, StringLength(50)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required] public string Contraseña { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Rol { get; set; } = "medico"; // medico | triaje | admin

        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public ICollection<Paciente>? Pacientes { get; set; }
    }
}