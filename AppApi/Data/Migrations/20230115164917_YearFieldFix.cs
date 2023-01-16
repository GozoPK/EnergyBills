using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppApi.Data.Migrations
{
    public partial class YearFieldFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Year",
                table: "Bills",
                type: "year(4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Bills",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "year(4)");
        }
    }
}
