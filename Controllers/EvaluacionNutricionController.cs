using Microsoft.AspNetCore.Mvc;
using AsistMedAPI.Models;
using AsistMedAPI.Data;
using Microsoft.EntityFrameworkCore;


namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionNutricionController : ControllerBase
    {
        private readonly AsistMedContext _context;

        public EvaluacionNutricionController(AsistMedContext context)
        {
            _context = context;
        }

        // Solo registro (POST)
        [HttpPost]
        public async Task<IActionResult> RegistrarEvaluacion([FromBody] EvaluacionNutricion evaluacion)
        {
            try
            {
                await _context.EvaluacionNutricion.AddAsync(evaluacion);
                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Evaluación nutricional registrada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar evaluación nutricional: {ex.Message}");
            }
        }

        // GET: Obtener por DNI
        [HttpGet("{dni}")]
        public async Task<IActionResult> ObtenerPorDni(string dni)
        {
            var entidad = await _context.EvaluacionNutricion
                .Where(e => e.Dni == dni)
                .OrderByDescending(e => e.Id)
                .FirstOrDefaultAsync();

            if (entidad == null)
                return NotFound("Evaluación nutricional no encontrada.");
            return Ok(entidad);
        }

        // PUT: Actualizar por ID
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] EvaluacionNutricion nuevaData)
        {
            var actual = await _context.EvaluacionNutricion.FindAsync(id);
            if (actual == null)
                return NotFound("Evaluación nutricional no encontrada para actualizar.");

            actual.frecuencia_ultraprocesados = nuevaData.frecuencia_ultraprocesados;
            actual.cantidad_comidas_dia = nuevaData.cantidad_comidas_dia;
            actual.perdida_peso_nutricion = nuevaData.perdida_peso_nutricion;
            actual.sintoma_deficiencia_nutricional = nuevaData.sintoma_deficiencia_nutricional;
            actual.frutas_verduras = nuevaData.frutas_verduras;
            actual.cantidadAguaDia = nuevaData.cantidadAguaDia;
            actual.observaciones = nuevaData.observaciones;

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Evaluación nutricional actualizada correctamente." });
        }

        // DELETE: Eliminar por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var entidad = await _context.EvaluacionNutricion.FindAsync(id);
            if (entidad == null)
                return NotFound("Evaluación nutricional no encontrada para eliminar.");

            _context.EvaluacionNutricion.Remove(entidad);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}