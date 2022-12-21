using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApi.Data.Migrations
{
    public partial class AddFieldTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceAccountName",
                table: "Transaction",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceAccountName",
                table: "Transaction");
        }
    }
}
