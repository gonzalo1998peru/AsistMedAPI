using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AsistMedAPI.Models
{
    [Table("signos_vitales")]
    public class SignosVitales
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Paciente")]
        [Column("paciente_id")]
        public int PacienteId { get; set; }

        public Paciente? Paciente { get; set; }

        [Column("presion_arterial")]
        [JsonPropertyName("presion")]
        public string? PresionArterial { get; set; }

        [Column("frecuencia_cardiaca")]
        [JsonPropertyName("frecuenciaCardiaca")]
        public int? FrecuenciaCardiaca { get; set; }

        [Column("frecuencia_respiratoria")]
        [JsonPropertyName("frecuenciaRespiratoria")]
        public int? FrecuenciaRespiratoria { get; set; }

        [Column("temperatura")]
        [JsonPropertyName("temperatura")]
        public double? Temperatura { get; set; }

        [Column("saturacion_oxigeno")]
        [JsonPropertyName("saturacionOxigeno")]
        public int? SaturacionOxigeno { get; set; }

        [Column("talla_cm")]
        [JsonPropertyName("talla")]
        public double? Talla { get; set; }

        [Column("peso_kg")]
        [JsonPropertyName("peso")]
        public double? Peso { get; set; }

        [Column("imc")]
        [JsonPropertyName("imc")]
        public double? IMC { get; set; }

        [Column("glucosa_mg_dl")]
        [JsonPropertyName("glucosa")]
        public double? Glucosa { get; set; }

        [Column("fecha_registro")]
        [JsonPropertyName("fechaRegistro")]
        public DateTime FechaRegistro { get; set; }
    }
}
