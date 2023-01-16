using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppApi.Data.Migrations
{
    public partial class BillsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BillNumber = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Month = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Ammount = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    AmmountToReturn = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfCreation = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserEntityId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_AspNetUsers_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillNumber",
                table: "Bills",
                column: "BillNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserEntityId",
                table: "Bills",
                column: "UserEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");
        }
    }
}
