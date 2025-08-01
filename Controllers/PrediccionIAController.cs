using Microsoft.AspNetCore.Mvc;
using AsistMedAPI.Models.DTO;
using AsistMedAPI.Services;
using AsistMedAPI.Models;
using AsistMedAPI.Data;

namespace AsistMedAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class PrediccionIAController : ControllerBase
    {
        private readonly EvaluadorIA _evaluadorIA;
        private readonly AsistMedContext _context;

        public PrediccionIAController(EvaluadorIA evaluadorIA, AsistMedContext context)
        {
            _evaluadorIA = evaluadorIA;
            _context = context;
        }

        [HttpPost("evaluar")]
        public async Task<ActionResult<ResultadoPrediccionDto>> EvaluarEnfermedad([FromBody] EvaluacionCompletaDto datos)
        {
            if (datos == null)
            {
                return BadRequest("Datos incompletos.");
            }

            // Ejecutar IA
            var resultado = _evaluadorIA.Evaluar(datos);

            // Calcular riesgos complementarios
            string riesgoDigestivo = _evaluadorIA.CalcularRiesgoDigestivo(datos);
            string riesgoClinico = _evaluadorIA.CalcularRiesgoClinico(datos);
            string riesgoNutricional = _evaluadorIA.CalcularRiesgoNutricional(datos);

            // Crear registro completo
            var prediccion = new PrediccionIA
            {
                PacienteId = datos.PacienteId,
                EnfermedadPredicha = resultado.Enfermedad,
                PorcentajeConfianza = resultado.Porcentaje,
                HistorialDescriptivo = resultado.Factores,
                FechaPrediccion = DateTime.UtcNow,
                RiesgoDigestivo = riesgoDigestivo,
                RiesgoClinico = riesgoClinico,
                RiesgoNutricional = riesgoNutricional
            };

            _context.PrediccionesIA.Add(prediccion);
            await _context.SaveChangesAsync();

            return Ok(resultado);
        }
    }
}
