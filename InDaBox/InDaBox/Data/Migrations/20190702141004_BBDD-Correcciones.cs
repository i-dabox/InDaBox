using Microsoft.EntityFrameworkCore.Migrations;

namespace InDaBox.Data.Migrations
{
    public partial class BBDDCorrecciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroDeColumnas",
                table: "Seccion");

            migrationBuilder.DropColumn(
                name: "NumeroDeFilas",
                table: "Seccion");

            migrationBuilder.DropColumn(
                name: "NumeroDeSecciones",
                table: "Pasillo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumeroDeColumnas",
                table: "Seccion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroDeFilas",
                table: "Seccion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroDeSecciones",
                table: "Pasillo",
                nullable: false,
                defaultValue: 0);
        }
    }
}
