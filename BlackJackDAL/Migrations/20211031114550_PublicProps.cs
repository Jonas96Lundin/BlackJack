using Microsoft.EntityFrameworkCore.Migrations;

namespace BlackJackDAL.Migrations
{
    public partial class PublicProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "PlayerInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurName",
                table: "PlayerInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PlayerInfo");

            migrationBuilder.DropColumn(
                name: "SurName",
                table: "PlayerInfo");
        }
    }
}
