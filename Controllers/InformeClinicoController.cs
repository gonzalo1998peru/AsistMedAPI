using Microsoft.AspNetCore.Mvc;
using AsistMedAPI.Models;
using AsistMedAPI.Models.DTO;
using AsistMedAPI.Data; // Asegúrate de tener esto para acceder a tu contexto
using System.Threading.Tasks;

namespace AsistMedAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InformeClinicoController : ControllerBase
    {
        private readonly AsistMedContext _context;

        public InformeClinicoController(AsistMedContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(typeof(InformeClinicoResultadoDto), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<InformeClinicoResultadoDto>> GenerarInforme([FromBody] InformeClinicoDto datos)
        {
            if (datos == null)
                return BadRequest("Datos clínicos incompletos.");

            var informe = new InformeClinicoResultadoDto
            {
                DiagnosticoFinal = datos.DiagnosticoFinal,
                RiesgosDetectados = $"Riesgo digestivo: {datos.RiesgoDigestivo?.ToLower()}, " +
                                    $"riesgo nutricional: {datos.RiesgoNutricional?.ToLower()}, " +
                                    $"riesgo clínico: {datos.RiesgoClinico?.ToLower()}",
                RecomendacionesClinicas = datos.RecomendacionesClinicas,
                FechaGeneracion = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                PacienteId = datos.PacienteId
            };

            // Armar el historial clínico descriptivo
            var descripcion = $"[CLÍNICO] {datos.ObservacionClinica}\n" +
                              $"[DIGESTIVO] {datos.ObservacionSintomas}\n" +
                              $"[NUTRICIONAL] {datos.ObservacionNutricional}\n" +
                              $"[SIGNOS VITALES] {datos.ObservacionSignosVitales}\n" +
                              $"[OBSERVACIÓN GENERAL] {datos.ObservacionGeneral}";

            // Guardar en la base de datos
            var prediccion = new PrediccionIA
            {
                PacienteId = datos.PacienteId,
                RiesgoDigestivo = datos.RiesgoDigestivo,
                RiesgoNutricional = datos.RiesgoNutricional,
                RiesgoClinico = datos.RiesgoClinico,
                EnfermedadPredicha = datos.DiagnosticoFinal,
                PorcentajeConfianza = null, // Se completará con la IA real
                FechaPrediccion = DateTime.UtcNow,
                HistorialDescriptivo = descripcion,
                InformePdf = null // Se generará luego con PDF
            };

            _context.PrediccionesIA.Add(prediccion);
            await _context.SaveChangesAsync();

            return Ok(informe);
        }
    }
}
