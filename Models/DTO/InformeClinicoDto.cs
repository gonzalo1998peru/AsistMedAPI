using System;
using AsistMedAPI.Models.DTO;


namespace AsistMedAPI.Models.DTO
{
    public class InformeClinicoDto
    {
        public int PacienteId { get; set; }

        // Datos de signos vitales
        public string? PresionArterial { get; set; }
        public int? FrecuenciaCardiaca { get; set; }
        public int? FrecuenciaRespiratoria { get; set; }
        public double? Temperatura { get; set; }
        public int? SaturacionOxigeno { get; set; }
        public double? TallaCm { get; set; }
        public double? PesoKg { get; set; }
        public double? IMC { get; set; }
        public double? GlucosaMgDl { get; set; }

        // Datos de evaluación clínica
        public bool? Fatiga { get; set; }
        public bool? DolorAbdominal { get; set; }
        public bool? MedicamentosActuales { get; set; }
        public bool? Alergias { get; set; }
        public bool? AntecedentesPatologicos { get; set; }

        // Datos de síntomas digestivos
        public bool? DolorEstomacal { get; set; }
        public bool? Nausea { get; set; }
        public bool? Vomito { get; set; }
        public bool? ArdorEstomacal { get; set; }
        public bool? SangradoDigestivo { get; set; }
        public string? ZonaDolor { get; set; }
        public string? FrecuenciaDolor { get; set; }

        // Datos de evaluación nutricional
        public bool? ConsumoUltraprocesados { get; set; }
        public int? CantidadAguaDia { get; set; }
        public int? FrutasPorSemana { get; set; }
        public int? VerdurasPorSemana { get; set; }
        public int? ComidasAlDia { get; set; }
        public bool? DesayunoDiario { get; set; }
        public bool? Refrigerio { get; set; }

        // Nuevas propiedades requeridas por el controlador
        public string? DiagnosticoFinal { get; set; }
        public string? RiesgoDigestivo { get; set; }
        public string? RiesgoNutricional { get; set; }
        public string? RiesgoClinico { get; set; }
        public string? RecomendacionesClinicas { get; set; }

        public DateTime FechaEvaluacion { get; set; }

        
        //Observaciones clínicas descriptivas (para el PDF y la BD)

        public string? ObservacionClinica { get; set; }
        public string? ObservacionSintomas { get; set; }
        public string? ObservacionNutricional { get; set; }
        public string? ObservacionSignosVitales { get; set; }
        public string? ObservacionGeneral { get; set; }
    }
}
