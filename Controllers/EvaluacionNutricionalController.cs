using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistMedAPI.Data;
using AsistMedAPI.Models;

namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionNutricionalController : ControllerBase
    {
        private readonly AsistMedContext _context;

        public EvaluacionNutricionalController(AsistMedContext context)
        {
            _context = context;
        }

        // GET: api/EvaluacionNutricional
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluacionNutricional>>> GetEvaluaciones()
        {
            return await _context.EvaluacionesNutricionales
                .Include(e => e.Paciente) // Opcional: si quieres mostrar datos del paciente
                .ToListAsync();
        }

        // GET: api/EvaluacionNutricional/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluacionNutricional>> GetEvaluacion(int id)
        {
            var evaluacion = await _context.EvaluacionesNutricionales
                .Include(e => e.Paciente)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evaluacion == null)
                return NotFound();

            return evaluacion;
        }

        // POST: api/EvaluacionNutricional
        [HttpPost]
        public async Task<ActionResult<EvaluacionNutricional>> PostEvaluacion(EvaluacionNutricional evaluacion)
        {
            evaluacion.FechaEvaluacion = DateTime.SpecifyKind(evaluacion.FechaEvaluacion, DateTimeKind.Utc);

            // Validar que paciente exista
            var existePaciente = await _context.Pacientes.AnyAsync(p => p.Id == evaluacion.PacienteId);
            if (!existePaciente)
                return BadRequest("El paciente_id ingresado no existe en la tabla paciente.");

            _context.EvaluacionesNutricionales.Add(evaluacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvaluacion), new { id = evaluacion.Id }, evaluacion);
        }

        // PUT: api/EvaluacionNutricional/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluacion(int id, EvaluacionNutricional evaluacion)
        {
            if (id != evaluacion.Id)
                return BadRequest("El ID de la URL no coincide con el del cuerpo del objeto.");

            evaluacion.FechaEvaluacion = DateTime.SpecifyKind(evaluacion.FechaEvaluacion, DateTimeKind.Utc);
            _context.Entry(evaluacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.EvaluacionesNutricionales.AnyAsync(e => e.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/EvaluacionNutricional/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluacion(int id)
        {
            var evaluacion = await _context.EvaluacionesNutricionales.FindAsync(id);
            if (evaluacion == null)
                return NotFound();

            _context.EvaluacionesNutricionales.Remove(evaluacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
