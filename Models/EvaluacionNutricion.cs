using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Models
{
    [Table("EvaluacionNutricion")]
    public class EvaluacionNutricion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Dni { get; set; } = string.Empty;

        public string? frecuencia_ultraprocesados { get; set; }
        public int cantidad_comidas_dia { get; set; }
        public string? perdida_peso_nutricion { get; set; }
        public string? sintoma_deficiencia_nutricional { get; set; }
        public string? frutas_verduras { get; set; }
        public int cantidadAguaDia { get; set; }
            
        public string? observaciones { get; set; }
    }
}