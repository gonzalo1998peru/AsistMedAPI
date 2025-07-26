using System;

namespace AsistMedAPI.Models.DTO
{
    public class InformeClinicoResultadoDto
    {
        public string? DiagnosticoFinal { get; set; }
        public string? RiesgosDetectados { get; set; }
        public string? RecomendacionesClinicas { get; set; }
        public string FechaGeneracion { get; set; } = string.Empty;
        public int PacienteId { get; set; }
    }
}
