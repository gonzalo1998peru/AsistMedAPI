using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistMedAPI.Models;
using AsistMedAPI.Models.DTO;
using AsistMedAPI.Data;
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

        // Endpoint para devolver informe clínico completo desde la BD
        [HttpGet("{pacienteId}")]
        [ProducesResponseType(typeof(InformeClinicoResultadoDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<InformeClinicoResultadoDto>> ObtenerInformePorPaciente(int pacienteId)
        {
            var paciente = await _context.Pacientes.FindAsync(pacienteId);
            if (paciente == null)
                return NotFound("Paciente no encontrado");

            var signos = await _context.SignosVitales.FirstOrDefaultAsync(s => s.PacienteId == pacienteId);
            var clinica = await _context.EvaluacionesClinicas.FirstOrDefaultAsync(c => c.PacienteId == pacienteId);
            var sintomas = await _context.SintomasDigestivos.FirstOrDefaultAsync(s => s.PacienteId == pacienteId);
            var nutricion = await _context.EvaluacionesNutricionales.FirstOrDefaultAsync(n => n.PacienteId == pacienteId);
            var resultado = await _context.PrediccionesIA.FirstOrDefaultAsync(r => r.PacienteId == pacienteId);

            if (resultado == null)
                return NotFound("No hay predicción para este paciente.");

            var informe = new InformeClinicoResultadoDto
            {
                Fecha = resultado.FechaPrediccion,
                DNI = paciente.DNI,
                HistorialGeneral = clinica?.ObservacionesGenerales ?? "Sin datos",
                HistorialGastroenterologico = sintomas?.NotasEspecialista ?? "Sin datos",
                HistorialNutricional = nutricion != null
                    ? $"Consume {nutricion.ConsumoUltraprocesados} ultraprocesados, toma {nutricion.CantidadAguaDia} vasos de agua al día."
                    : "Sin datos",
                DiagnosticoMedico = resultado.EnfermedadPredicha,
                DiagnosticoGastro = resultado.EnfermedadPredicha,
                DiagnosticoNutricional = "Evaluación nutricional pendiente",
                FactoresDeRiesgo = resultado.Factores ?? "No registrados",
                ResultadoTexto = $"Se predice con {resultado.PorcentajeConfianza ?? 0}% la enfermedad: {resultado.EnfermedadPredicha}"
            };

            return Ok(informe);
        }
    }
}
