using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistMedAPI.Data;
using AsistMedAPI.Services;
using System.Threading.Tasks;

namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformeClinicoController : ControllerBase
    {
        private readonly AsistMedContext _context;

        public InformeClinicoController(AsistMedContext context)
        {
            _context = context;
        }

        [HttpGet("GenerarPdf/{dni}")]
        public async Task<IActionResult> GenerarPdf(string dni)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Dni == dni);
            if (paciente == null)
                return NotFound("Paciente no encontrado.");

            var signos = await _context.SignosVitales
                .Where(s => s.Dni == dni)
                .OrderByDescending(s => s.Id)
                .FirstOrDefaultAsync();

            var evaluacionGeneral = await _context.EvaluacionGeneralMed
                .Where(e => e.Dni == dni)
                .OrderByDescending(e => e.Id)
                .FirstOrDefaultAsync();

            var evaluacionGI = await _context.EvaluacionGI
                .Where(e => e.Dni == dni)
                .OrderByDescending(e => e.Id)
                .FirstOrDefaultAsync();

            var evaluacionNutricion = await _context.EvaluacionNutricion
                .Where(e => e.Dni == dni)
                .OrderByDescending(e => e.Id)
                .FirstOrDefaultAsync();

            var prediccion = await _context.PrediccionesIA
                .Where(p => p.Dni == dni)
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync();

            if (signos == null || evaluacionGeneral == null || evaluacionGI == null || evaluacionNutricion == null || prediccion == null)
                return BadRequest("Faltan datos clínicos para generar el informe.");

            var pdfBytes = PdfGenerator.GenerarInforme(paciente, signos, evaluacionGeneral, evaluacionGI, evaluacionNutricion, prediccion);

            return File(pdfBytes, "application/pdf", $"Informe_{dni}.pdf");
        }
    }
}
