using Microsoft.EntityFrameworkCore.Migrations;

namespace Slackiffy.Migrations
{
    public partial class addvirtual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Messages_Chat_ChatId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Users_Chat_ChatId",
                table: "Slackiffy_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat",
                table: "Chat");

            migrationBuilder.RenameTable(
                name: "Chat",
                newName: "Chats");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slackiffy_Messages_Chats_ChatId",
                table: "Slackiffy_Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Slackiffy_Users_Chats_ChatId",
                table: "Slackiffy_Users",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Messages_Chats_ChatId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Users_Chats_ChatId",
                table: "Slackiffy_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.RenameTable(
                name: "Chats",
                newName: "Chat");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat",
                table: "Chat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slackiffy_Messages_Chat_ChatId",
                table: "Slackiffy_Messages",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Slackiffy_Users_Chat_ChatId",
                table: "Slackiffy_Users",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
