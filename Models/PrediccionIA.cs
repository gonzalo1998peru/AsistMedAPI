using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Models
{
    [Table("prediccion_ia")]
    public class PrediccionIA
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("paciente_id")]
        [ForeignKey("Paciente")]
        public int PacienteId { get; set; }

        public Paciente? Paciente { get; set; }

        [Column("riesgo_digestivo")]
        public string? RiesgoDigestivo { get; set; }

        [Column("riesgo_nutricional")]
        public string? RiesgoNutricional { get; set; }

        [Column("riesgo_clinico")]
        public string? RiesgoClinico { get; set; }

        [Column("enfermedad_predicha")]
        public string? EnfermedadPredicha { get; set; }

        [Column("porcentaje_confianza")]
        public double? PorcentajeConfianza { get; set; }

        [Column("fecha_prediccion")]
        public DateTime FechaPrediccion { get; set; }

        [Column("informe_pdf")]
        public string? InformePdf { get; set; }

        [Column("historial_descriptivo")]
        public string? HistorialDescriptivo { get; set; }

        [Column("factores_riesgo")]
        public string? Factores { get; set; }

        
        
        

    }
}
