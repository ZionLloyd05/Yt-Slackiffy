using Microsoft.EntityFrameworkCore.Migrations;

namespace Slackiffy.Migrations
{
    public partial class userMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecieverId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecieverId",
                table: "Messages");
        }
    }
}
