using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AsistMedAPI.Migrations
{
    /// <inheritdoc />
    public partial class InicialPrediccionIA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrediccionesIA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Edad = table.Column<int>(type: "integer", nullable: false),
                    Sexo = table.Column<string>(type: "text", nullable: true),
                    Peso = table.Column<double>(type: "double precision", nullable: false),
                    Talla = table.Column<double>(type: "double precision", nullable: false),
                    PresionSistolica = table.Column<double>(type: "double precision", nullable: false),
                    PresionDiastolica = table.Column<double>(type: "double precision", nullable: false),
                    FrecuenciaCardiaca = table.Column<int>(type: "integer", nullable: false),
                    FrecuenciaRespiratoria = table.Column<int>(type: "integer", nullable: false),
                    Temperatura = table.Column<double>(type: "double precision", nullable: false),
                    SaturacionOxigeno = table.Column<double>(type: "double precision", nullable: false),
                    GlucosaCapilar = table.Column<double>(type: "double precision", nullable: false),
                    VariableAuxiliar = table.Column<string>(type: "text", nullable: true),
                    DolorAbdominal = table.Column<bool>(type: "boolean", nullable: false),
                    ZonaDolorAbdominal = table.Column<string>(type: "text", nullable: true),
                    Vomitos = table.Column<bool>(type: "boolean", nullable: false),
                    Nauseas = table.Column<bool>(type: "boolean", nullable: false),
                    Diarrea = table.Column<bool>(type: "boolean", nullable: false),
                    SangradoDigestivo = table.Column<bool>(type: "boolean", nullable: false),
                    ReflujoGastroesofagico = table.Column<bool>(type: "boolean", nullable: false),
                    CambiosDeposiciones = table.Column<bool>(type: "boolean", nullable: false),
                    DistensionAbdominal = table.Column<bool>(type: "boolean", nullable: false),
                    Fatiga = table.Column<bool>(type: "boolean", nullable: false),
                    PerdidaPeso = table.Column<bool>(type: "boolean", nullable: false),
                    InfeccionesRecientes = table.Column<bool>(type: "boolean", nullable: false),
                    AntecedenteEDA = table.Column<bool>(type: "boolean", nullable: false),
                    AntecedenteUsoAines = table.Column<bool>(type: "boolean", nullable: false),
                    AntecedenteTabaquismo = table.Column<bool>(type: "boolean", nullable: false),
                    AntecedentesDiabetesFamiliar = table.Column<bool>(type: "boolean", nullable: false),
                    TieneAlergias = table.Column<bool>(type: "boolean", nullable: false),
                    DiagnosticoFinal = table.Column<string>(type: "text", nullable: true),
                    DiagnosticoMedico = table.Column<string>(type: "text", nullable: true),
                    DiagnosticoGastro = table.Column<string>(type: "text", nullable: true),
                    DiagnosticoNutri = table.Column<string>(type: "text", nullable: true),
                    Porcentaje = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrediccionesIA", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrediccionesIA");
        }
    }
}
