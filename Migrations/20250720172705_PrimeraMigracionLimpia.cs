using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AsistMedAPI.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracionLimpia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "paciente",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dni = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    nombres = table.Column<string>(type: "text", nullable: false),
                    apellido_paterno = table.Column<string>(type: "text", nullable: false),
                    apellido_materno = table.Column<string>(type: "text", nullable: false),
                    sexo = table.Column<string>(type: "text", nullable: false),
                    edad = table.Column<int>(type: "integer", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paciente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "evaluacion_clinica",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    paciente_id = table.Column<int>(type: "integer", nullable: false),
                    fatiga = table.Column<bool>(type: "boolean", nullable: true),
                    dolor_abdominal = table.Column<bool>(type: "boolean", nullable: true),
                    medicamentos_actuales = table.Column<bool>(type: "boolean", nullable: true),
                    alergias = table.Column<bool>(type: "boolean", nullable: true),
                    antecedentes_patologicos = table.Column<bool>(type: "boolean", nullable: true),
                    fecha_evaluacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluacion_clinica", x => x.id);
                    table.ForeignKey(
                        name: "FK_evaluacion_clinica_paciente_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "paciente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "evaluacion_nutricional",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    paciente_id = table.Column<int>(type: "integer", nullable: false),
                    consumo_ultraprocesados = table.Column<bool>(type: "boolean", nullable: true),
                    cantidad_agua_dia = table.Column<int>(type: "integer", nullable: true),
                    frutas_por_semana = table.Column<int>(type: "integer", nullable: true),
                    verduras_por_semana = table.Column<int>(type: "integer", nullable: true),
                    comidas_al_dia = table.Column<int>(type: "integer", nullable: true),
                    desayuno_diario = table.Column<bool>(type: "boolean", nullable: true),
                    refrigerio = table.Column<bool>(type: "boolean", nullable: true),
                    fecha_evaluacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluacion_nutricional", x => x.id);
                    table.ForeignKey(
                        name: "FK_evaluacion_nutricional_paciente_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "paciente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prediccion_detalle_pdf",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    diagnostico_final = table.Column<string>(type: "text", nullable: true),
                    riesgos_detectados = table.Column<string>(type: "text", nullable: true),
                    recomendaciones_clinicas = table.Column<string>(type: "text", nullable: true),
                    fecha_generacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    paciente_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prediccion_detalle_pdf", x => x.id);
                    table.ForeignKey(
                        name: "FK_prediccion_detalle_pdf_paciente_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "paciente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prediccion_ia",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    paciente_id = table.Column<int>(type: "integer", nullable: false),
                    riesgo_digestivo = table.Column<string>(type: "text", nullable: true),
                    riesgo_nutricional = table.Column<string>(type: "text", nullable: true),
                    riesgo_clinico = table.Column<string>(type: "text", nullable: true),
                    enfermedad_predicha = table.Column<string>(type: "text", nullable: true),
                    porcentaje_confianza = table.Column<double>(type: "double precision", nullable: true),
                    fecha_prediccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    informe_pdf = table.Column<string>(type: "text", nullable: true),
                    historial_descriptivo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prediccion_ia", x => x.id);
                    table.ForeignKey(
                        name: "FK_prediccion_ia_paciente_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "paciente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "signos_vitales",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    paciente_id = table.Column<int>(type: "integer", nullable: false),
                    
                    frecuencia_cardiaca = table.Column<int>(type: "integer", nullable: true),
                    frecuencia_respiratoria = table.Column<int>(type: "integer", nullable: true),
                    temperatura = table.Column<double>(type: "double precision", nullable: true),
                    saturacion_oxigeno = table.Column<int>(type: "integer", nullable: true),
                    talla_cm = table.Column<double>(type: "double precision", nullable: true),
                    peso_kg = table.Column<double>(type: "double precision", nullable: true),
                    imc = table.Column<double>(type: "double precision", nullable: true),
                    glucosa_mg_dl = table.Column<double>(type: "double precision", nullable: true),
                    fecha_registro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_signos_vitales", x => x.id);
                    table.ForeignKey(
                        name: "FK_signos_vitales_paciente_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "paciente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sintomas_digestivos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    paciente_id = table.Column<int>(type: "integer", nullable: false),
                    dolor_estomacal = table.Column<bool>(type: "boolean", nullable: true),
                    nausea = table.Column<bool>(type: "boolean", nullable: true),
                    vomito = table.Column<bool>(type: "boolean", nullable: true),
                    ardor_estomacal = table.Column<bool>(type: "boolean", nullable: true),
                    sangrado_digestivo = table.Column<bool>(type: "boolean", nullable: true),
                    zona_dolor = table.Column<string>(type: "text", nullable: true),
                    frecuencia_dolor = table.Column<string>(type: "text", nullable: true),
                    fecha_evaluacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sintomas_digestivos", x => x.id);
                    table.ForeignKey(
                        name: "FK_sintomas_digestivos_paciente_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "paciente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_evaluacion_clinica_paciente_id",
                table: "evaluacion_clinica",
                column: "paciente_id");

            migrationBuilder.CreateIndex(
                name: "IX_evaluacion_nutricional_paciente_id",
                table: "evaluacion_nutricional",
                column: "paciente_id");

            migrationBuilder.CreateIndex(
                name: "IX_prediccion_detalle_pdf_paciente_id",
                table: "prediccion_detalle_pdf",
                column: "paciente_id");

            migrationBuilder.CreateIndex(
                name: "IX_prediccion_ia_paciente_id",
                table: "prediccion_ia",
                column: "paciente_id");

            migrationBuilder.CreateIndex(
                name: "IX_signos_vitales_paciente_id",
                table: "signos_vitales",
                column: "paciente_id");

            migrationBuilder.CreateIndex(
                name: "IX_sintomas_digestivos_paciente_id",
                table: "sintomas_digestivos",
                column: "paciente_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "evaluacion_clinica");

            migrationBuilder.DropTable(
                name: "evaluacion_nutricional");

            migrationBuilder.DropTable(
                name: "prediccion_detalle_pdf");

            migrationBuilder.DropTable(
                name: "prediccion_ia");

            migrationBuilder.DropTable(
                name: "signos_vitales");

            migrationBuilder.DropTable(
                name: "sintomas_digestivos");

            migrationBuilder.DropTable(
                name: "paciente");
        }
    }
}
