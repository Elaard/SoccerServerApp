using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.EFCore.Migrations
{
    public partial class removepropertiesfromteam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_UrlDatas_UrlDataId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "UrlDatas");

            migrationBuilder.DropIndex(
                name: "IX_Teams_UrlDataId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "UrlDataId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Teams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UrlDataId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UrlDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlForHtml = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlDatas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_UrlDataId",
                table: "Teams",
                column: "UrlDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_UrlDatas_UrlDataId",
                table: "Teams",
                column: "UrlDataId",
                principalTable: "UrlDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
