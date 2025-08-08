using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AsistMedAPI.Migrations
{
    /// <inheritdoc />
    public partial class CrearPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pacientes",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Pacientes");

            migrationBuilder.RenameTable(
                name: "Pacientes",
                newName: "paciente");

            migrationBuilder.RenameColumn(
                name: "DNI",
                table: "paciente",
                newName: "Dni");

            migrationBuilder.AddPrimaryKey(
                name: "PK_paciente",
                table: "paciente",
                column: "Dni");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_paciente",
                table: "paciente");

            migrationBuilder.RenameTable(
                name: "paciente",
                newName: "Pacientes");

            migrationBuilder.RenameColumn(
                name: "Dni",
                table: "Pacientes",
                newName: "DNI");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Pacientes",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pacientes",
                table: "Pacientes",
                column: "Id");
        }
    }
}
