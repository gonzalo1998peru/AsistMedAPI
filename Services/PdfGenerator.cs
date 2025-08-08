using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using AsistMedAPI.Models;
using System;

namespace AsistMedAPI.Services
{
    public static class PdfGenerator
    {
        public static byte[] GenerarInforme
        (

            Paciente paciente,
            SignosVitales signos,
            EvaluacionGeneralMed evaluacionGeneral,
            EvaluacionGI evaluacionGI,
            EvaluacionNutricion evaluacionNutricion,
            PrediccionIA prediccion

        )
        
        {
            var fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);

                    page.Header().Text("🩺 Informe Clínico de Predicción IA").FontSize(20).Bold().AlignCenter();

                    page.Content().PaddingVertical(15).Column(col =>
                    {
                        col.Item().Text($"📅 Fecha: {fecha}");

                        col.Item().PaddingTop(10).Text($"📌 Datos del paciente").FontSize(14).Bold();
                        col.Item().Text($"DNI: {paciente.Dni}");
                        col.Item().Text($"Edad: {paciente.edad} años");
                        col.Item().Text($"Sexo: {(paciente.sexo == "M" ? "Masculino" : "Femenino")}");

                        col.Item().PaddingTop(10).Text($"❤️ Signos Vitales").FontSize(14).Bold();
                        col.Item().Text($"Peso: {signos.peso} kg | Talla: {signos.talla} m");
                        col.Item().Text($"Presión arterial: {signos.presion_sistolica}/{signos.presion_diastolica} mmHg");
                        col.Item().Text($"F. cardíaca: {signos.frecuencia_cardiaca} bpm");
                        col.Item().Text($"F. respiratoria: {signos.frecuencia_respiratoria} rpm");
                        col.Item().Text($"Temperatura: {signos.temperatura} °C");
                        col.Item().Text($"Saturación O2: {signos.saturacion_oxigeno} %");
                        col.Item().Text($"IMC: {signos.imc:F2} | Glucosa: {signos.glucosa_capilar} mg/dL");
                        
                        col.Item().PaddingTop(10).Text("🧾 Evaluación Médica General").FontSize(14).Bold();
                        col.Item().Text($"Fatiga: {evaluacionGeneral.fatiga} | Fatiga (confirmación): {evaluacionGeneral.fatiga_2}");
                        col.Item().Text($"Náuseas: {evaluacionGeneral.nauseas} | Alergias: {evaluacionGeneral.alergias_conocidas}");
                        col.Item().Text($"EDA: {evaluacionGeneral.antecedente_eda} | Tabaquismo: {evaluacionGeneral.antecedente_tabaquismo}");
                        col.Item().Text($"Uso de AINES: {evaluacionGeneral.antecedente_uso_ains}");
                        col.Item().Text($"Pérdida de peso no intencional: {evaluacionGeneral.perdida_peso_no_intencional}");


                        if (!string.IsNullOrWhiteSpace(evaluacionGeneral.observaciones))
                        {
                            col.Item().PaddingTop(5).Text("🗒 Observaciones médicas:").Italic().FontSize(11);
                            col.Item().Text(evaluacionGeneral.observaciones);
                        }

                        col.Item().PaddingTop(10).Text($"🧻 Evaluación Gastrointestinal").FontSize(14).Bold();
                        col.Item().Text($"Dolor abdominal: {evaluacionGI.dolor_abdominal} ({evaluacionGI.zona_dolor_abdominal})");
                        col.Item().Text($"Duración síntomas: {evaluacionGI.duracion_sintomas_dias} días");
                        col.Item().Text($"Vómitos: {evaluacionGI.vomitos} | Diarrea: {evaluacionGI.diarrea}");
                        col.Item().Text($"Reflujo: {evaluacionGI.reflujo_gastroesofagico} | Distensión: {evaluacionGI.distension_abdominal}");
                        col.Item().Text($"Sangrado: {evaluacionGI.sangrado_digestivo} | Infecciones recientes: {evaluacionGI.infecciones_recientes}");

                        if (!string.IsNullOrWhiteSpace(evaluacionGI.observaciones))
                        {
                            col.Item().PaddingTop(5).Text("🗒 Observaciones GI:").Italic().FontSize(11);
                            col.Item().Text(evaluacionGI.observaciones);
                        }

                        col.Item().PaddingTop(10).Text($"🥗 Evaluación Nutricional").FontSize(14).Bold();
                        col.Item().Text($"Comidas/día: {evaluacionNutricion.cantidad_comidas_dia} | Agua/día: {evaluacionNutricion.cantidadAguaDia} vasos");
                        col.Item().Text($"Frutas/verduras: {evaluacionNutricion.frutas_verduras}");
                        col.Item().Text($"Ultraprocesados: {evaluacionNutricion.frecuencia_ultraprocesados}");

                        if (!string.IsNullOrWhiteSpace(evaluacionNutricion.observaciones))
                        {
                            col.Item().PaddingTop(5).Text("🗒 Observaciones nutricionales:").Italic().FontSize(11);
                            col.Item().Text(evaluacionNutricion.observaciones);
                        }

                        col.Item().PaddingTop(10).Text($"🤖 Resultado del Modelo IA").FontSize(14).Bold();
                        col.Item().Text($"Diagnóstico Médico: {prediccion.DiagnosticoMedico}");
                        col.Item().Text($"Diagnóstico Gastroenterológico: {prediccion.DiagnosticoGastro}");
                        col.Item().Text($"Diagnóstico Nutricional: {prediccion.DiagnosticoNutri}");
                        col.Item().Text($"✅ Diagnóstico Final: {prediccion.DiagnosticoFinal}");
                        col.Item().Text($"Precisión del modelo: {prediccion.Porcentaje}");

                        col.Item().PaddingTop(15).Text("📝 Recomendación médica:").Bold();
                        col.Item().Text("Este informe es de carácter orientativo. Se recomienda atención médica presencial si los síntomas persisten o empeoran.");
                    });

                    page.Footer().AlignCenter().Text("Sistema AsistMed | Centro de Salud Baños - Huánuco");
                });
            });

            return documento.GeneratePdf();
        }
    }
}
