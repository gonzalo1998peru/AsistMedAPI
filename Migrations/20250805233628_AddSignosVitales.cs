using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AsistMedAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSignosVitales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sexo",
                table: "paciente",
                newName: "sexo");

            migrationBuilder.RenameColumn(
                name: "Edad",
                table: "paciente",
                newName: "edad");

            migrationBuilder.AddColumn<string>(
                name: "Dni",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SignosVitales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Dni = table.Column<string>(type: "text", nullable: false),
                    peso = table.Column<float>(type: "real", nullable: false),
                    talla = table.Column<float>(type: "real", nullable: false),
                    presionsistolica = table.Column<int>(name: "presion_sistolica", type: "integer", nullable: false),
                    presiondiastolica = table.Column<int>(name: "presion_diastolica", type: "integer", nullable: false),
                    frecuenciacardiaca = table.Column<int>(name: "frecuencia_cardiaca", type: "integer", nullable: false),
                    frecuenciarespiratoria = table.Column<int>(name: "frecuencia_respiratoria", type: "integer", nullable: false),
                    temperatura = table.Column<float>(type: "real", nullable: false),
                    saturacionoxigeno = table.Column<int>(name: "saturacion_oxigeno", type: "integer", nullable: false),
                    imc = table.Column<float>(type: "real", nullable: false),
                    glucosacapilar = table.Column<int>(name: "glucosa_capilar", type: "integer", nullable: false),
                    fecharegistro = table.Column<DateTime>(name: "fecha_registro", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignosVitales", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignosVitales");

            migrationBuilder.DropColumn(
                name: "Dni",
                table: "PrediccionesIA");

            migrationBuilder.RenameColumn(
                name: "sexo",
                table: "paciente",
                newName: "Sexo");

            migrationBuilder.RenameColumn(
                name: "edad",
                table: "paciente",
                newName: "Edad");
        }
    }
}
