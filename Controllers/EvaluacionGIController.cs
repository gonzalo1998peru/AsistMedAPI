using Microsoft.AspNetCore.Mvc;
using AsistMedAPI.Models;
using AsistMedAPI.Data;
using Microsoft.EntityFrameworkCore;


namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionGIController : ControllerBase
    {
        private readonly AsistMedContext _context;

        public EvaluacionGIController(AsistMedContext context)
        {
            _context = context;
        }

        // Solo registro (POST)
        [HttpPost]
        public async Task<IActionResult> RegistrarEvaluacion([FromBody] EvaluacionGI evaluacion)
        {
            try
            {
                await _context.EvaluacionGI.AddAsync(evaluacion);
                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Evaluación GI registrada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar evaluación GI: {ex.Message}");
            }
        }


         // GET: Obtener evaluación por DNI
        [HttpGet("{dni}")]
        public async Task<IActionResult> ObtenerPorDni(string dni)
        {
            var entidad = await _context.EvaluacionGI
                .Where(e => e.Dni == dni)
                .OrderByDescending(e => e.Id)
                .FirstOrDefaultAsync();

            if (entidad == null)
                return NotFound("Evaluación GI no encontrada.");
            return Ok(entidad);
        }

        // PUT: Actualizar por ID
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] EvaluacionGI nuevaData)
        {
            var actual = await _context.EvaluacionGI.FindAsync(id);
            if (actual == null)
                return NotFound("Evaluación GI no encontrada para actualizar.");

            actual.duracion_sintomas_dias = nuevaData.duracion_sintomas_dias;
            actual.dolor_abdominal = nuevaData.dolor_abdominal;
            actual.zona_dolor_abdominal = nuevaData.zona_dolor_abdominal;
            actual.cambios_deposiciones = nuevaData.cambios_deposiciones;
            actual.sangrado_digestivo = nuevaData.sangrado_digestivo;
            actual.infecciones_recientes = nuevaData.infecciones_recientes;
            actual.perdida_apetito = nuevaData.perdida_apetito;
            actual.vomitos = nuevaData.vomitos;
            actual.distension_abdominal = nuevaData.distension_abdominal;
            actual.diarrea = nuevaData.diarrea;
            actual.reflujo_gastroesofagico = nuevaData.reflujo_gastroesofagico;
            actual.antecedente_gastritis = nuevaData.antecedente_gastritis;
            actual.antecedente_ulcera = nuevaData.antecedente_ulcera;
            actual.antecedente_colitis = nuevaData.antecedente_colitis;
            actual.observaciones = nuevaData.observaciones;

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Evaluación GI actualizada correctamente." });
        }

        // DELETE: Eliminar por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var entidad = await _context.EvaluacionGI.FindAsync(id);
            if (entidad == null)
                return NotFound("Evaluación GI no encontrada para eliminar.");

            _context.EvaluacionGI.Remove(entidad);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}