using Microsoft.EntityFrameworkCore;
using AsistMedAPI.Models;

namespace AsistMedAPI.Data
{
    public class AsistMedContext : DbContext
    {
        public AsistMedContext(DbContextOptions<AsistMedContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<SignosVitales> SignosVitales { get; set; }
        public DbSet<EvaluacionClinica> EvaluacionesClinicas { get; set; }
        public DbSet<SintomasDigestivos> SintomasDigestivos { get; set; }
        public DbSet<EvaluacionNutricional> EvaluacionesNutricionales { get; set; }

        public DbSet<PrediccionIA> PrediccionesIA { get; set; }
        public DbSet<PrediccionDetallePdf> PrediccionesPdf { get; set; }
    }
}
