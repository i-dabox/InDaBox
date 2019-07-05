using Microsoft.EntityFrameworkCore.Migrations;

namespace InDaBox.Data.Migrations
{
    public partial class codigoPostalIntToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodigoPostal",
                table: "Almacen",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CodigoPostal",
                table: "Almacen",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5);
        }
    }
}
