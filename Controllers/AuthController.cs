using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AsistMedAPI.Data;
using AsistMedAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AsistMedContext _context;
        private readonly IConfiguration _config;

        public AuthController(AsistMedContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public class RegistroDto
        {
            public string NombreUsuario { get; set; } = string.Empty;
            public string Contrasena { get; set; } = string.Empty;
            public string Rol { get; set; } = "medico";  // medico|triaje|admin
        }
        public class LoginDto
        {
            public string NombreUsuario { get; set; } = string.Empty;
            public string Contrasena { get; set; } = string.Empty;
        }

        // Déjalo abierto solo para crear cuentas; luego cámbialo a [Authorize(Roles="admin")]
        [Authorize(Roles = "admin")]
[HttpPost("register")]
public async Task<IActionResult> Register([FromBody] RegistroDto dto)
        {
            if (_context.Usuarios.Any(u => u.NombreUsuario == dto.NombreUsuario))
                return BadRequest("Usuario ya existe.");

            var user = new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena),
                Rol = dto.Rol
            };

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return Ok("Registro exitoso.");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == dto.NombreUsuario);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Contrasena, user.Contraseña))
                return Unauthorized("Credenciales inválidas.");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.NombreUsuario),
                new Claim(ClaimTypes.Role, user.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddHours(4), signingCredentials: creds);
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                usuario = new
                {
                    id = user.Id,
                    nombre = user.NombreUsuario,
                    rol = user.Rol
                }
            });


        }
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
            {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var nombre = User.FindFirstValue(ClaimTypes.Name);
                var rol = User.FindFirstValue(ClaimTypes.Role);
                return Ok(new { id, nombre, rol });
            }

    }
}
