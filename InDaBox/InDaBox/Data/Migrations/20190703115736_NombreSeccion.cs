using Microsoft.EntityFrameworkCore.Migrations;

namespace InDaBox.Data.Migrations
{
    public partial class NombreSeccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreSeccion",
                table: "Seccion",
                newName: "Nombre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Seccion",
                newName: "NombreSeccion");
        }
    }
}
