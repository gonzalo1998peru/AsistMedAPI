using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Models
{
    [Table("evaluacion_clinica")]
    public class EvaluacionClinica
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("paciente_id")]
        [ForeignKey("Paciente")]
        public int PacienteId { get; set; }

        public Paciente? Paciente { get; set; }

        [Column("fatiga")]
        public bool? Fatiga { get; set; }

        [Column("dolor_abdominal")]
        public bool? DolorAbdominal { get; set; }

        [Column("medicamentos_actuales")]
        public bool? MedicamentosActuales { get; set; }

        [Column("alergias")]
        public bool? Alergias { get; set; }

        [Column("antecedentes_patologicos")]
        public bool? AntecedentesPatologicos { get; set; }

        [Column("fecha_evaluacion")]
        public DateTime FechaEvaluacion { get; set; }
    }
}
