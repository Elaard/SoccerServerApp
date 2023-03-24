using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.EFCore.Migrations
{
    public partial class addComplexIndexToTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_Name",
                table: "Teams");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name_Id",
                table: "Teams",
                columns: new[] { "Name", "Id" },
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_Name_Id",
                table: "Teams");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name",
                table: "Teams",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
