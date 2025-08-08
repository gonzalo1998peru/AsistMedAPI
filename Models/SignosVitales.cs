using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistMedAPI.Models
{
    [Table("SignosVitales")]
    public class SignosVitales
    {
        public int Id { get; set; }

        // Relación con el paciente
        public string Dni { get; set; } = string.Empty;

        // Signos clínicos
        public float peso { get; set; }
        public float talla { get; set; }
        public int presion_sistolica { get; set; }
        public int presion_diastolica { get; set; }
        public int frecuencia_cardiaca { get; set; }
        public int frecuencia_respiratoria { get; set; }
        public float temperatura { get; set; }
        public int saturacion_oxigeno { get; set; }
        public float imc { get; set; }
        public int glucosa_capilar { get; set; }
        public string? VariableAuxiliar { get; set; }

        public DateTime fecha_registro { get; set; } = DateTime.UtcNow;
    }
}
