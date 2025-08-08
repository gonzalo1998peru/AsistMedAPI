using Microsoft.AspNetCore.Mvc;
using AsistMedAPI.Models;
using AsistMedAPI.Data;
using Microsoft.EntityFrameworkCore; 

namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionGeneralMedController : ControllerBase
    {
        private readonly AsistMedContext _context;

        public EvaluacionGeneralMedController(AsistMedContext context)
        {
            _context = context;
        }

        // Solo registro (POST)
        [HttpPost]
        public async Task<IActionResult> RegistrarEvaluacion([FromBody] EvaluacionGeneralMed evaluacion)
        {
            try
            {
                await _context.EvaluacionGeneralMed.AddAsync(evaluacion);
                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Evaluación médica registrada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar evaluación: {ex.Message}");
            }
        }

        // GET: Obtener por DNI
        [HttpGet("{dni}")]
        public async Task<IActionResult> ObtenerPorDni(string dni)
        {
            var entidad = await _context.EvaluacionGeneralMed
                .Where(e => e.Dni == dni)
                .OrderByDescending(e => e.Id)
                .FirstOrDefaultAsync();

            if (entidad == null)
                return NotFound("Evaluación médica no encontrada.");
            return Ok(entidad);
        }

        // PUT: Actualizar por ID
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] EvaluacionGeneralMed nuevaData)
        {
            var actual = await _context.EvaluacionGeneralMed.FindAsync(id);
            if (actual == null)
                return NotFound("Evaluación no encontrada para actualizar.");

            actual.antecedente_uso_ains = nuevaData.antecedente_uso_ains;
            actual.alergias_conocidas = nuevaData.alergias_conocidas;
            actual.fatiga = nuevaData.fatiga;
            actual.antecedente_eda = nuevaData.antecedente_eda;
            actual.antecedentes_diabetes_familiar = nuevaData.antecedentes_diabetes_familiar;
            actual.perdida_peso_no_intencional = nuevaData.perdida_peso_no_intencional;
            actual.fatiga_2 = nuevaData.fatiga_2;
            actual.nauseas = nuevaData.nauseas;
            actual.antecedente_tabaquismo = nuevaData.antecedente_tabaquismo;
            actual.observaciones = nuevaData.observaciones;

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Evaluación médica actualizada correctamente." });
        }

        // DELETE: Eliminar por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var entidad = await _context.EvaluacionGeneralMed.FindAsync(id);
            if (entidad == null)
                return NotFound("Evaluación no encontrada para eliminar.");

            _context.EvaluacionGeneralMed.Remove(entidad);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}