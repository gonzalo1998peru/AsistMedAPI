using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Models
{
    [Table("paciente")]
    public class Paciente
    {
        [Key]
        [Required]
        [StringLength(8)]
        public string Dni { get; set; } = null!;

        [Required]
        [Range(0, 110, ErrorMessage = "La edad debe estar entre 0 y 110 años.")]
        public int edad { get; set; }

        [Required]
        [RegularExpression("^(M|F)$", ErrorMessage = "El sexo debe ser 'M' o 'F'.")]
        public string sexo { get; set; } = null!;
    }
}

