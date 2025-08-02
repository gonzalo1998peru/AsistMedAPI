using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistMedAPI.Models.DTO;
using AsistMedAPI.Services;
using AsistMedAPI.Models;
using AsistMedAPI.Data;

namespace AsistMedAPI.Controllers
{
    [ApiController]

    [Route("api/[controller]")]

    public class PrediccionIAController : ControllerBase
    {
        private readonly EvaluadorIA _evaluadorIA;
        private readonly AsistMedContext _context;

        public PrediccionIAController(EvaluadorIA evaluadorIA, AsistMedContext context)
        {
            _evaluadorIA = evaluadorIA;
            _context = context;
        }

        [HttpPost("evaluar")]
public async Task<ActionResult<InformeClinicoResultadoDto>> EvaluarYDevolverInforme([FromBody] int pacienteId)
{
    // 1. Obtener datos clínicos
    var signos = await _context.SignosVitales
        .Where(s => s.PacienteId == pacienteId)
        .OrderByDescending(s => s.FechaRegistro)
        .FirstOrDefaultAsync();

    var clinica = await _context.EvaluacionesClinicas
        .Where(c => c.PacienteId == pacienteId)
        .OrderByDescending(c => c.FechaEvaluacion)
        .FirstOrDefaultAsync();

    var sintomas = await _context.SintomasDigestivos
        .Where(s => s.PacienteId == pacienteId)
        .OrderByDescending(s => s.FechaEvaluacion) // <- asegúrate de que exista este campo en el modelo
        .FirstOrDefaultAsync();

    var nutricion = await _context.EvaluacionesNutricionales
        .Where(n => n.PacienteId == pacienteId)
        .OrderByDescending(n => n.FechaEvaluacion)
        .FirstOrDefaultAsync();

    if (signos == null || clinica == null || sintomas == null || nutricion == null)
    {
        return BadRequest("Faltan datos clínicos del paciente.");
    }

    // 2. Armar DTO completo para IA
    var dto = new EvaluacionCompletaDto
    {
        PacienteId = pacienteId,
        Fatiga = clinica.Fatiga ?? false,
        MedicamentosActuales = clinica.MedicamentosActuales ?? false,
        Alergias = clinica.Alergias ?? false,
        AntecedentesPatologicos = clinica.AntecedenteEDA ?? false,
        GlucosaMgDl = signos.Glucosa ?? 0,
        Imc = signos.IMC ?? 0,
        DolorEstomacal = sintomas.DolorAbdominal ?? false, // corrige según tu modelo
        Nausea = sintomas.Nausea ?? false,
        Vomito = sintomas.Vomito ?? false,
        SangradoDigestivo = sintomas.SangradoDigestivo ?? false,
        ArdorEstomacal = sintomas.ArdorEstomacal ?? false,
        ConsumoUltraprocesados = nutricion.ConsumoUltraprocesados ?? false,
        CantidadAguaDia = nutricion.CantidadAguaDia ?? 0
    };

    // 3. Ejecutar IA
    var resultado = _evaluadorIA.Evaluar(dto);
    var riesgoDigestivo = _evaluadorIA.CalcularRiesgoDigestivo(dto);
    var riesgoClinico = _evaluadorIA.CalcularRiesgoClinico(dto);
    var riesgoNutricional = _evaluadorIA.CalcularRiesgoNutricional(dto);

    // 4. Guardar predicción
    var prediccion = new PrediccionIA
    {
        PacienteId = pacienteId,
        EnfermedadPredicha = resultado.Enfermedad,
        PorcentajeConfianza = resultado.Porcentaje,
        HistorialDescriptivo = resultado.Factores,
        FechaPrediccion = DateTime.UtcNow,
        RiesgoDigestivo = riesgoDigestivo,
        RiesgoClinico = riesgoClinico,
        RiesgoNutricional = riesgoNutricional
    };

    _context.PrediccionesIA.Add(prediccion);
    await _context.SaveChangesAsync();

    // 5. Devolver informe clínico completo
    var informe = new InformeClinicoResultadoDto
    {
        Fecha = prediccion.FechaPrediccion,
        DNI = (await _context.Pacientes.FindAsync(pacienteId))?.DNI ?? "Sin DNI",
        HistorialGeneral = clinica.ObservacionesGenerales ?? "Sin datos",
        HistorialGastroenterologico = sintomas.NotasEspecialista ?? "Sin datos",
        HistorialNutricional = nutricion.Observaciones ?? "Sin datos",
        DiagnosticoMedico = resultado.Enfermedad,
        DiagnosticoGastro = resultado.Enfermedad,
        DiagnosticoNutricional = "Requiere evaluación nutricional", // Puedes mejorar esto
        FactoresDeRiesgo = resultado.Factores,
        ResultadoTexto = $"Se predice con {resultado.Porcentaje}% la enfermedad: {resultado.Enfermedad}"
    };

    return Ok(informe);
}

    }
}
