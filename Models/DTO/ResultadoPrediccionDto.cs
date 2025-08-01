namespace AsistMedAPI.Models.DTO
{
    public class ResultadoPrediccionDto
    {
        public string Enfermedad { get; set; } = string.Empty;
        public double Porcentaje { get; set; }
        public string Factores { get; set; } = string.Empty;
    }
}
