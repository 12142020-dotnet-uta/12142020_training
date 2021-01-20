using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
	public partial class initmigration : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "players",
				columns: table => new
				{
					playerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Fname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
					Lname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
					numWins = table.Column<int>(type: "int", nullable: false),
					numLosses = table.Column<int>(type: "int", nullable: false),
					ByteArrayImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
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
					Player2playerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					p1RoundWins = table.Column<int>(type: "int", nullable: false),
					p2RoundWins = table.Column<int>(type: "int", nullable: false),
					ties = table.Column<int>(type: "int", nullable: false)
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
					RoundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					Player1Choice = table.Column<int>(type: "int", nullable: false),
					Player2Choice = table.Column<int>(type: "int", nullable: false),
					WinningPlayerplayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_rounds", x => x.RoundId);
					table.ForeignKey(
						name: "FK_rounds_matches_MatchId",
						column: x => x.MatchId,
						principalTable: "matches",
						principalColumn: "matchId",
						onDelete: ReferentialAction.Restrict);
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
				name: "IX_rounds_MatchId",
				table: "rounds",
				column: "MatchId");

			migrationBuilder.CreateIndex(
				name: "IX_rounds_WinningPlayerplayerId",
				table: "rounds",
				column: "WinningPlayerplayerId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "rounds");

			migrationBuilder.DropTable(
				name: "matches");

			migrationBuilder.DropTable(
				name: "players");
		}
	}
}
