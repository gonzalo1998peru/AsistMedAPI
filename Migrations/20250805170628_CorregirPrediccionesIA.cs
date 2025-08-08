using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsistMedAPI.Migrations
{
    /// <inheritdoc />
    public partial class CorregirPrediccionesIA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AntecedenteEDA",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "AntecedenteTabaquismo",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "AntecedenteUsoAines",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "AntecedentesDiabetesFamiliar",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "CambiosDeposiciones",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "DistensionAbdominal",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "DolorAbdominal",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "GlucosaCapilar",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "InfeccionesRecientes",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "PerdidaPeso",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "PresionDiastolica",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "PresionSistolica",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "ReflujoGastroesofagico",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "SangradoDigestivo",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "SaturacionOxigeno",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "TieneAlergias",
                table: "PrediccionesIA");

            migrationBuilder.RenameColumn(
                name: "Vomitos",
                table: "PrediccionesIA",
                newName: "vomitos");

            migrationBuilder.RenameColumn(
                name: "Temperatura",
                table: "PrediccionesIA",
                newName: "temperatura");

            migrationBuilder.RenameColumn(
                name: "Talla",
                table: "PrediccionesIA",
                newName: "talla");

            migrationBuilder.RenameColumn(
                name: "Sexo",
                table: "PrediccionesIA",
                newName: "sexo");

            migrationBuilder.RenameColumn(
                name: "Peso",
                table: "PrediccionesIA",
                newName: "peso");

            migrationBuilder.RenameColumn(
                name: "Nauseas",
                table: "PrediccionesIA",
                newName: "nauseas");

            migrationBuilder.RenameColumn(
                name: "Fatiga",
                table: "PrediccionesIA",
                newName: "fatiga");

            migrationBuilder.RenameColumn(
                name: "Edad",
                table: "PrediccionesIA",
                newName: "edad");

            migrationBuilder.RenameColumn(
                name: "Diarrea",
                table: "PrediccionesIA",
                newName: "diarrea");

            migrationBuilder.RenameColumn(
                name: "ZonaDolorAbdominal",
                table: "PrediccionesIA",
                newName: "zona_dolor_abdominal");

            migrationBuilder.RenameColumn(
                name: "VariableAuxiliar",
                table: "PrediccionesIA",
                newName: "variable_auxiliar");

            migrationBuilder.RenameColumn(
                name: "Porcentaje",
                table: "PrediccionesIA",
                newName: "sintoma_deficiencia_nutricional");

            migrationBuilder.RenameColumn(
                name: "FrecuenciaRespiratoria",
                table: "PrediccionesIA",
                newName: "saturacion_oxigeno");

            migrationBuilder.RenameColumn(
                name: "FrecuenciaCardiaca",
                table: "PrediccionesIA",
                newName: "presion_sistolica");

            migrationBuilder.RenameColumn(
                name: "DiagnosticoNutri",
                table: "PrediccionesIA",
                newName: "sangrado_digestivo");

            migrationBuilder.RenameColumn(
                name: "DiagnosticoMedico",
                table: "PrediccionesIA",
                newName: "reflujo_gastroesofagico");

            migrationBuilder.RenameColumn(
                name: "DiagnosticoGastro",
                table: "PrediccionesIA",
                newName: "perdida_peso_nutricion");

            migrationBuilder.RenameColumn(
                name: "DiagnosticoFinal",
                table: "PrediccionesIA",
                newName: "perdida_peso_no_intencional");

            migrationBuilder.AlterColumn<string>(
                name: "vomitos",
                table: "PrediccionesIA",
                type: "text",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<float>(
                name: "temperatura",
                table: "PrediccionesIA",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "talla",
                table: "PrediccionesIA",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "peso",
                table: "PrediccionesIA",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<string>(
                name: "nauseas",
                table: "PrediccionesIA",
                type: "text",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "fatiga",
                table: "PrediccionesIA",
                type: "text",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "diarrea",
                table: "PrediccionesIA",
                type: "text",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<string>(
                name: "alergias_conocidas",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "antecedente_colitis",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "antecedente_eda",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "antecedente_gastritis",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "antecedente_tabaquismo",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "antecedente_ulcera",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "antecedente_uso_ains",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "antecedentes_diabetes_familiar",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cambios_deposiciones",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cantidadAguaDia",
                table: "PrediccionesIA",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cantidad_comidas_dia",
                table: "PrediccionesIA",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "distension_abdominal",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "dolor_abdominal",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "duracion_sintomas_dias",
                table: "PrediccionesIA",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "fatiga_2",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "frecuencia_cardiaca",
                table: "PrediccionesIA",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "frecuencia_respiratoria",
                table: "PrediccionesIA",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "frecuencia_ultraprocesados",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "frutas_verduras",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "glucosa_capilar",
                table: "PrediccionesIA",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "imc",
                table: "PrediccionesIA",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "infecciones_recientes",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "perdida_apetito",
                table: "PrediccionesIA",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "presion_diastolica",
                table: "PrediccionesIA",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "alergias_conocidas",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "antecedente_colitis",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "antecedente_eda",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "antecedente_gastritis",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "antecedente_tabaquismo",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "antecedente_ulcera",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "antecedente_uso_ains",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "antecedentes_diabetes_familiar",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "cambios_deposiciones",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "cantidadAguaDia",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "cantidad_comidas_dia",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "distension_abdominal",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "dolor_abdominal",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "duracion_sintomas_dias",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "fatiga_2",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "frecuencia_cardiaca",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "frecuencia_respiratoria",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "frecuencia_ultraprocesados",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "frutas_verduras",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "glucosa_capilar",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "imc",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "infecciones_recientes",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "perdida_apetito",
                table: "PrediccionesIA");

            migrationBuilder.DropColumn(
                name: "presion_diastolica",
                table: "PrediccionesIA");

            migrationBuilder.RenameColumn(
                name: "vomitos",
                table: "PrediccionesIA",
                newName: "Vomitos");

            migrationBuilder.RenameColumn(
                name: "temperatura",
                table: "PrediccionesIA",
                newName: "Temperatura");

            migrationBuilder.RenameColumn(
                name: "talla",
                table: "PrediccionesIA",
                newName: "Talla");

            migrationBuilder.RenameColumn(
                name: "sexo",
                table: "PrediccionesIA",
                newName: "Sexo");

            migrationBuilder.RenameColumn(
                name: "peso",
                table: "PrediccionesIA",
                newName: "Peso");

            migrationBuilder.RenameColumn(
                name: "nauseas",
                table: "PrediccionesIA",
                newName: "Nauseas");

            migrationBuilder.RenameColumn(
                name: "fatiga",
                table: "PrediccionesIA",
                newName: "Fatiga");

            migrationBuilder.RenameColumn(
                name: "edad",
                table: "PrediccionesIA",
                newName: "Edad");

            migrationBuilder.RenameColumn(
                name: "diarrea",
                table: "PrediccionesIA",
                newName: "Diarrea");

            migrationBuilder.RenameColumn(
                name: "zona_dolor_abdominal",
                table: "PrediccionesIA",
                newName: "ZonaDolorAbdominal");

            migrationBuilder.RenameColumn(
                name: "variable_auxiliar",
                table: "PrediccionesIA",
                newName: "VariableAuxiliar");

            migrationBuilder.RenameColumn(
                name: "sintoma_deficiencia_nutricional",
                table: "PrediccionesIA",
                newName: "Porcentaje");

            migrationBuilder.RenameColumn(
                name: "saturacion_oxigeno",
                table: "PrediccionesIA",
                newName: "FrecuenciaRespiratoria");

            migrationBuilder.RenameColumn(
                name: "sangrado_digestivo",
                table: "PrediccionesIA",
                newName: "DiagnosticoNutri");

            migrationBuilder.RenameColumn(
                name: "reflujo_gastroesofagico",
                table: "PrediccionesIA",
                newName: "DiagnosticoMedico");

            migrationBuilder.RenameColumn(
                name: "presion_sistolica",
                table: "PrediccionesIA",
                newName: "FrecuenciaCardiaca");

            migrationBuilder.RenameColumn(
                name: "perdida_peso_nutricion",
                table: "PrediccionesIA",
                newName: "DiagnosticoGastro");

            migrationBuilder.RenameColumn(
                name: "perdida_peso_no_intencional",
                table: "PrediccionesIA",
                newName: "DiagnosticoFinal");

            migrationBuilder.AlterColumn<bool>(
                name: "Vomitos",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Temperatura",
                table: "PrediccionesIA",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Talla",
                table: "PrediccionesIA",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Peso",
                table: "PrediccionesIA",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<bool>(
                name: "Nauseas",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Fatiga",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Diarrea",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AntecedenteEDA",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AntecedenteTabaquismo",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AntecedenteUsoAines",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AntecedentesDiabetesFamiliar",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CambiosDeposiciones",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DistensionAbdominal",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DolorAbdominal",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "GlucosaCapilar",
                table: "PrediccionesIA",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "InfeccionesRecientes",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PerdidaPeso",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "PresionDiastolica",
                table: "PrediccionesIA",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PresionSistolica",
                table: "PrediccionesIA",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "ReflujoGastroesofagico",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SangradoDigestivo",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "SaturacionOxigeno",
                table: "PrediccionesIA",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "TieneAlergias",
                table: "PrediccionesIA",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
