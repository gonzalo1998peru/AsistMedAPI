using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Models
{
    [Table("prediccion_detalle_pdf")]
    public class PrediccionDetallePdf
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("diagnostico_final")]
        public string? DiagnosticoFinal { get; set; }

        [Column("riesgos_detectados")]
        public string? RiesgosDetectados { get; set; }

        [Column("recomendaciones_clinicas")]
        public string? RecomendacionesClinicas { get; set; }

        [Column("fecha_generacion")]
        public DateTime FechaGeneracion { get; set; }

        [Required]
        [Column("paciente_id")]
        [ForeignKey("Paciente")]
        public int PacienteId { get; set; }

        public Paciente? Paciente { get; set; }
    }
}
