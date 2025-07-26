using AsistMedAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistMedAPI.Data;
using AsistMedAPI.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrediccionDetallePdfController : ControllerBase
    {
        private readonly AsistMedContext _context;

        public PrediccionDetallePdfController(AsistMedContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrediccionDetallePdf>>> GetPrediccionesPdf()
        {
            return await _context.PrediccionesPdf
                .Include(p => p.Paciente)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrediccionDetallePdf>> GetPrediccionDetallePdf(int id)
        {
            var prediccion = await _context.PrediccionesPdf
                .Include(p => p.Paciente)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prediccion == null)
                return NotFound();

            return prediccion;
        }

        [HttpPost]
        public async Task<ActionResult<PrediccionDetallePdf>> PostPrediccionDetallePdf(PrediccionDetallePdf prediccion)
        {
            prediccion.FechaGeneracion = DateTime.SpecifyKind(prediccion.FechaGeneracion, DateTimeKind.Utc);
            _context.PrediccionesPdf.Add(prediccion);

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

            return CreatedAtAction(nameof(GetPrediccionDetallePdf), new { id = prediccion.Id }, prediccion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrediccionDetallePdf(int id, PrediccionDetallePdf prediccion)
        {
            if (id != prediccion.Id)
                return BadRequest("El ID de la URL no coincide con el objeto.");

            prediccion.FechaGeneracion = DateTime.SpecifyKind(prediccion.FechaGeneracion, DateTimeKind.Utc);
            _context.Entry(prediccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.PrediccionesPdf.AnyAsync(e => e.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrediccionDetallePdf(int id)
        {
            var prediccion = await _context.PrediccionesPdf.FindAsync(id);
            if (prediccion == null)
                return NotFound();

            _context.PrediccionesPdf.Remove(prediccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Endpoint para generar el PDF
        [HttpGet("descargar/{pacienteId}")]
public async Task<IActionResult> DescargarInformePdf(int pacienteId)
{
    var paciente = await ObtenerPaciente(pacienteId);
    var signos = await ObtenerSignosVitales(pacienteId);
    var clinica = await ObtenerEvaluacionClinica(pacienteId);
    var sintomas = await ObtenerSintomasDigestivos(pacienteId);
    var nutricion = await ObtenerEvaluacionNutricional(pacienteId);
    var prediccion = await ObtenerPrediccionIA(pacienteId);

    if (paciente == null || prediccion == null)
        return NotFound("Paciente o predicción no encontrada.");

    if (signos == null || clinica == null || sintomas == null || nutricion == null)
        return BadRequest("Faltan datos clínicos necesarios para generar el PDF.");

    var pdfBytes = PdfGenerator.GenerarInformePdf(paciente, signos, clinica, sintomas, nutricion, prediccion);

    return File(pdfBytes, "application/pdf", $"Informe_Paciente_{paciente.DNI}.pdf");
}


        // Métodos privados de obtención de datos
        private async Task<Paciente?> ObtenerPaciente(int pacienteId) =>
            await _context.Pacientes.FindAsync(pacienteId);

        private async Task<SignosVitales?> ObtenerSignosVitales(int pacienteId) =>
            await _context.SignosVitales
                .Where(s => s.PacienteId == pacienteId)
                .OrderByDescending(s => s.FechaRegistro)
                .FirstOrDefaultAsync();

        private async Task<EvaluacionClinica?> ObtenerEvaluacionClinica(int pacienteId) =>
            await _context.EvaluacionesClinicas
                .Where(e => e.PacienteId == pacienteId)
                .OrderByDescending(e => e.FechaEvaluacion)
                .FirstOrDefaultAsync();

        private async Task<SintomasDigestivos?> ObtenerSintomasDigestivos(int pacienteId) =>
            await _context.SintomasDigestivos
                .Where(s => s.PacienteId == pacienteId)
                .OrderByDescending(s => s.FechaEvaluacion)
                .FirstOrDefaultAsync();

        private async Task<EvaluacionNutricional?> ObtenerEvaluacionNutricional(int pacienteId) =>
            await _context.EvaluacionesNutricionales
                .Where(e => e.PacienteId == pacienteId)
                .OrderByDescending(e => e.FechaEvaluacion)
                .FirstOrDefaultAsync();

        private async Task<PrediccionIA?> ObtenerPrediccionIA(int pacienteId) =>
            await _context.PrediccionesIA
                .Where(p => p.PacienteId == pacienteId)
                .OrderByDescending(p => p.FechaPrediccion)
                .FirstOrDefaultAsync();

        // Métodos estáticos de formateo para el PDF
    
    }
}
