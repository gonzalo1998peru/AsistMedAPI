using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistMedAPI.Data;
using AsistMedAPI.Models;

namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SintomasDigestivosController : ControllerBase
    {
        private readonly AsistMedContext _context;

        public SintomasDigestivosController(AsistMedContext context)
        {
            _context = context;
        }

        // GET: api/SintomasDigestivos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SintomasDigestivos>>> GetAll()
        {
            return await _context.SintomasDigestivos
                .Include(s => s.Paciente) // Opcional: si quieres traer datos del paciente
                .ToListAsync();
        }

        // GET: api/SintomasDigestivos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SintomasDigestivos>> GetById(int id)
        {
            var sintomas = await _context.SintomasDigestivos
                .Include(s => s.Paciente)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sintomas == null)
                return NotFound();

            return sintomas;
        }

        // POST: api/SintomasDigestivos
        [HttpPost]
        public async Task<ActionResult<SintomasDigestivos>> PostSintomaDigestivo(SintomasDigestivos sintoma)
        {
            _context.SintomasDigestivos.Add(sintoma);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("violates foreign key"))
                {
                    return BadRequest("El paciente_id ingresado no existe en la tabla paciente.");
                }
                throw;
            }

            return CreatedAtAction(nameof(GetById), new { id = sintoma.Id }, sintoma);
        }

        // PUT: api/SintomasDigestivos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSintomaDigestivo(int id, SintomasDigestivos sintoma)
        {
            if (id != sintoma.Id)
                return BadRequest("El ID de la URL no coincide con el del cuerpo del objeto.");

            _context.Entry(sintoma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.SintomasDigestivos.AnyAsync(e => e.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/SintomasDigestivos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSintomaDigestivo(int id)
        {
            var sintoma = await _context.SintomasDigestivos.FindAsync(id);
            if (sintoma == null)
                return NotFound();

            _context.SintomasDigestivos.Remove(sintoma);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
