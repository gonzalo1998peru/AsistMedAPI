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

            // 🧠 Puntuaciones por enfermedad
            int scoreGastritis = 0;
            int scoreUlceras = 0;
            int scoreMetabolico = 0;

            // 🔍 Evaluar Gastritis
            if (datos.DolorEstomacal) scoreGastritis++;
            if (datos.Nausea) scoreGastritis++;
            if (datos.Vomito) scoreGastritis++;
            if (datos.ArdorEstomacal) scoreGastritis++;

            // 🔍 Evaluar Úlcera Gástrica
            if (datos.DolorEstomacal) scoreUlceras++;
            if (datos.SangradoDigestivo) scoreUlceras++;
            if (datos.AntecedentesPatologicos) scoreUlceras++;

            // 🔍 Evaluar Síndrome Metabólico
            if (datos.ConsumoUltraprocesados) scoreMetabolico++;
            if (datos.GlucosaMgDl > 110) scoreMetabolico++;
            if (datos.Imc >= 30) scoreMetabolico++;

            // 🧾 Evaluación priorizada por mayor puntuación
            if (scoreUlceras >= 2)
            {
                enfermedad = "Úlcera gástrica";
                probabilidad = 85;
                factores = "Dolor, sangrado o antecedentes";
            }
            else if (scoreGastritis >= 2)
            {
                enfermedad = "Gastritis";
                probabilidad = 75;
                factores = "Síntomas digestivos múltiples";
            }
            else if (scoreMetabolico >= 2)
            {
                enfermedad = "Síndrome Metabólico";
                probabilidad = 65;
                factores = "Dieta y glucosa alteradas";
            }

            return new ResultadoPrediccionDto
            {
                Enfermedad = enfermedad,
                Porcentaje = probabilidad,
                Factores = factores
            };
        }

        // 📊 Riesgo Digestivo
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

        // 📊 Riesgo Clínico
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

        // 📊 Riesgo Nutricional
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
