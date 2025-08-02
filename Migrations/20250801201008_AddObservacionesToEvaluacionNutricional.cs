using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsistMedAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddObservacionesToEvaluacionNutricional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.RenameColumn(
                name: "zona_dolor",
                table: "sintomas_digestivos",
                newName: "zona_dolor_abdominal");

            migrationBuilder.RenameColumn(
                name: "frecuencia_dolor",
                table: "sintomas_digestivos",
                newName: "notas_especialista");

            migrationBuilder.RenameColumn(
                name: "dolor_estomacal",
                table: "sintomas_digestivos",
                newName: "reflujo_gastroesofagico");

            migrationBuilder.RenameColumn(
                name: "dolor_abdominal",
                table: "evaluacion_clinica",
                newName: "perdida_peso_no_intencional");

            migrationBuilder.RenameColumn(
                name: "antecedentes_patologicos",
                table: "evaluacion_clinica",
                newName: "nauseas");

            migrationBuilder.AddColumn<bool>(
                name: "antecedente_colitis",
                table: "sintomas_digestivos",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "antecedente_gastritis",
                table: "sintomas_digestivos",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "antecedente_ulcera",
                table: "sintomas_digestivos",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "cambios_deposiciones",
                table: "sintomas_digestivos",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "diarrea",
                table: "sintomas_digestivos",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "distension_abdominal",
                table: "sintomas_digestivos",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "dolor_abdominal",
                table: "sintomas_digestivos",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "duracion_sintomas_dias",
                table: "sintomas_digestivos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "estrenimiento",
                table: "sintomas_digestivos",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "infecciones_recientes",
                table: "sintomas_digestivos",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "perdida_apetito",
                table: "sintomas_digestivos",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "presion_diastolica",
                table: "signos_vitales",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "presion_sistolica",
                table: "signos_vitales",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "antecedente_diabetes_familiar",
                table: "evaluacion_clinica",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "antecedente_eda",
                table: "evaluacion_clinica",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "antecedente_tabaquismo",
                table: "evaluacion_clinica",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "antecedente_uso_ains",
                table: "evaluacion_clinica",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "observaciones_generales",
                table: "evaluacion_clinica",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "antecedente_colitis",
                table: "sintomas_digestivos");

            migrationBuilder.DropColumn(
                name: "antecedente_gastritis",
                table: "sintomas_digestivos");

            migrationBuilder.DropColumn(
                name: "antecedente_ulcera",
                table: "sintomas_digestivos");

            migrationBuilder.DropColumn(
                name: "cambios_deposiciones",
                table: "sintomas_digestivos");

            migrationBuilder.DropColumn(
                name: "diarrea",
                table: "sintomas_digestivos");

            migrationBuilder.DropColumn(
                name: "distension_abdominal",
                table: "sintomas_digestivos");

            migrationBuilder.DropColumn(
                name: "dolor_abdominal",
                table: "sintomas_digestivos");

            migrationBuilder.DropColumn(
                name: "duracion_sintomas_dias",
                table: "sintomas_digestivos");

            migrationBuilder.DropColumn(
                name: "estrenimiento",
                table: "sintomas_digestivos");

            migrationBuilder.DropColumn(
                name: "infecciones_recientes",
                table: "sintomas_digestivos");

            migrationBuilder.DropColumn(
                name: "perdida_apetito",
                table: "sintomas_digestivos");

            migrationBuilder.DropColumn(
                name: "presion_diastolica",
                table: "signos_vitales");

            migrationBuilder.DropColumn(
                name: "presion_sistolica",
                table: "signos_vitales");

            migrationBuilder.DropColumn(
                name: "antecedente_diabetes_familiar",
                table: "evaluacion_clinica");

            migrationBuilder.DropColumn(
                name: "antecedente_eda",
                table: "evaluacion_clinica");

            migrationBuilder.DropColumn(
                name: "antecedente_tabaquismo",
                table: "evaluacion_clinica");

            migrationBuilder.DropColumn(
                name: "antecedente_uso_ains",
                table: "evaluacion_clinica");

            migrationBuilder.DropColumn(
                name: "observaciones_generales",
                table: "evaluacion_clinica");

            migrationBuilder.RenameColumn(
                name: "zona_dolor_abdominal",
                table: "sintomas_digestivos",
                newName: "zona_dolor");

            migrationBuilder.RenameColumn(
                name: "reflujo_gastroesofagico",
                table: "sintomas_digestivos",
                newName: "dolor_estomacal");

            migrationBuilder.RenameColumn(
                name: "notas_especialista",
                table: "sintomas_digestivos",
                newName: "frecuencia_dolor");

            migrationBuilder.RenameColumn(
                name: "perdida_peso_no_intencional",
                table: "evaluacion_clinica",
                newName: "dolor_abdominal");

            migrationBuilder.RenameColumn(
                name: "nauseas",
                table: "evaluacion_clinica",
                newName: "antecedentes_patologicos");

            
        }
    }
}
