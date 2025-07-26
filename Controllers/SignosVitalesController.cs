using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistMedAPI.Models;
using AsistMedAPI.Data;

namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignosVitalesController : ControllerBase
    {
        private readonly AsistMedContext _context;

        public SignosVitalesController(AsistMedContext context)
        {
            _context = context;
        }

        // GET: api/SignosVitales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SignosVitales>>> GetSignosVitales()
        {
            return await _context.SignosVitales.ToListAsync();
        }

        // GET: api/SignosVitales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SignosVitales>> GetSignosVitales(int id)
        {
            var signos = await _context.SignosVitales.FindAsync(id);

            if (signos == null)
                return NotFound();

            return signos;
        }

        // POST: api/SignosVitales
        [HttpPost]
        public async Task<ActionResult<SignosVitales>> PostSignosVitales(SignosVitales signos)
        {
            _context.SignosVitales.Add(signos);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSignosVitales), new { id = signos.Id }, signos);
        }

        // PUT: api/SignosVitales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSignosVitales(int id, SignosVitales signos)
        {
            if (id != signos.Id)
                return BadRequest();

            _context.Entry(signos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SignosVitalesExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/SignosVitales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSignosVitales(int id)
        {
            var signos = await _context.SignosVitales.FindAsync(id);
            if (signos == null)
                return NotFound();

            _context.SignosVitales.Remove(signos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SignosVitalesExists(int id)
        {
            return _context.SignosVitales.Any(e => e.Id == id);
        }
    }
}
