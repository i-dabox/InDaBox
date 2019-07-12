using Microsoft.EntityFrameworkCore.Migrations;

namespace InDaBox.Data.Migrations
{
    public partial class FiltrosComunes5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Contador",
                table: "FiltrosComunes",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Contador",
                table: "FiltrosComunes",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
