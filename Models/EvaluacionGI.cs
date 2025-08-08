using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Models
{
    [Table("EvaluacionGI")]
    public class EvaluacionGI
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Dni { get; set; } = string.Empty;

        public int duracion_sintomas_dias { get; set; }
        public string? dolor_abdominal { get; set; }
        public string? zona_dolor_abdominal { get; set; }
        public string? cambios_deposiciones { get; set; }
        public string? sangrado_digestivo { get; set; }
        public string? infecciones_recientes { get; set; }
        public string? perdida_apetito { get; set; }
        public string? vomitos { get; set; }
        public string? distension_abdominal { get; set; }
        public string? diarrea { get; set; }
        public string? reflujo_gastroesofagico { get; set; }
        public string? antecedente_gastritis { get; set; }
        public string? antecedente_ulcera { get; set; }
        public string? antecedente_colitis { get; set; }
        public string? observaciones { get; set; }
    }
}