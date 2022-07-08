using Microsoft.EntityFrameworkCore.Migrations;

namespace Slackiffy.Migrations
{
    public partial class addednewchatmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Messages_Slackiffy_Messages_MessageId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Messages_Slackiffy_Users_UserId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropIndex(
                name: "IX_Slackiffy_Messages_MessageId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropColumn(
                name: "ParentMessageId",
                table: "Slackiffy_Messages");

            migrationBuilder.RenameColumn(
                name: "RecieverId",
                table: "Slackiffy_Messages",
                newName: "ChatId");

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "Slackiffy_Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Slackiffy_Messages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slackiffy_Users_ChatId",
                table: "Slackiffy_Users",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Slackiffy_Messages_ChatId",
                table: "Slackiffy_Messages",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slackiffy_Messages_Chat_ChatId",
                table: "Slackiffy_Messages",
                column: "ChatId",
                principalTable: "Chat",
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
                name: "FK_Slackiffy_Users_Chat_ChatId",
                table: "Slackiffy_Users",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Messages_Chat_ChatId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Messages_Slackiffy_Users_UserId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Slackiffy_Users_Chat_ChatId",
                table: "Slackiffy_Users");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Slackiffy_Users_ChatId",
                table: "Slackiffy_Users");

            migrationBuilder.DropIndex(
                name: "IX_Slackiffy_Messages_ChatId",
                table: "Slackiffy_Messages");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Slackiffy_Users");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "Slackiffy_Messages",
                newName: "RecieverId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Slackiffy_Messages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "Slackiffy_Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentMessageId",
                table: "Slackiffy_Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Slackiffy_Messages_MessageId",
                table: "Slackiffy_Messages",
                column: "MessageId");

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
    }
}
