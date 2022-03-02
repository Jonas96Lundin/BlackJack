using Microsoft.EntityFrameworkCore.Migrations;

namespace BlackJackDAL.Migrations
{
    public partial class AddedTotalCurrencyHighscore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayedGames_PlayerInfo_PlayerInfoNickName",
                table: "PlayedGames");

            migrationBuilder.DropTable(
                name: "PlayerInfo");

            migrationBuilder.RenameColumn(
                name: "PlayerInfoNickName",
                table: "PlayedGames",
                newName: "PlayerNickName");

            migrationBuilder.RenameColumn(
                name: "PlayedGamesId",
                table: "PlayedGames",
                newName: "PlayedGameId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayedGames_PlayerInfoNickName",
                table: "PlayedGames",
                newName: "IX_PlayedGames_PlayerNickName");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    NickName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalCurrency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.NickName);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayedGames_Players_PlayerNickName",
                table: "PlayedGames",
                column: "PlayerNickName",
                principalTable: "Players",
                principalColumn: "NickName",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayedGames_Players_PlayerNickName",
                table: "PlayedGames");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.RenameColumn(
                name: "PlayerNickName",
                table: "PlayedGames",
                newName: "PlayerInfoNickName");

            migrationBuilder.RenameColumn(
                name: "PlayedGameId",
                table: "PlayedGames",
                newName: "PlayedGamesId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayedGames_PlayerNickName",
                table: "PlayedGames",
                newName: "IX_PlayedGames_PlayerInfoNickName");

            migrationBuilder.CreateTable(
                name: "PlayerInfo",
                columns: table => new
                {
                    NickName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerInfo", x => x.NickName);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayedGames_PlayerInfo_PlayerInfoNickName",
                table: "PlayedGames",
                column: "PlayerInfoNickName",
                principalTable: "PlayerInfo",
                principalColumn: "NickName",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
