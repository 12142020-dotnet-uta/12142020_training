using Microsoft.EntityFrameworkCore.Migrations;

namespace RpsGame_NoDb.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "numLosses",
                table: "players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "numWins",
                table: "players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numLosses",
                table: "players");

            migrationBuilder.DropColumn(
                name: "numWins",
                table: "players");
        }
    }
}
