using Microsoft.EntityFrameworkCore.Migrations;

namespace Slackiffy.Migrations
{
    public partial class renamedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_MessageId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Slackiffy_Users");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Slackiffy_Messages");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_UserId",
                table: "Slackiffy_Messages",
                newName: "IX_Slackiffy_Messages_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_MessageId",
                table: "Slackiffy_Messages",
                newName: "IX_Slackiffy_Messages_MessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slackiffy_Users",
                table: "Slackiffy_Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slackiffy_Messages",
                table: "Slackiffy_Messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slackiffy_Messages_Slackiffy_Messages_MessageId",
                table: "Slackiffy_Messages",
                column: "MessageId",
                principalTable: "Slackiffy_Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Slackiffy_Messages_Slackiffy_Users_UserId",
                table: "Slackiffy_Messages",
                column: "UserId",
                principalTable: "Slackiffy_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Messages_Slackiffy_Messages_MessageId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Messages_Slackiffy_Users_UserId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slackiffy_Users",
                table: "Slackiffy_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slackiffy_Messages",
                table: "Slackiffy_Messages");

            migrationBuilder.RenameTable(
                name: "Slackiffy_Users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Slackiffy_Messages",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_Slackiffy_Messages_UserId",
                table: "Messages",
                newName: "IX_Messages_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Slackiffy_Messages_MessageId",
                table: "Messages",
                newName: "IX_Messages_MessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_MessageId",
                table: "Messages",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
