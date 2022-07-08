using Microsoft.EntityFrameworkCore.Migrations;

namespace Slackiffy.Migrations
{
    public partial class modelrevamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Messages_Chats_ChatId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Messages_Slackiffy_Users_UserId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Users_Chats_ChatId",
                table: "Slackiffy_Users");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Slackiffy_Users_ChatId",
                table: "Slackiffy_Users");

            migrationBuilder.DropIndex(
                name: "IX_Slackiffy_Messages_UserId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Slackiffy_Users");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Slackiffy_Messages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Slackiffy_Messages");

            migrationBuilder.RenameColumn(
                name: "When",
                table: "Slackiffy_Messages",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Slackiffy_Messages",
                newName: "Chat");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "Slackiffy_Messages",
                newName: "ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Slackiffy_Messages_ChatId",
                table: "Slackiffy_Messages",
                newName: "IX_Slackiffy_Messages_ToUserId");

            migrationBuilder.AddColumn<int>(
                name: "FromUserId",
                table: "Slackiffy_Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Slackiffy_Messages_FromUserId",
                table: "Slackiffy_Messages",
                column: "FromUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slackiffy_Messages_Slackiffy_Users_FromUserId",
                table: "Slackiffy_Messages",
                column: "FromUserId",
                principalTable: "Slackiffy_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Slackiffy_Messages_Slackiffy_Users_ToUserId",
                table: "Slackiffy_Messages",
                column: "ToUserId",
                principalTable: "Slackiffy_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Messages_Slackiffy_Users_FromUserId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Messages_Slackiffy_Users_ToUserId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropIndex(
                name: "IX_Slackiffy_Messages_FromUserId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropColumn(
                name: "FromUserId",
                table: "Slackiffy_Messages");

            migrationBuilder.RenameColumn(
                name: "ToUserId",
                table: "Slackiffy_Messages",
                newName: "ChatId");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Slackiffy_Messages",
                newName: "When");

            migrationBuilder.RenameColumn(
                name: "Chat",
                table: "Slackiffy_Messages",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_Slackiffy_Messages_ToUserId",
                table: "Slackiffy_Messages",
                newName: "IX_Slackiffy_Messages_ChatId");

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "Slackiffy_Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Slackiffy_Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Slackiffy_Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slackiffy_Users_ChatId",
                table: "Slackiffy_Users",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Slackiffy_Messages_UserId",
                table: "Slackiffy_Messages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slackiffy_Messages_Chats_ChatId",
                table: "Slackiffy_Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Slackiffy_Messages_Slackiffy_Users_UserId",
                table: "Slackiffy_Messages",
                column: "UserId",
                principalTable: "Slackiffy_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Slackiffy_Users_Chats_ChatId",
                table: "Slackiffy_Users",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
