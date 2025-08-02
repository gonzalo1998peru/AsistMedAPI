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

        

        [Column("nausea")]
        public bool? Nausea { get; set; }

        [Column("vomito")]
        public bool? Vomito { get; set; }

        [Column("ardor_estomacal")]
        public bool? ArdorEstomacal { get; set; }

        [Column("sangrado_digestivo")]
        public bool? SangradoDigestivo { get; set; }

        [Column("dolor_abdominal")]
        public bool? DolorAbdominal { get; set; }


        [Column("zona_dolor_abdominal")]
        public string? ZonaDolorAbdominal { get; set; }

        // ✅ NUEVO CAMPO AGREGADO:
        [Column("estrenimiento")]
        public bool? Estreñimiento { get; set; }

        [Column("duracion_sintomas_dias")]
        public int? DuracionSintomasDias { get; set; }

        [Column("cambios_deposiciones")]
        public bool? CambiosDeposiciones { get; set; }

        [Column("infecciones_recientes")]
        public bool? InfeccionesRecientes { get; set; }


        [Column("perdida_apetito")]
        public bool? PerdidaApetito { get; set; }

        [Column("distension_abdominal")]
        public bool? DistensionAbdominal { get; set; }

        [Column("notas_especialista")]
        public string? NotasEspecialista { get; set; }

        [Column("diarrea")]
        public bool? Diarrea { get; set; }

        [Column("reflujo_gastroesofagico")]
        public bool? ReflujoGastroesofagico { get; set; }

        [Column("antecedente_gastritis")]
        public bool? AntecedenteGastritis { get; set; }

        [Column("antecedente_ulcera")]
        public bool? AntecedenteUlcera { get; set; }

        [Column("antecedente_colitis")]
        public bool? AntecedenteColitis { get; set; }

        [Column("fecha_evaluacion")]
        public DateTime FechaEvaluacion { get; set; }
        
        

    }
}
