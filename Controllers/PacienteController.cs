using Microsoft.AspNetCore.Mvc;
using AsistMedAPI.Models;
using AsistMedAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly AsistMedContext _context;

        public PacienteController(AsistMedContext context)
        {
            _context = context;
        }

        // POST: api/Paciente
        [Authorize(Roles = "medico")]
        [HttpPost]
        public async Task<IActionResult> RegistrarPaciente([FromBody] Paciente paciente)
        {
            try
            {
                // 👇 toma el ID del usuario desde el token
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                paciente.UsuarioId = userId;

                // Verificar si el DNI ya existe
                var existe = await _context.Pacientes.FindAsync(paciente.Dni);
                if (existe != null)
                {
                    return BadRequest("Este paciente ya está registrado.");
                }

                await _context.Pacientes.AddAsync(paciente);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Paciente registrado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // GET: api/Paciente/{dni}
        [HttpGet("{Dni}")]
        public async Task<IActionResult> ObtenerPaciente(string Dni)
        {
            var paciente = await _context.Pacientes.FindAsync(Dni);
            if (paciente == null)
            {
                return NotFound("Paciente no encontrado.");
            }

            return Ok(paciente);
        }

        // PUT: api/Paciente/{Dni}
        [HttpPut("{Dni}")]
        public async Task<IActionResult> ActualizarPaciente(string Dni, [FromBody] Paciente pacienteActualizado)
        {
            if (Dni != pacienteActualizado.Dni)
            {
                return BadRequest("El DNI no coincide.");
            }

            var pacienteExistente = await _context.Pacientes.FindAsync(Dni);
            if (pacienteExistente == null)
            {
                return NotFound("Paciente no encontrado.");
            }

            pacienteExistente.edad = pacienteActualizado.edad;
            pacienteExistente.sexo = pacienteActualizado.sexo;

            pacienteExistente.telefono = pacienteActualizado.telefono;
            pacienteExistente.direccion = pacienteActualizado.direccion;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Paciente actualizado correctamente." });
        }
        


        [Authorize(Roles = "medico")]
        [HttpGet("mis-pacientes")]
        public async Task<IActionResult> MisPacientes()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var pacientes = await _context.Pacientes
                .Where(p => p.UsuarioId == userId)
                .ToListAsync();

            return Ok(pacientes);
        }
    }
}
