using System.ComponentModel.DataAnnotations; // Importamos librería que permite validar campos con reglas como obligatorio, rango o formato
using System.ComponentModel.DataAnnotations.Schema; // Importamos librería que permite mapear esta clase directamente a una tabla de base de datos

namespace AsistMedAPI.Models // Definimos el espacio de nombres del proyecto donde estará agrupada la clase Paciente
{
    [Table("paciente")] // Indicamos que esta clase se guardará en la tabla llamada "paciente" en la base de datos
    public class Paciente // Definimos la clase Paciente que representará a cada paciente registrado en el sistema
    {
        [Key] // Definimos que esta propiedad será la clave primaria en la base de datos
        [Required] // Indicamos que este campo es obligatorio y no puede quedar vacío
        [StringLength(8)] // Limitamos el campo a un máximo de 8 caracteres para evitar datos inválidos
        public string Dni { get; set; } = null!; // Guardamos el DNI del paciente como identificador único, con el operador null! para evitar advertencias de compilador

        [Required] // Campo obligatorio que siempre debe ser registrado
        [Range(0, 110, ErrorMessage = "La edad debe estar entre 0 y 110 años.")] // Validamos que la edad esté dentro de un rango clínico realista
        public int edad { get; set; } // Guardamos la edad del paciente en años

        [Required] // El campo sexo es obligatorio para el registro
        [RegularExpression("^(M|F)$", ErrorMessage = "El sexo debe ser 'M' o 'F'.")] // Validamos que solo se pueda registrar M o F como sexo
        public string sexo { get; set; } = null!; // Guardamos el sexo del paciente, asegurando que no quede vacío

        [StringLength(15)] // Permitimos máximo 15 caracteres para que no se ingresen teléfonos incorrectos
        public string? telefono { get; set; } // Guardamos el teléfono del paciente como opcional (puede estar vacío)

        [StringLength(200)] // Permitimos máximo 200 caracteres en la dirección
        public string? direccion { get; set; } // Guardamos la dirección del paciente como opcional

        public int? UsuarioId { get; set; }   // Guardamos el ID de usuario que registró al paciente, pero lo dejamos opcional (nullable) para no romper compatibilidad con datos
        public Usuario? Usuario { get; set; } // Definimos la relación con la clase Usuario para saber qué usuario creó o gestiona este paciente
    }
}
