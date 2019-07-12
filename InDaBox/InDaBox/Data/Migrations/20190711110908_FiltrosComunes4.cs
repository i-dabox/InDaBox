using Microsoft.EntityFrameworkCore.Migrations;

namespace InDaBox.Data.Migrations
{
    public partial class FiltrosComunes4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Contador",
                table: "FiltrosComunes",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Contador",
                table: "FiltrosComunes",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
