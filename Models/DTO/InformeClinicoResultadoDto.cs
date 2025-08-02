using System;

namespace AsistMedAPI.Models.DTO
{
    public class InformeClinicoResultadoDto
    {
        public DateTime Fecha { get; set; }
        public string? DNI { get; set; }

        public string? HistorialGeneral { get; set; }
        public string? HistorialGastroenterologico { get; set; }
        public string? HistorialNutricional { get; set; }

        public string? DiagnosticoMedico { get; set; }
        public string? DiagnosticoGastro { get; set; }
        public string? DiagnosticoNutricional { get; set; }

        public string? FactoresDeRiesgo { get; set; }

        public string? ResultadoTexto { get; set; }
    }
}
