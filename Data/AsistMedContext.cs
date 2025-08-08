using Microsoft.EntityFrameworkCore;
using AsistMedAPI.Models;


namespace AsistMedAPI.Data
{
    public class AsistMedContext : DbContext
    {
        public AsistMedContext(DbContextOptions<AsistMedContext> options) : base(options)
        {
        }

        public DbSet<PrediccionIA> PrediccionesIA { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<SignosVitales> SignosVitales { get; set; }

        public DbSet<EvaluacionGeneralMed> EvaluacionGeneralMed { get; set; }

        public DbSet<EvaluacionGI> EvaluacionGI { get; set; }

        public DbSet<EvaluacionNutricion> EvaluacionNutricion { get; set; }    
    }
}
