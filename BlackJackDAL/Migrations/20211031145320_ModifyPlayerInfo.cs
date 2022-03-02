using Microsoft.EntityFrameworkCore.Migrations;

namespace BlackJackDAL.Migrations
{
    public partial class ModifyPlayerInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerInfo_PlayedGames_PlayedGamesId",
                table: "PlayerInfo");

            migrationBuilder.DropIndex(
                name: "IX_PlayerInfo_PlayedGamesId",
                table: "PlayerInfo");

            migrationBuilder.DropColumn(
                name: "PlayedGamesId",
                table: "PlayerInfo");

            migrationBuilder.AddColumn<string>(
                name: "PlayerInfoNickName",
                table: "PlayedGames",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayedGames_PlayerInfoNickName",
                table: "PlayedGames",
                column: "PlayerInfoNickName");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayedGames_PlayerInfo_PlayerInfoNickName",
                table: "PlayedGames",
                column: "PlayerInfoNickName",
                principalTable: "PlayerInfo",
                principalColumn: "NickName",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayedGames_PlayerInfo_PlayerInfoNickName",
                table: "PlayedGames");

            migrationBuilder.DropIndex(
                name: "IX_PlayedGames_PlayerInfoNickName",
                table: "PlayedGames");

            migrationBuilder.DropColumn(
                name: "PlayerInfoNickName",
                table: "PlayedGames");

            migrationBuilder.AddColumn<int>(
                name: "PlayedGamesId",
                table: "PlayerInfo",
                type: "int",
                nullable: true);

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
    }
}
