namespace AsistMedAPI.Models.DTO
{
    public class EvaluacionCompletaDto
    {
        public int PacienteId { get; set; }

        // Signos Vitales
        public int PresionSistolica { get; set; }
        public int PresionDiastolica { get; set; }
        public int FrecuenciaCardiaca { get; set; }
        public int FrecuenciaRespiratoria { get; set; }
        public double Temperatura { get; set; }
        public double SaturacionOxigeno { get; set; }
        public double TallaCm { get; set; }
        public double PesoKg { get; set; }
        public double Imc { get; set; }
        public double GlucosaMgDl { get; set; }

        // Evaluación Clínica
        public bool Fatiga { get; set; }
        public bool DolorAbdominal { get; set; }
        public bool MedicamentosActuales { get; set; }
        public bool Alergias { get; set; }
        public bool AntecedentesPatologicos { get; set; }

        // Síntomas Digestivos
        public bool DolorEstomacal { get; set; }
        public bool Nausea { get; set; }
        public bool Vomito { get; set; }
        public bool ArdorEstomacal { get; set; }
        public bool SangradoDigestivo { get; set; }

        public bool? Estreñimiento { get; set; }
        public string ZonaDolor { get; set; } = string.Empty;
        public string FrecuenciaDolor { get; set; } = string.Empty;

        // Evaluación Nutricional
        public bool ConsumoUltraprocesados { get; set; }
        public int CantidadAguaDia { get; set; }
    }
}
