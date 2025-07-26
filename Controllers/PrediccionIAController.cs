using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistMedAPI.Data;
using AsistMedAPI.Models;

namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrediccionIAController : ControllerBase
    {
        private readonly AsistMedContext _context;

        public PrediccionIAController(AsistMedContext context)
        {
            _context = context;
        }

        // GET: api/PrediccionIA
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrediccionIA>>> GetPrediccionesIA()
        {
            return await _context.PrediccionesIA
                .Include(p => p.Paciente)
                .ToListAsync();
        }

        // GET: api/PrediccionIA/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrediccionIA>> GetPrediccionIA(int id)
        {
            var prediccion = await _context.PrediccionesIA
                .Include(p => p.Paciente)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prediccion == null)
                return NotFound();

            return prediccion;
        }

        // POST: api/PrediccionIA
        [HttpPost]
        public async Task<ActionResult<PrediccionIA>> PostPrediccionIA(PrediccionIA prediccion)
        {
            prediccion.FechaPrediccion = DateTime.SpecifyKind(prediccion.FechaPrediccion, DateTimeKind.Utc);

            _context.PrediccionesIA.Add(prediccion);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("violates foreign key") == true)
                    return BadRequest("El paciente_id no existe en la tabla Paciente.");
                throw;
            }

            return CreatedAtAction(nameof(GetPrediccionIA), new { id = prediccion.Id }, prediccion);
        }

        // PUT: api/PrediccionIA/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrediccionIA(int id, PrediccionIA prediccion)
        {
            if (id != prediccion.Id)
                return BadRequest("El ID de la URL no coincide con el objeto.");

            prediccion.FechaPrediccion = DateTime.SpecifyKind(prediccion.FechaPrediccion, DateTimeKind.Utc);

            _context.Entry(prediccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.PrediccionesIA.AnyAsync(e => e.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/PrediccionIA/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrediccionIA(int id)
        {
            var prediccion = await _context.PrediccionesIA.FindAsync(id);
            if (prediccion == null)
                return NotFound();

            _context.PrediccionesIA.Remove(prediccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
