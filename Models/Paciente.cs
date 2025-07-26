using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Models
{
    [Table("paciente")]
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("dni")]
        [StringLength(8)]
        public string DNI { get; set; } = string.Empty;

        [Required]
        [Column("nombres")]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [Column("apellido_paterno")]
        public string ApellidoPaterno { get; set; } = string.Empty;

        [Required]
        [Column("apellido_materno")]
        public string ApellidoMaterno { get; set; } = string.Empty;

        [Required]
        [Column("sexo")]
        public string Sexo { get; set; } = string.Empty;

        [Required]
        [Column("edad")]
        public int Edad { get; set; }

        [Required]
        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
