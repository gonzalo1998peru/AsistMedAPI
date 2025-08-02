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
        

        [Column("medicamentos_actuales")]
        public bool? MedicamentosActuales { get; set; }

        [Column("alergias")]
        public bool? Alergias { get; set; }

        [Column("fatiga")]
        public bool? Fatiga { get; set; }

        [Column("antecedente_eda")]
        public bool? AntecedenteEDA { get; set; }

        [Column("antecedente_diabetes_familiar")]
        public bool? AntecedenteDiabetesFamiliar { get; set; }

        [Column("perdida_peso_no_intencional")]
        public bool? PerdidaPesoNoIntencional { get; set; }

        [Column("antecedente_uso_ains")]
        public bool? AntecedenteUsoAINS { get; set; }

        [Column("antecedente_tabaquismo")]
        public bool? AntecedenteTabaquismo { get; set; }

        [Column("nauseas")]
        public bool? Nauseas { get; set; }

        [Column("observaciones_generales")]
        public string? ObservacionesGenerales { get; set; }

        [Column("fecha_evaluacion")]
        public DateTime FechaEvaluacion { get; set; }
    }
}
