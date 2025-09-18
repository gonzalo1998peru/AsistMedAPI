using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using AsistMedAPI.Data;
using AsistMedAPI.Models;
using AsistMedAPI.Models.DTO;

namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrediccionIAController : ControllerBase
    {
        private readonly AsistMedContext _context;
        private readonly IConfiguration _config;
        private readonly HttpClient _http;

        public PrediccionIAController(AsistMedContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _http = new HttpClient();
        }

        // ============================================================
        // A) ENDPOINT ORIGINAL: recibe los 42 campos en el body (DTO)
        // ============================================================
        [HttpPost]
        public async Task<IActionResult> PostPrediccionIA([FromBody] PrediccionEntradaDto datos)
        {
            try
            {
                var paciente = await _context.Pacientes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Dni == datos.Dni);

                if (paciente == null)
                    return BadRequest("Paciente no encontrado con ese DNI.");

                var payload = MapToIaPayload(datos, paciente.edad, paciente.sexo);
                SanitizePayload(payload); // 🔒 sin strings vacíos/inesperados

                var iaBaseUrl = _config.GetSection("IAService")["BaseUrl"];
                if (string.IsNullOrWhiteSpace(iaBaseUrl))
                    return StatusCode(500, "URL de la IA no configurada (IAService:BaseUrl).");

                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var resp = await _http.PostAsync($"{iaBaseUrl}/predecir", content);
                if (!resp.IsSuccessStatusCode)
                {
                    var detalle = await resp.Content.ReadAsStringAsync();
                    return StatusCode((int)resp.StatusCode, $"Error al comunicarse con la IA: {detalle}");
                }

                var resultado = await resp.Content.ReadAsStringAsync();
                var ia = JsonSerializer.Deserialize<Dictionary<string, string>>(resultado);
                if (ia == null || !ia.ContainsKey("diagnostico_final"))
                    return StatusCode(500, "La IA no devolvió un resultado válido.");

                var entity = BuildEntityFromDto(datos, paciente, ia);
                _context.PrediccionesIA.Add(entity);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    mensaje = "Datos guardados y predicción obtenida con éxito.",
                    resultado_ia = JsonDocument.Parse(resultado).RootElement
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // ============================================================
        // B) NUEVO: construir DTO de 42 desde la BD y predecir por DNI
        // ============================================================
        [HttpPost("predict/{dni}")]
        public async Task<IActionResult> PredictByDni(string dni)
        {
            try
            {
                var dto = await BuildDtoFromDbAsync(dni);
                if (dto == null)
                    return BadRequest("Faltan registros en alguna sección (triaje, general, GI o nutrición).");

                var paciente = await _context.Pacientes.AsNoTracking()
                                   .FirstAsync(p => p.Dni == dni);

                var payload = MapToIaPayload(dto, paciente.edad, paciente.sexo);
                SanitizePayload(payload); // 🔒 sin strings vacíos/inesperados

                var iaBaseUrl = _config.GetSection("IAService")["BaseUrl"];
                if (string.IsNullOrWhiteSpace(iaBaseUrl))
                    return StatusCode(500, "URL de la IA no configurada (IAService:BaseUrl).");

                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var resp = await _http.PostAsync($"{iaBaseUrl}/predecir", content);
                if (!resp.IsSuccessStatusCode)
                {
                    var detalle = await resp.Content.ReadAsStringAsync();
                    return StatusCode((int)resp.StatusCode, $"Error al comunicarse con la IA: {detalle}");
                }

                var resultado = await resp.Content.ReadAsStringAsync();
                var ia = JsonSerializer.Deserialize<Dictionary<string, string>>(resultado);
                if (ia == null || !ia.ContainsKey("diagnostico_final"))
                    return StatusCode(500, "La IA no devolvió un resultado válido.");

                var entity = BuildEntityFromDto(dto, paciente, ia);
                _context.PrediccionesIA.Add(entity);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    mensaje = "Predicción generada y guardada.",
                    resultado_ia = JsonDocument.Parse(resultado).RootElement
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // ============================================================
        // C) DEBUG: ver el JSON de 42 armado desde la BD (sin llamar IA)
        // ============================================================
        [HttpGet("features/{dni}")]
        public async Task<IActionResult> GetFeaturesByDni(string dni)
        {
            var dto = await BuildDtoFromDbAsync(dni);
            if (dto == null)
                return BadRequest("Faltan registros en alguna sección (triaje, general, GI o nutrición).");

            var paciente = await _context.Pacientes.AsNoTracking()
                               .FirstAsync(p => p.Dni == dni);
            var payload = MapToIaPayload(dto, paciente.edad, paciente.sexo);
            SanitizePayload(payload);
            return Ok(payload);
        }

        // ============================================================
        // Helpers (existentes)
        // ============================================================

        // Normalizadores (evitan strings vacíos que rompen los encoders del modelo)
        private static string Norm(string? s, string def = "No")
        {
            if (string.IsNullOrWhiteSpace(s)) return def;
            var t = s.Trim();
            if (t.Length == 0) return def;
            return char.ToUpper(t[0]) + (t.Length > 1 ? t.Substring(1).ToLower() : "");
        }
        private static string NormSexo(string? s) => string.IsNullOrWhiteSpace(s) ? "F" : s.Trim().ToUpper();

        // Deriva "variable_auxiliar" directamente desde Signos Vitales
        private static string DerivarVariableAuxiliar(PrediccionEntradaDto d)
        {
            return string.IsNullOrWhiteSpace(d.variable_auxiliar) ? "No especificado" : d.variable_auxiliar.Trim();
        }

        // Reemplaza cualquier "" / null del payload por valores seguros
        private static void SanitizePayload(Dictionary<string, object> payload)
        {
            // Campos con default especial (si faltan, usamos "Media")
            var defaultMedia = new HashSet<string> { "frecuencia_ultraprocesados" };

            foreach (var k in payload.Keys.ToList())
            {
                if (payload[k] is string s)
                {
                    if (k == "sexo") { payload[k] = NormSexo(s); continue; }
                    var def = defaultMedia.Contains(k) ? "Media" : "No";
                    payload[k] = Norm(s, def);
                }
            }
        }

        // Une los últimos registros por DNI (si no tienes fecha, ordena por Id)
        private async Task<PrediccionEntradaDto?> BuildDtoFromDbAsync(string dni)
        {
            var p = await _context.Pacientes.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Dni == dni);
            if (p == null) return null;

            var sv = await _context.SignosVitales.AsNoTracking()
                        .Where(x => x.Dni == dni)
                        .OrderByDescending(x => x.Id)
                        .FirstOrDefaultAsync();

            var eg = await _context.EvaluacionGeneralMed.AsNoTracking()
                        .Where(x => x.Dni == dni)
                        .OrderByDescending(x => x.Id)
                        .FirstOrDefaultAsync();

            var gi = await _context.EvaluacionGI.AsNoTracking()
                        .Where(x => x.Dni == dni)
                        .OrderByDescending(x => x.Id)
                        .FirstOrDefaultAsync();

            var nu = await _context.EvaluacionNutricion.AsNoTracking()
                        .Where(x => x.Dni == dni)
                        .OrderByDescending(x => x.Id)
                        .FirstOrDefaultAsync();

            if (sv == null || eg == null || gi == null || nu == null) return null;

            return new PrediccionEntradaDto
            {
                Dni = dni,

                // ---- Signos Vitales
                peso = sv.peso,
                talla = sv.talla,
                presion_sistolica = sv.presion_sistolica,
                presion_diastolica = sv.presion_diastolica,
                frecuencia_cardiaca = sv.frecuencia_cardiaca,
                frecuencia_respiratoria = sv.frecuencia_respiratoria,
                temperatura = sv.temperatura,
                saturacion_oxigeno = sv.saturacion_oxigeno,
                imc = sv.imc,
                glucosa_capilar = sv.glucosa_capilar,

                // 👇 Copiamos el valor real guardado en SignosVitales
                variable_auxiliar = sv.VariableAuxiliar,

                // ---- Evaluación General (normalizada)
                antecedente_uso_ains = Norm(eg.antecedente_uso_ains),
                alergias_conocidas = Norm(eg.alergias_conocidas),
                fatiga = Norm(eg.fatiga),
                antecedente_eda = Norm(eg.antecedente_eda),
                antecedentes_diabetes_familiar = Norm(eg.antecedentes_diabetes_familiar),
                perdida_peso_no_intencional = Norm(eg.perdida_peso_no_intencional),
                fatiga_2 = Norm(eg.fatiga_2),
                nauseas = Norm(eg.nauseas),
                antecedente_tabaquismo = Norm(eg.antecedente_tabaquismo),

                // ---- Gastrointestinal (normalizada)
                duracion_sintomas_dias = gi.duracion_sintomas_dias,
                dolor_abdominal = Norm(gi.dolor_abdominal),
                zona_dolor_abdominal = Norm(gi.zona_dolor_abdominal, "No"),
                cambios_deposiciones = Norm(gi.cambios_deposiciones),
                sangrado_digestivo = Norm(gi.sangrado_digestivo),
                infecciones_recientes = Norm(gi.infecciones_recientes),
                perdida_apetito = Norm(gi.perdida_apetito),
                vomitos = Norm(gi.vomitos),
                distension_abdominal = Norm(gi.distension_abdominal),
                diarrea = Norm(gi.diarrea),
                reflujo_gastroesofagico = Norm(gi.reflujo_gastroesofagico),
                antecedente_gastritis = Norm(gi.antecedente_gastritis),
                antecedente_ulcera = Norm(gi.antecedente_ulcera),
                antecedente_colitis = Norm(gi.antecedente_colitis),

                // ---- Nutricional (normalizada)
                frecuencia_ultraprocesados = Norm(nu.frecuencia_ultraprocesados, "Media"),
                cantidad_comidas_dia = nu.cantidad_comidas_dia,
                perdida_peso_nutricion = Norm(nu.perdida_peso_nutricion),
                sintoma_deficiencia_nutricional = Norm(nu.sintoma_deficiencia_nutricional),
                frutas_verduras = Norm(nu.frutas_verduras),
                cantidadAguaDia = nu.cantidadAguaDia
            };
        }

        // Renombra al formato EXACTO que espera Flask y fuerza edad/sexo de BD (+ normaliza y deriva aux)
        private Dictionary<string, object> MapToIaPayload(PrediccionEntradaDto d, int edadDb, string sexoDb)
        {
            var imc = d.imc > 0 ? d.imc : (d.talla > 0 ? d.peso / (d.talla * d.talla) : 0);

            // Si viene vacío o "No", derivamos etiqueta válida desde signos vitales
            var aux = string.IsNullOrWhiteSpace(d.variable_auxiliar) || d.variable_auxiliar.Trim().ToLower() == "no"
                ? DerivarVariableAuxiliar(d)
                : d.variable_auxiliar.Trim();

            return new Dictionary<string, object>
            {
                { "edad",       edadDb },
                { "sexo",       NormSexo(sexoDb) },

                { "peso (kg)",  d.peso },
                { "talla (mt)", d.talla },
                { "presion_sistolica", d.presion_sistolica },
                { "presion_diastolica", d.presion_diastolica },
                { "frecuencia_cardiaca", d.frecuencia_cardiaca },
                { "frecuencia_respiratoria", d.frecuencia_respiratoria },
                { "temperatura", d.temperatura },
                { "saturacion_oxigeno", d.saturacion_oxigeno },
                { "imc= peso/(tallaxtalla)", imc },
                { "glucosa_capilar", d.glucosa_capilar },

                // ⚠️ Enviamos SIEMPRE una etiqueta válida para el modelo
                { "variable_auxiliar", aux },

                { "antecedente_uso_ains", Norm(d.antecedente_uso_ains) },
                { "¿Tiene alergias conocidas?", Norm(d.alergias_conocidas) },
                { "fatiga o debilidad", Norm(d.fatiga) },
                { "antecedente_eda", Norm(d.antecedente_eda) },
                { "antecedentes_diabetes_familiar", Norm(d.antecedentes_diabetes_familiar) },
                { "perdida_peso_no_intencional", Norm(d.perdida_peso_no_intencional) },
                { "¿Ha experimentado fatiga o debilidad?", Norm(d.fatiga_2) },
                { "nauseas", Norm(d.nauseas) },
                { "antecedente_tabaquismo", Norm(d.antecedente_tabaquismo) },

                { "duracion_sintomas_dias", d.duracion_sintomas_dias },
                { "dolor_abdominal", Norm(d.dolor_abdominal) },
                { "zona_dolor_abdominal", Norm(d.zona_dolor_abdominal, "No") },
                { "cambios_deposiciones", Norm(d.cambios_deposiciones) },
                { "sangrado_digestivo", Norm(d.sangrado_digestivo) },
                { "infecciones_recientes", Norm(d.infecciones_recientes) },
                { "perdida_apetito", Norm(d.perdida_apetito) },
                { "vomitos", Norm(d.vomitos) },
                { "distension_abdominal", Norm(d.distension_abdominal) },
                { "diarrea", Norm(d.diarrea) },
                { "reflujo_gastroesofagico", Norm(d.reflujo_gastroesofagico) },
                { "antecedente_gastritis(checkbox)", Norm(d.antecedente_gastritis) },
                { "antecedente_ulcera", Norm(d.antecedente_ulcera) },
                { "antecedente_colitis", Norm(d.antecedente_colitis) },

                { "frecuencia_ultraprocesados", Norm(d.frecuencia_ultraprocesados, "Media") },
                { "cantidad_comidas_dia", d.cantidad_comidas_dia },
                { "perdida_peso_nutricion", Norm(d.perdida_peso_nutricion) },
                { "sintoma_deficiencia_nutricional", Norm(d.sintoma_deficiencia_nutricional) },
                { "frutas_verduras", Norm(d.frutas_verduras) },
                { "agua_diaria", d.cantidadAguaDia }
            };
        }

        // Construye la entidad para guardar en PrediccionesIA
        private PrediccionIA BuildEntityFromDto(PrediccionEntradaDto d, dynamic paciente,
                                               Dictionary<string, string> ia)
        {
            return new PrediccionIA
            {
                Dni = paciente.Dni,
                edad = paciente.edad,
                sexo = paciente.sexo,

                peso = d.peso,
                talla = d.talla,
                presion_sistolica = d.presion_sistolica,
                presion_diastolica = d.presion_diastolica,
                frecuencia_cardiaca = d.frecuencia_cardiaca,
                frecuencia_respiratoria = d.frecuencia_respiratoria,
                temperatura = d.temperatura,
                saturacion_oxigeno = d.saturacion_oxigeno,
                imc = d.imc,
                glucosa_capilar = d.glucosa_capilar,
                variable_auxiliar = d.variable_auxiliar,

                antecedente_uso_ains = d.antecedente_uso_ains,
                alergias_conocidas = d.alergias_conocidas,
                fatiga = d.fatiga,
                antecedente_eda = d.antecedente_eda,
                antecedentes_diabetes_familiar = d.antecedentes_diabetes_familiar,
                perdida_peso_no_intencional = d.perdida_peso_no_intencional,
                fatiga_2 = d.fatiga_2,
                nauseas = d.nauseas,
                antecedente_tabaquismo = d.antecedente_tabaquismo,

                duracion_sintomas_dias = d.duracion_sintomas_dias,
                dolor_abdominal = d.dolor_abdominal,
                zona_dolor_abdominal = d.zona_dolor_abdominal,
                cambios_deposiciones = d.cambios_deposiciones,
                sangrado_digestivo = d.sangrado_digestivo,
                infecciones_recientes = d.infecciones_recientes,
                perdida_apetito = d.perdida_apetito,
                vomitos = d.vomitos,
                distension_abdominal = d.distension_abdominal,
                diarrea = d.diarrea,
                reflujo_gastroesofagico = d.reflujo_gastroesofagico,
                antecedente_gastritis = d.antecedente_gastritis,
                antecedente_ulcera = d.antecedente_ulcera,
                antecedente_colitis = d.antecedente_colitis,

                frecuencia_ultraprocesados = d.frecuencia_ultraprocesados,
                cantidad_comidas_dia = d.cantidad_comidas_dia,
                perdida_peso_nutricion = d.perdida_peso_nutricion,
                sintoma_deficiencia_nutricional = d.sintoma_deficiencia_nutricional,
                frutas_verduras = d.frutas_verduras,
                cantidadAguaDia = d.cantidadAguaDia,

                DiagnosticoFinal = ia.GetValueOrDefault("diagnostico_final"),
                DiagnosticoMedico = ia.GetValueOrDefault("diagnostico_medico"),
                DiagnosticoGastro = ia.GetValueOrDefault("diagnostico_gastro"),
                DiagnosticoNutri = ia.GetValueOrDefault("diagnostico_nutri"),
                Porcentaje = ia.GetValueOrDefault("porcentaje")
            };
        }

        // ============================================================
        // D) CONSULTAS PARA VISTA PREDICCIONES (SOLO LECTURA)  👇 NUEVO
        //    Coinciden EXACTO con lo que consume tu front:
        //    /api/predicciones/resumen
        //    /api/predicciones/detalle-enfermedad
        //    /api/predicciones/detalle-riesgo
        // ============================================================

        // DTOs ligeros (locales al controlador para no tocar tus Models)
        public class KpisPrediccionDto
        {
            public int TotalPredicciones { get; set; }
            public double PorcentajeRiesgoAlto { get; set; } // 0..100
            public string DiagnosticoMasComun { get; set; } = "-";
        }

        public class PrediccionPorEnfermedadDto
        {
            public string Enfermedad { get; set; } = "";
            public int Cantidad { get; set; }
        }

        public class DistribucionRiesgoItemDto
        {
            public string Nivel { get; set; } = "";
            public double Porcentaje { get; set; } // 0..100
        }

        public class PrediccionesResumenDto
        {
            public KpisPrediccionDto Kpis { get; set; } = new();
            public List<PrediccionPorEnfermedadDto> PorEnfermedad { get; set; } = new();
            public List<DistribucionRiesgoItemDto> DistribucionRiesgo { get; set; } = new();
        }

        public class DetalleEnfermedadDto
        {
            public string Enfermedad { get; set; } = "";
            public int CantidadCasos { get; set; }
            public Dictionary<string, double> PorcentajeRiesgo { get; set; } = new()
            {
                ["Bajo"] = 0, ["Medio"] = 0, ["Alto"] = 0
            };
        }

        public class EnfermedadFrecuenteItemDto
        {
            public string Enfermedad { get; set; } = "";
            public int Casos { get; set; }
        }

        public class DetalleRiesgoDto
        {
            public string Nivel { get; set; } = "";
            public int CantidadPacientes { get; set; }
            public List<EnfermedadFrecuenteItemDto> EnfermedadesFrecuentes { get; set; } = new();
        }

        // ────────────────────────────────────────────────────────────
        // GET /api/predicciones/resumen?periodo=semana|mes|anio
        // ────────────────────────────────────────────────────────────
        [HttpGet("/api/predicciones/resumen")]
        public async Task<ActionResult<PrediccionesResumenDto>> GetResumen([FromQuery] string periodo = "semana")
        {
            var (inicio, fin) = CalcularRango(periodo);

            // Intenta usar CreatedAt o FechaPrediccion si existen; si no, no filtra por fecha.
            var q = _context.PrediccionesIA.AsNoTracking(); // sin filtro de fecha


            // Si tu entidad no tiene ninguna fecha, descomenta esta línea:
            // q = qBase;

            var total = await q.CountAsync();

            // Diagnóstico más común (usa tu campo ya guardado)
            var masComun = total == 0
                ? "-"
                : await q.GroupBy(p => p.DiagnosticoFinal)
                         .Select(g => new { Enfermedad = g.Key!, Cnt = g.Count() })
                         .OrderByDescending(x => x.Cnt)
                         .Select(x => x.Enfermedad)
                         .FirstAsync();

            // Distribución de riesgo (derivada desde Porcentaje string "82" / "0.82" / "82%")
            var riesgos = await q.Select(p => NivelDesdePorcentaje(p.Porcentaje)).ToListAsync();
            var bajo = riesgos.Count(n => n == "Bajo");
            var medio = riesgos.Count(n => n == "Medio");
            var alto = riesgos.Count(n => n == "Alto");

            var dist = new List<DistribucionRiesgoItemDto>();
            if (total > 0)
            {
                dist.Add(new() { Nivel = "Bajo", Porcentaje = Math.Round(bajo * 100.0 / total, 2) });
                dist.Add(new() { Nivel = "Medio", Porcentaje = Math.Round(medio * 100.0 / total, 2) });
                dist.Add(new() { Nivel = "Alto", Porcentaje = Math.Round(alto * 100.0 / total, 2) });
            }
            else
            {
                dist.AddRange(new[]
                {
                    new DistribucionRiesgoItemDto{ Nivel="Bajo",  Porcentaje=0 },
                    new DistribucionRiesgoItemDto{ Nivel="Medio", Porcentaje=0 },
                    new DistribucionRiesgoItemDto{ Nivel="Alto",  Porcentaje=0 },
                });
            }

            // Barras por enfermedad (top 10)
            var porEnfermedad = await q.GroupBy(p => p.DiagnosticoFinal)
                .Select(g => new PrediccionPorEnfermedadDto
                {
                    Enfermedad = g.Key!,
                    Cantidad = g.Count()
                })
                .OrderByDescending(x => x.Cantidad)
                .Take(10)
                .ToListAsync();

            var dto = new PrediccionesResumenDto
            {
                Kpis = new KpisPrediccionDto
                {
                    TotalPredicciones = total,
                    PorcentajeRiesgoAlto = total == 0 ? 0 : Math.Round(alto * 100.0 / total, 2),
                    DiagnosticoMasComun = masComun ?? "-"
                },
                PorEnfermedad = porEnfermedad,
                DistribucionRiesgo = dist
            };

            return Ok(dto);
        }

        // ────────────────────────────────────────────────────────────
        // GET /api/predicciones/detalle-enfermedad?periodo=...&enfermedad=...
        // ────────────────────────────────────────────────────────────
        [HttpGet("/api/predicciones/detalle-enfermedad")]
        public async Task<ActionResult<DetalleEnfermedadDto>> GetDetalleEnfermedad(
            [FromQuery] string periodo, [FromQuery] string enfermedad)
        {
            if (string.IsNullOrWhiteSpace(enfermedad))
                return BadRequest("enfermedad es requerida");

            var (inicio, fin) = CalcularRango(periodo);
            var qBase = _context.PrediccionesIA.AsNoTracking();
            var q = qBase.Where(p =>
                    ((EF.Property<DateTime?>(p, "CreatedAt") ?? EF.Property<DateTime?>(p, "FechaPrediccion")) ?? DateTime.MinValue) >= inicio &&
                    ((EF.Property<DateTime?>(p, "CreatedAt") ?? EF.Property<DateTime?>(p, "FechaPrediccion")) ?? DateTime.MaxValue) < fin
                )
                .Where(p => p.DiagnosticoFinal != null && p.DiagnosticoFinal.ToLower() == enfermedad.ToLower());

            var total = await q.CountAsync();
            var dto = new DetalleEnfermedadDto
            {
                Enfermedad = enfermedad,
                CantidadCasos = total
            };

            if (total > 0)
            {
                var riesgos = await q.Select(p => NivelDesdePorcentaje(p.Porcentaje)).ToListAsync();
                var bajo = riesgos.Count(n => n == "Bajo");
                var medio = riesgos.Count(n => n == "Medio");
                var alto = riesgos.Count(n => n == "Alto");

                dto.PorcentajeRiesgo["Bajo"] = Math.Round(bajo * 100.0 / total, 2);
                dto.PorcentajeRiesgo["Medio"] = Math.Round(medio * 100.0 / total, 2);
                dto.PorcentajeRiesgo["Alto"] = Math.Round(alto * 100.0 / total, 2);
            }

            return Ok(dto);
        }

        // ────────────────────────────────────────────────────────────
        // GET /api/predicciones/detalle-riesgo?periodo=...&nivel=Bajo|Medio|Alto
        // ────────────────────────────────────────────────────────────
        [HttpGet("/api/predicciones/detalle-riesgo")]
        public async Task<ActionResult<DetalleRiesgoDto>> GetDetalleRiesgo(
            [FromQuery] string periodo, [FromQuery] string nivel)
        {
            if (string.IsNullOrWhiteSpace(nivel))
                return BadRequest("nivel es requerido");

            var (inicio, fin) = CalcularRango(periodo);
            var nivelNorm = Capitalizar(nivel);

            var qBase = _context.PrediccionesIA.AsNoTracking();
            var q = qBase
                .Where(p =>
                    ((EF.Property<DateTime?>(p, "CreatedAt") ?? EF.Property<DateTime?>(p, "FechaPrediccion")) ?? DateTime.MinValue) >= inicio &&
                    ((EF.Property<DateTime?>(p, "CreatedAt") ?? EF.Property<DateTime?>(p, "FechaPrediccion")) ?? DateTime.MaxValue) < fin
                )
                .Select(p => new { p.DiagnosticoFinal, Nivel = NivelDesdePorcentaje(p.Porcentaje) })
                .Where(x => x.Nivel == nivelNorm);

            var total = await q.CountAsync();

            var topEnfermedades = await q.GroupBy(x => x.DiagnosticoFinal)
                .Select(g => new EnfermedadFrecuenteItemDto
                {
                    Enfermedad = g.Key!,
                    Casos = g.Count()
                })
                .OrderByDescending(x => x.Casos)
                .Take(10)
                .ToListAsync();

            var dto = new DetalleRiesgoDto
            {
                Nivel = nivelNorm,
                CantidadPacientes = total,
                EnfermedadesFrecuentes = topEnfermedades
            };

            return Ok(dto);
        }

        // ────────────────────────────────────────────────────────────
        // Helpers NUEVOS para rango y nivel de riesgo
        // ────────────────────────────────────────────────────────────
        private static (DateTime inicio, DateTime fin) CalcularRango(string periodo)
        {
            var hoy = DateTime.UtcNow;
            DateTime inicio;

            if (string.Equals(periodo, "mes", StringComparison.OrdinalIgnoreCase))
                inicio = new DateTime(hoy.Year, hoy.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            else if (string.Equals(periodo, "anio", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(periodo, "año", StringComparison.OrdinalIgnoreCase))
                inicio = new DateTime(hoy.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            else
                inicio = hoy.AddDays(-7); // “semana” por defecto

            var fin = hoy.AddSeconds(1);
            return (inicio, fin);
        }

        private static string NivelDesdePorcentaje(string? porcentaje)
        {
            if (string.IsNullOrWhiteSpace(porcentaje)) return "Medio";
            var s = porcentaje.Trim().Replace("%", "");
            if (!double.TryParse(s, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out var v))
            {
                return "Medio";
            }
            // 0..1 -> 0..100
            if (v <= 1.0) v *= 100.0;

            if (v >= 70) return "Alto";
            if (v >= 40) return "Medio";
            return "Bajo";
        }

        private static string Capitalizar(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }
    }
}
