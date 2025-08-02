namespace AsistMedAPI.Models.DTO
{

    public class EvaluacionClinicaDto
    {
        public int PacienteId { get; set; }
        public bool? MedicamentosActuales { get; set; }
        public bool? Alergias { get; set; }
        public bool? Fatiga { get; set; }
        public bool? AntecedenteEDA { get; set; }
        public bool? AntecedenteDiabetesFamiliar { get; set; }
        public bool? PerdidaPesoNoIntencional { get; set; }
        public bool? AntecedenteUsoAINS { get; set; }
        public bool? AntecedenteTabaquismo { get; set; }
        public bool? Nauseas { get; set; }
        public string? ObservacionesGenerales { get; set; }
    }
}