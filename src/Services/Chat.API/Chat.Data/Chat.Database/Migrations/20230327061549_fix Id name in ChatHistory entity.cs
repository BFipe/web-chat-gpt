using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Database.Migrations
{
    public partial class fixIdnameinChatHistoryentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatHistories",
                table: "ChatHistories");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ChatHistories",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChatName",
                table: "ChatHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatHistories",
                table: "ChatHistories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatHistories_GPTUserId",
                table: "ChatHistories",
                column: "GPTUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatHistories",
                table: "ChatHistories");

            migrationBuilder.DropIndex(
                name: "IX_ChatHistories_GPTUserId",
                table: "ChatHistories");

            migrationBuilder.DropColumn(
                name: "ChatName",
                table: "ChatHistories");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ChatHistories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatHistories",
                table: "ChatHistories",
                column: "GPTUserId");
        }
    }
}
