using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Models
{
    [Table("PrediccionesIA")]
    public class PrediccionIA
    {       
        public int Id { get; set; }
        public string? Dni { get; set; }
        public int edad { get; set; }
        public string? sexo { get; set; }
        public float peso { get; set; }
        public float talla { get; set; }
        public int presion_sistolica { get; set; }
        public int presion_diastolica { get; set; }
        public int frecuencia_cardiaca { get; set; }
        public int frecuencia_respiratoria { get; set; }
        public float temperatura { get; set; }
        public int saturacion_oxigeno { get; set; }
        public float imc { get; set; }
        public int glucosa_capilar { get; set; }
        public string? variable_auxiliar { get; set; }
        public string? antecedente_uso_ains { get; set; }
        public string? alergias_conocidas { get; set; }
        public string? fatiga { get; set; }
        public string? antecedente_eda { get; set; }
        public string? antecedentes_diabetes_familiar { get; set; }
        public string? perdida_peso_no_intencional { get; set; }
        public string? fatiga_2 { get; set; }
        public string? nauseas { get; set; }
        public string? antecedente_tabaquismo { get; set; }
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
        public string? frecuencia_ultraprocesados { get; set; }
        public int cantidad_comidas_dia { get; set; }
        public string? perdida_peso_nutricion { get; set; }
        public string? sintoma_deficiencia_nutricional { get; set; }
        public string? frutas_verduras { get; set; }
        public int cantidadAguaDia { get; set; }
        public string? DiagnosticoFinal { get; set; }
        public string? DiagnosticoMedico { get; set; }
        public string? DiagnosticoGastro { get; set; }
        public string? DiagnosticoNutri { get; set; }
        public string? Porcentaje { get; set; }
    }
}
