using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Models
{
    [Table("EvaluacionGeneralMed")]
    public class EvaluacionGeneralMed
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Dni { get; set; } = string.Empty;

        public string? antecedente_uso_ains { get; set; }
        public string? alergias_conocidas { get; set; }
        public string? fatiga { get; set; }
        public string? antecedente_eda { get; set; }
        public string? antecedentes_diabetes_familiar { get; set; }
        public string? perdida_peso_no_intencional { get; set; }
        public string? fatiga_2 { get; set; }
        public string? nauseas { get; set; }
        public string? antecedente_tabaquismo { get; set; }

        public string? observaciones { get; set; }
    }
}