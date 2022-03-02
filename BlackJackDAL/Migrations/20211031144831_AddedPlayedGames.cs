using Microsoft.EntityFrameworkCore.Migrations;

namespace BlackJackDAL.Migrations
{
    public partial class AddedPlayedGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayedGamesId",
                table: "PlayerInfo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlayedGames",
                columns: table => new
                {
                    PlayedGamesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsGameWon = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayedGames", x => x.PlayedGamesId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerInfo_PlayedGamesId",
                table: "PlayerInfo",
                column: "PlayedGamesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerInfo_PlayedGames_PlayedGamesId",
                table: "PlayerInfo",
                column: "PlayedGamesId",
                principalTable: "PlayedGames",
                principalColumn: "PlayedGamesId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerInfo_PlayedGames_PlayedGamesId",
                table: "PlayerInfo");

            migrationBuilder.DropTable(
                name: "PlayedGames");

            migrationBuilder.DropIndex(
                name: "IX_PlayerInfo_PlayedGamesId",
                table: "PlayerInfo");

            migrationBuilder.DropColumn(
                name: "PlayedGamesId",
                table: "PlayerInfo");
        }
    }
}
