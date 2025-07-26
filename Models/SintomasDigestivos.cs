using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Models
{
    [Table("sintomas_digestivos")]
    public class SintomasDigestivos
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("paciente_id")]
        [ForeignKey("Paciente")]
        public int PacienteId { get; set; }

        public Paciente? Paciente { get; set; }

        [Column("dolor_estomacal")]
        public bool? DolorEstomacal { get; set; }

        [Column("nausea")]
        public bool? Nausea { get; set; }

        [Column("vomito")]
        public bool? Vomito { get; set; }

        [Column("ardor_estomacal")]
        public bool? ArdorEstomacal { get; set; }

        [Column("sangrado_digestivo")]
        public bool? SangradoDigestivo { get; set; }

        [Column("zona_dolor")]
        public string? ZonaDolor { get; set; }

        [Column("frecuencia_dolor")]
        public string? FrecuenciaDolor { get; set; }

        [Column("fecha_evaluacion")]
        public DateTime FechaEvaluacion { get; set; }
    }
}
