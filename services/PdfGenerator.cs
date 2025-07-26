using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using AsistMedAPI.Models;

namespace AsistMedAPI.Services
{
    public static class PdfGenerator
    {
        public static byte[] GenerarInformePdf(
            Paciente paciente,
            SignosVitales signos,
            EvaluacionClinica clinica,
            SintomasDigestivos sintomas,
            EvaluacionNutricional nutricion,
            PrediccionIA prediccion)
        {
            // Declaración obligatoria para evitar excepción de licencia
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content().Column(col =>
                    {
                        col.Item().Text("PREDICCIÓN DE ENFERMEDADES GASTROINTESTINALES")
                            .Bold().FontSize(18).AlignCenter();

                        col.Item().Text($"Fecha: {prediccion.FechaPrediccion:dd/MM/yyyy}   DNI: {paciente.DNI}")
                            .FontSize(10).AlignRight();

                        col.Item().PaddingVertical(5).LineHorizontal(1);

                        col.Item().Text("HISTORIAL GENERAL").Bold();
                        col.Item().Text(FormatearHistorialGeneral(clinica));

                        col.Item().Text("HISTORIAL GASTROENTEROLÓGICO").Bold();
                        col.Item().Text(FormatearHistorialGastro(sintomas));

                        col.Item().Text("HISTORIAL NUTRICIONAL").Bold();
                        col.Item().Text(FormatearHistorialNutricional(nutricion));

                        col.Item().Text("DIAGNÓSTICO").Bold();
                        col.Item().Text(prediccion.HistorialDescriptivo ?? "No disponible");

                        col.Item().Text("FACTORES DE RIESGO").Bold();
                        col.Item().Text(FormatearFactoresDeRiesgo(prediccion));

                        col.Item().Text("RESULTADO").Bold();
                        col.Item().Text(t =>
                        {
                            t.Span($"{prediccion.PorcentajeConfianza}% predice la enfermedad: ");
                            t.Span(prediccion.EnfermedadPredicha ?? "No determinada").Bold();
                        });
                    });
                });
            }).GeneratePdf();
        }

        // Métodos auxiliares para formatear texto clínico
        private static string FormatearHistorialGeneral(EvaluacionClinica clinica) =>
            $"Fatiga: {(clinica.Fatiga == true ? "Sí" : "No")}, " +
            $"Dolor Abdominal: {(clinica.DolorAbdominal == true ? "Sí" : "No")}, " +
            $"Alergias: {(clinica.Alergias == true ? "Sí" : "No")}";

        private static string FormatearHistorialGastro(SintomasDigestivos sintomas) =>
            $"Dolor estomacal: {(sintomas.DolorEstomacal == true ? "Sí" : "No")}, " +
            $"Náusea: {(sintomas.Nausea == true ? "Sí" : "No")}, " +
            $"Vómito: {(sintomas.Vomito == true ? "Sí" : "No")}, " +
            $"Zona dolor: {sintomas.ZonaDolor}";

        private static string FormatearHistorialNutricional(EvaluacionNutricional nutricion) =>
            $"Consumo ultraprocesados: {(nutricion.ConsumoUltraprocesados == true ? "Sí" : "No")}, " +
            $"Agua/día: {nutricion.CantidadAguaDia} vasos, Comidas/día: {nutricion.ComidasAlDia}";

        private static string FormatearFactoresDeRiesgo(PrediccionIA p) =>
            $"• Riesgo clínico: {p.RiesgoClinico}\n" +
            $"• Riesgo nutricional: {p.RiesgoNutricional}\n" +
            $"• Riesgo digestivo: {p.RiesgoDigestivo}";
    }
}
