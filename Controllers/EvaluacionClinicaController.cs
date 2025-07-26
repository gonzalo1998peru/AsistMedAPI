using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistMedAPI.Data;
using AsistMedAPI.Models;

namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionClinicaController : ControllerBase
    {
        private readonly AsistMedContext _context;

        public EvaluacionClinicaController(AsistMedContext context)
        {
            _context = context;
        }

        // GET: api/EvaluacionClinica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluacionClinica>>> GetEvaluacionesClinicas()
        {
            return await _context.EvaluacionesClinicas.ToListAsync();
        }

        // GET: api/EvaluacionClinica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluacionClinica>> GetEvaluacionClinica(int id)
        {
            var evaluacion = await _context.EvaluacionesClinicas.FindAsync(id);

            if (evaluacion == null)
            {
                return NotFound();
            }

            return evaluacion;
        }

        // POST: api/EvaluacionClinica
        [HttpPost]
        public async Task<ActionResult<EvaluacionClinica>> PostEvaluacionClinica(EvaluacionClinica evaluacion)
        {
            _context.EvaluacionesClinicas.Add(evaluacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvaluacionClinica), new { id = evaluacion.Id }, evaluacion);
        }

        // PUT: api/EvaluacionClinica/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluacionClinica(int id, EvaluacionClinica evaluacion)
        {
            if (id != evaluacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(evaluacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluacionClinicaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/EvaluacionClinica/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluacionClinica(int id)
        {
            var evaluacion = await _context.EvaluacionesClinicas.FindAsync(id);
            if (evaluacion == null)
            {
                return NotFound();
            }

            _context.EvaluacionesClinicas.Remove(evaluacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvaluacionClinicaExists(int id)
        {
            return _context.EvaluacionesClinicas.Any(e => e.Id == id);
        }
    }
}
