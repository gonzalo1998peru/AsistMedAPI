using Microsoft.AspNetCore.Mvc;
using AsistMedAPI.Models;
using AsistMedAPI.Data;
using Microsoft.EntityFrameworkCore;

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

        // POST: api/SignosVitales
        [HttpPost]
        public async Task<IActionResult> PostSignosVitales([FromBody] SignosVitales signos)
        {
            try
            {
                await _context.SignosVitales.AddAsync(signos);
                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Signos vitales registrados correctamente.", datos = signos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar signos vitales: {ex.Message}");
            }
        }

        // GET: api/SignosVitales/Dni/12345678
        [HttpGet("Dni/{Dni}")]
        public async Task<IActionResult> GetSignosPorDni(string Dni)
        {
            var registros = await _context.SignosVitales
                                        .Where(s => s.Dni == Dni)
                                        .ToListAsync();

            if (registros == null || !registros.Any())
                return NotFound($"No se encontraron signos vitales para el DNI {Dni}");

            return Ok(registros);
        }
    }
}
