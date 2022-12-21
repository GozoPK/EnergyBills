using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppApi.Data.Migrations
{
    public partial class UniqueAfm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Afm",
                table: "Users",
                column: "Afm",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Afm",
                table: "Users");
        }
    }
}
