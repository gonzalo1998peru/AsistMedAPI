namespace AsistMedAPI.Models.DTO
{
    public class PrediccionEntradaDto
    {
        public string Dni { get; set; } = string.Empty;

        public int edad { get; set; }
        public string sexo { get; set; } = "F";
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
        public string variable_auxiliar { get; set; } = "";
        public string antecedente_uso_ains { get; set; } = "No";
        public string alergias_conocidas { get; set; } = "No";
        public string fatiga { get; set; } = "No";
        public string antecedente_eda { get; set; } = "No";
        public string antecedentes_diabetes_familiar { get; set; } = "No";
        public string perdida_peso_no_intencional { get; set; } = "No";
        public string fatiga_2 { get; set; } = "No";
        public string nauseas { get; set; } = "No";
        public string antecedente_tabaquismo { get; set; } = "No";
        public int duracion_sintomas_dias { get; set; }
        public string dolor_abdominal { get; set; } = "No";
        public string zona_dolor_abdominal { get; set; } = "No";
        public string cambios_deposiciones { get; set; } = "No";
        public string sangrado_digestivo { get; set; } = "No";
        public string infecciones_recientes { get; set; } = "No";
        public string perdida_apetito { get; set; } = "No";
        public string vomitos { get; set; } = "No";
        public string distension_abdominal { get; set; } = "No";
        public string diarrea { get; set; } = "No";
        public string reflujo_gastroesofagico { get; set; } = "No";
        public string antecedente_gastritis { get; set; } = "No";
        public string antecedente_ulcera { get; set; } = "No";
        public string antecedente_colitis { get; set; } = "No";
        public string frecuencia_ultraprocesados { get; set; } = "Baja";
        public int cantidad_comidas_dia { get; set; } = 3;
        public string perdida_peso_nutricion { get; set; } = "No";
        public string sintoma_deficiencia_nutricional { get; set; } = "No";
        public string frutas_verduras { get; set; } = "No";
        public int cantidadAguaDia { get; set; } = 2;

            public string? DiagnosticoFinal { get; set; }
            public string? DiagnosticoMedico { get; set; }
            public string? DiagnosticoGastro { get; set; }
            public string? DiagnosticoNutri { get; set; }
            public string? Porcentaje { get; set; }

    }
}
