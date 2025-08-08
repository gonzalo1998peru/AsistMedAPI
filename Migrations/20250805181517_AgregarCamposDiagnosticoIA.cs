using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsistMedAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCamposDiagnosticoIA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiagnosticoFinal",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosticoGastro",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosticoMedico",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosticoNutri",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Porcentaje",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiagnosticoFinal",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "DiagnosticoGastro",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "DiagnosticoMedico",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "DiagnosticoNutri",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "Porcentaje",
                table: "PrediccionesIA");
        }
    }
}
