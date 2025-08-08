using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AsistMedAPI.Migrations
{
    /// <inheritdoc />
    public partial class Agregarespecialistas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Dni",
                table: "paciente",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "EvaluacionGeneralMed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Dni = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    antecedenteusoains = table.Column<string>(name: "antecedente_uso_ains", type: "text", nullable: true),
                    alergiasconocidas = table.Column<string>(name: "alergias_conocidas", type: "text", nullable: true),
                    fatiga = table.Column<string>(type: "text", nullable: true),
                    antecedenteeda = table.Column<string>(name: "antecedente_eda", type: "text", nullable: true),
                    antecedentesdiabetesfamiliar = table.Column<string>(name: "antecedentes_diabetes_familiar", type: "text", nullable: true),
                    perdidapesonointencional = table.Column<string>(name: "perdida_peso_no_intencional", type: "text", nullable: true),
                    fatiga2 = table.Column<string>(name: "fatiga_2", type: "text", nullable: true),
                    nauseas = table.Column<string>(type: "text", nullable: true),
                    antecedentetabaquismo = table.Column<string>(name: "antecedente_tabaquismo", type: "text", nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionGeneralMed", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionGI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Dni = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    duracionsintomasdias = table.Column<int>(name: "duracion_sintomas_dias", type: "integer", nullable: false),
                    dolorabdominal = table.Column<string>(name: "dolor_abdominal", type: "text", nullable: true),
                    zonadolorabdominal = table.Column<string>(name: "zona_dolor_abdominal", type: "text", nullable: true),
                    cambiosdeposiciones = table.Column<string>(name: "cambios_deposiciones", type: "text", nullable: true),
                    sangradodigestivo = table.Column<string>(name: "sangrado_digestivo", type: "text", nullable: true),
                    infeccionesrecientes = table.Column<string>(name: "infecciones_recientes", type: "text", nullable: true),
                    perdidaapetito = table.Column<string>(name: "perdida_apetito", type: "text", nullable: true),
                    vomitos = table.Column<string>(type: "text", nullable: true),
                    distensionabdominal = table.Column<string>(name: "distension_abdominal", type: "text", nullable: true),
                    diarrea = table.Column<string>(type: "text", nullable: true),
                    reflujogastroesofagico = table.Column<string>(name: "reflujo_gastroesofagico", type: "text", nullable: true),
                    antecedentegastritis = table.Column<string>(name: "antecedente_gastritis", type: "text", nullable: true),
                    antecedenteulcera = table.Column<string>(name: "antecedente_ulcera", type: "text", nullable: true),
                    antecedentecolitis = table.Column<string>(name: "antecedente_colitis", type: "text", nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionGI", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionNutricion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Dni = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    frecuenciaultraprocesados = table.Column<string>(name: "frecuencia_ultraprocesados", type: "text", nullable: true),
                    cantidadcomidasdia = table.Column<int>(name: "cantidad_comidas_dia", type: "integer", nullable: false),
                    perdidapesonutricion = table.Column<string>(name: "perdida_peso_nutricion", type: "text", nullable: true),
                    sintomadeficiencianutricional = table.Column<string>(name: "sintoma_deficiencia_nutricional", type: "text", nullable: true),
                    frutasverduras = table.Column<string>(name: "frutas_verduras", type: "text", nullable: true),
                    cantidadAguaDia = table.Column<int>(type: "integer", nullable: false),
                    observaciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionNutricion", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluacionGeneralMed");

            migrationBuilder.DropTable(
                name: "EvaluacionGI");

            migrationBuilder.DropTable(
                name: "EvaluacionNutricion");

            migrationBuilder.AlterColumn<string>(
                name: "Dni",
                table: "paciente",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8);
        }
    }
}
