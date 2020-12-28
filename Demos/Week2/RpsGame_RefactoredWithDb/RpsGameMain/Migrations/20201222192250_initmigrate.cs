using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RpsGame_NoDb.Migrations
{
    public partial class initmigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    playerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.playerId);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    matchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player1playerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Player2playerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.matchId);
                    table.ForeignKey(
                        name: "FK_matches_players_Player1playerId",
                        column: x => x.Player1playerId,
                        principalTable: "players",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_players_Player2playerId",
                        column: x => x.Player2playerId,
                        principalTable: "players",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rounds",
                columns: table => new
                {
                    roundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player1Choice = table.Column<int>(type: "int", nullable: false),
                    Player2Choice = table.Column<int>(type: "int", nullable: false),
                    WinningPlayerplayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rounds", x => x.roundId);
                    table.ForeignKey(
                        name: "FK_rounds_players_WinningPlayerplayerId",
                        column: x => x.WinningPlayerplayerId,
                        principalTable: "players",
                        principalColumn: "playerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_matches_Player1playerId",
                table: "matches",
                column: "Player1playerId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_Player2playerId",
                table: "matches",
                column: "Player2playerId");

            migrationBuilder.CreateIndex(
                name: "IX_rounds_WinningPlayerplayerId",
                table: "rounds",
                column: "WinningPlayerplayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "rounds");

            migrationBuilder.DropTable(
                name: "players");
        }
    }
}
