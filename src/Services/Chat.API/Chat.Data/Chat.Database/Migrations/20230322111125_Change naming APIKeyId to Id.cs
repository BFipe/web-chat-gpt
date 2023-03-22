using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Database.Migrations
{
    public partial class ChangenamingAPIKeyIdtoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "APIKeyId",
                table: "APIKeys",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "APIKeys",
                newName: "APIKeyId");
        }
    }
}
