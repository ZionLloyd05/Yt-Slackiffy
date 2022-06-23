using Microsoft.EntityFrameworkCore.Migrations;

namespace Slackiffy.Migrations
{
    public partial class addedemail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Slackiffy_Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Slackiffy_Users");
        }
    }
}
