using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistMedAPI.Data;
using AsistMedAPI.Models;

namespace AsistMedAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly AsistMedContext _context;

        public PacienteController(AsistMedContext context)
        {
            _context = context;
        }

        // GET: api/Paciente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            return await _context.Pacientes.ToListAsync();
        }

        // GET: api/Paciente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetPaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return NotFound();

            return paciente;
        }

        // POST: api/Paciente
        [HttpPost]
        public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
        {
            // Validación básica de DNI
            if (paciente.DNI.Length != 8)
                return BadRequest("El DNI debe tener exactamente 8 dígitos.");

            paciente.FechaRegistro = DateTime.UtcNow;

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaciente), new { id = paciente.Id }, paciente);
        }

        // PUT: api/Paciente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, Paciente paciente)
        {
            if (id != paciente.Id)
                return BadRequest("El ID de la URL no coincide con el del cuerpo del objeto.");

            _context.Entry(paciente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Pacientes.AnyAsync(p => p.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Paciente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return NotFound();

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
