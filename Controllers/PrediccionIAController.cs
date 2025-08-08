using Microsoft.AspNetCore.Mvc;
using AsistMedAPI.Models;
using AsistMedAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using AsistMedAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrediccionIAController : ControllerBase
    {
        private readonly AsistMedContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public PrediccionIAController(AsistMedContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _httpClient = new HttpClient();
        }

        [HttpPost]
        public async Task<IActionResult> PostPrediccionIA([FromBody] PrediccionEntradaDto datos)
        {
            try
            {

                // 👉 Buscar paciente por DNI en la tabla Paciente
                var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Dni == datos.Dni);
                if (paciente == null)
                    return BadRequest("Paciente no encontrado con ese DNI.");

                // 👉 Tomar edad y sexo desde la base de datos
                var edad = paciente.edad;
                var sexo = paciente.sexo;

                // 👉 Serializar datos a JSON
                // 🔁 Renombrar los campos a los que espera la IA
                var datosTransformados = new Dictionary<string, object>
                {
                    { "edad", datos.edad },
                    { "sexo", datos.sexo },
                    { "peso (kg)", datos.peso },
                    { "talla (mt)", datos.talla },
                    { "presion_sistolica", datos.presion_sistolica },
                    { "presion_diastolica", datos.presion_diastolica },
                    { "frecuencia_cardiaca", datos.frecuencia_cardiaca },
                    { "frecuencia_respiratoria", datos.frecuencia_respiratoria },
                    { "temperatura", datos.temperatura },
                    { "saturacion_oxigeno", datos.saturacion_oxigeno },
                    { "imc= peso/(tallaxtalla)", datos.imc },
                    { "glucosa_capilar", datos.glucosa_capilar },
                    { "variable_auxiliar", datos.variable_auxiliar },
                    { "antecedente_uso_ains", datos.antecedente_uso_ains },
                    { "¿Tiene alergias conocidas?", datos.alergias_conocidas },
                    { "fatiga o debilidad", datos.fatiga },
                    { "antecedente_eda", datos.antecedente_eda },
                    { "antecedentes_diabetes_familiar", datos.antecedentes_diabetes_familiar },
                    { "perdida_peso_no_intencional", datos.perdida_peso_no_intencional },
                    { "¿Ha experimentado fatiga o debilidad?", datos.fatiga_2 },
                    { "nauseas", datos.nauseas },
                    { "antecedente_tabaquismo", datos.antecedente_tabaquismo },
                    { "duracion_sintomas_dias", datos.duracion_sintomas_dias },
                    { "dolor_abdominal", datos.dolor_abdominal },
                    { "zona_dolor_abdominal", datos.zona_dolor_abdominal },
                    { "cambios_deposiciones", datos.cambios_deposiciones },
                    { "sangrado_digestivo", datos.sangrado_digestivo },
                    { "infecciones_recientes", datos.infecciones_recientes },
                    { "perdida_apetito", datos.perdida_apetito },
                    { "vomitos", datos.vomitos },
                    { "distension_abdominal", datos.distension_abdominal },
                    { "diarrea", datos.diarrea },
                    { "reflujo_gastroesofagico", datos.reflujo_gastroesofagico },
                    { "antecedente_gastritis(checkbox)", datos.antecedente_gastritis },
                    { "antecedente_ulcera", datos.antecedente_ulcera },
                    { "antecedente_colitis", datos.antecedente_colitis },
                    { "frecuencia_ultraprocesados", datos.frecuencia_ultraprocesados },
                    { "cantidad_comidas_dia", datos.cantidad_comidas_dia },
                    { "perdida_peso_nutricion", datos.perdida_peso_nutricion },
                    { "sintoma_deficiencia_nutricional", datos.sintoma_deficiencia_nutricional },
                    { "frutas_verduras", datos.frutas_verduras },
                    { "agua_diaria", datos.cantidadAguaDia }
        };

                // 👉 Serializar datos a JSON con nombres compatibles con la IA
                var json = JsonSerializer.Serialize(datosTransformados);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                // 👉 Obtener URL de la IA desde appsettings.json
                var iaBaseUrl = _config.GetSection("IAService")["BaseUrl"];
                if (string.IsNullOrEmpty(iaBaseUrl))
                    return StatusCode(500, "URL de la IA no configurada.");

                // 👉 Llamar a la API de IA en Flask
                var response = await _httpClient.PostAsync($"{iaBaseUrl}/predecir", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorDetail = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"Error al comunicarse con la IA: {errorDetail}");
                }
                    


                // 👉 Obtener respuesta de la IA
                var resultado = await response.Content.ReadAsStringAsync();

                    // 👉 Deserializar JSON de la IA
                    var resultadoIA = JsonSerializer.Deserialize<Dictionary<string, string>>(resultado);
            if (resultadoIA == null || !resultadoIA.ContainsKey("diagnostico_final"))
                    return StatusCode(500, "La IA no devolvió un resultado válido.");

                // 👉 Guardar los datos ingresados + respuesta IA en la BD
                var prediccionEntity = new PrediccionIA
                {   
                    Dni = datos.Dni,
                    edad = edad,
                    sexo = sexo,
                    peso = datos.peso,
                    talla = datos.talla,
                    presion_sistolica = datos.presion_sistolica,
                    presion_diastolica = datos.presion_diastolica,
                    frecuencia_cardiaca = datos.frecuencia_cardiaca,
                    frecuencia_respiratoria = datos.frecuencia_respiratoria,
                    temperatura = datos.temperatura,
                    saturacion_oxigeno = datos.saturacion_oxigeno,
                    imc = datos.imc,
                    glucosa_capilar = datos.glucosa_capilar,
                    variable_auxiliar = datos.variable_auxiliar,
                    antecedente_uso_ains = datos.antecedente_uso_ains,
                    alergias_conocidas = datos.alergias_conocidas,
                    fatiga = datos.fatiga,
                    antecedente_eda = datos.antecedente_eda,
                    antecedentes_diabetes_familiar = datos.antecedentes_diabetes_familiar,
                    perdida_peso_no_intencional = datos.perdida_peso_no_intencional,
                    fatiga_2 = datos.fatiga_2,
                    nauseas = datos.nauseas,
                    antecedente_tabaquismo = datos.antecedente_tabaquismo,
                    duracion_sintomas_dias = datos.duracion_sintomas_dias,
                    dolor_abdominal = datos.dolor_abdominal,
                    zona_dolor_abdominal = datos.zona_dolor_abdominal,
                    cambios_deposiciones = datos.cambios_deposiciones,
                    sangrado_digestivo = datos.sangrado_digestivo,
                    infecciones_recientes = datos.infecciones_recientes,
                    perdida_apetito = datos.perdida_apetito,
                    vomitos = datos.vomitos,
                    distension_abdominal = datos.distension_abdominal,
                    diarrea = datos.diarrea,
                    reflujo_gastroesofagico = datos.reflujo_gastroesofagico,
                    antecedente_gastritis = datos.antecedente_gastritis,
                    antecedente_ulcera = datos.antecedente_ulcera,
                    antecedente_colitis = datos.antecedente_colitis,
                    frecuencia_ultraprocesados = datos.frecuencia_ultraprocesados,
                    cantidad_comidas_dia = datos.cantidad_comidas_dia,
                    perdida_peso_nutricion = datos.perdida_peso_nutricion,
                    sintoma_deficiencia_nutricional = datos.sintoma_deficiencia_nutricional,
                    frutas_verduras = datos.frutas_verduras,
                    cantidadAguaDia = datos.cantidadAguaDia,
                
                 // 👉 Campos provenientes del modelo de IA
                    DiagnosticoFinal = resultadoIA["diagnostico_final"],
                    DiagnosticoMedico = resultadoIA["diagnostico_medico"],
                    DiagnosticoGastro = resultadoIA["diagnostico_gastro"],
                    DiagnosticoNutri = resultadoIA["diagnostico_nutri"],
                    Porcentaje = resultadoIA["porcentaje"]
            };

                await _context.PrediccionesIA.AddAsync(prediccionEntity);
                await _context.SaveChangesAsync();

                // 👉 Devolver resultado IA como respuesta al usuario
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
    }

    [Table("EvaluacionGeneral")]
    public class EvaluacionGeneral
    {
        [Key]
        public int Id { get; set; }

        // Este campo Dni NO debe ser ingresado manualmente por el doctor en cada paso.
        // El sistema debe asignar automáticamente el Dni del paciente que está en proceso de registro.
        [Required]
        [StringLength(8)]
        public string Dni { get; set; } = string.Empty;

        public string? antecedente_uso_ains { get; set; }
        public string? alergias_conocidas { get; set; }
        public string? fatiga { get; set; }
        public string? antecedente_eda { get; set; }
        public string? antecedentes_diabetes_familiar { get; set; }
        public string? perdida_peso_no_intencional { get; set; }
        public string? fatiga_2 { get; set; }
        public string? nauseas { get; set; }
        public string? antecedente_tabaquismo { get; set; }
    }
}
