using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaSystem.Data.Migrations
{
    public partial class movieticketsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MovieTickets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MovieTickets");
        }
    }
}
