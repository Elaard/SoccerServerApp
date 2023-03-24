using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.EFCore.Migrations
{
    public partial class AddUrlTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UrlDataId",
                table: "Leagues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UrlDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlForHtml = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlDatas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_UrlDataId",
                table: "Leagues",
                column: "UrlDataId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_UrlDatas_UrlDataId",
                table: "Leagues",
                column: "UrlDataId",
                principalTable: "UrlDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_UrlDatas_UrlDataId",
                table: "Leagues");

            migrationBuilder.DropTable(
                name: "UrlDatas");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_UrlDataId",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "UrlDataId",
                table: "Leagues");
        }
    }
}
