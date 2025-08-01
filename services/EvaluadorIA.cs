using AsistMedAPI.Models.DTO;
using System;

namespace AsistMedAPI.Services
{
    public class EvaluadorIA
    {
        public ResultadoPrediccionDto Evaluar(EvaluacionCompletaDto datos)
        {
            string enfermedad = "No determinada";
            string factores = "";
            double probabilidad = 0.0;

            // Reglas simples para enfermedad principal
            if (datos.DolorEstomacal && datos.SangradoDigestivo)
            {
                enfermedad = "Úlcera gástrica";
                factores = "Dolor + Sangrado";
                probabilidad = 85.5;
            }
            else if (datos.ConsumoUltraprocesados && datos.GlucosaMgDl > 110)
            {
                enfermedad = "Síndrome Metabólico";
                factores = "Dieta poco saludable + Glucosa elevada";
                probabilidad = 75.0;
            }
            else if (datos.Nausea && datos.Vomito && datos.ArdorEstomacal)
            {
                enfermedad = "Gastritis";
                factores = "Síntomas digestivos agudos";
                probabilidad = 65.2;
            }

            return new ResultadoPrediccionDto
            {
                Enfermedad = enfermedad,
                Porcentaje = probabilidad,
                Factores = factores
            };
        }

        // Riesgo Digestivo
        public string CalcularRiesgoDigestivo(EvaluacionCompletaDto datos)
        {
            int sintomas = 0;
            if (datos.DolorEstomacal) sintomas++;
            if (datos.Nausea) sintomas++;
            if (datos.Vomito) sintomas++;
            if (datos.SangradoDigestivo) sintomas++;
            if (datos.ArdorEstomacal) sintomas++;

            if (sintomas >= 4) return "Alto";
            if (sintomas >= 2) return "Moderado";
            return "Bajo";
        }

        // Riesgo Clínico
        public string CalcularRiesgoClinico(EvaluacionCompletaDto datos)
        {
            int score = 0;
            if (datos.Fatiga) score++;
            if (datos.MedicamentosActuales) score++;
            if (datos.Alergias) score++;
            if (datos.AntecedentesPatologicos) score++;

            if (score >= 3) return "Alto";
            if (score == 2) return "Moderado";
            return "Bajo";
        }

        // Riesgo Nutricional
        public string CalcularRiesgoNutricional(EvaluacionCompletaDto datos)
        {
            int score = 0;
            if (datos.ConsumoUltraprocesados) score++;
            if (datos.CantidadAguaDia < 4) score++;
            if (datos.Imc >= 30 || datos.Imc < 18.5) score++;  // obesidad o desnutrición

            if (score == 3) return "Alto";
            if (score == 2) return "Moderado";
            return "Bajo";
        }
    }
}
