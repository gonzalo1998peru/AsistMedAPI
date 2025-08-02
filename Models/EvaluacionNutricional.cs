using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Models
{
    [Table("evaluacion_nutricional")]
    public class EvaluacionNutricional
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("paciente_id")]
        [ForeignKey("Paciente")]
        public int PacienteId { get; set; }

        public Paciente? Paciente { get; set; }

        [Column("consumo_ultraprocesados")]
        public bool? ConsumoUltraprocesados { get; set; }

        [Column("cantidad_agua_dia")]
        public int? CantidadAguaDia { get; set; }

        [Column("frutas_por_semana")]
        public int? FrutasPorSemana { get; set; }

        [Column("verduras_por_semana")]
        public int? VerdurasPorSemana { get; set; }

        [Column("comidas_al_dia")]
        public int? ComidasAlDia { get; set; }

        [Column("desayuno_diario")]
        public bool? DesayunoDiario { get; set; }

        [Column("refrigerio")]
        public bool? Refrigerio { get; set; }

        [Column("fecha_evaluacion")]
        public DateTime FechaEvaluacion { get; set; }

        [Column("observaciones")]
        public string? Observaciones { get; set; }

    }
}
