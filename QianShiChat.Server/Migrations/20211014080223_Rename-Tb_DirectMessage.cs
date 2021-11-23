using Microsoft.EntityFrameworkCore.Migrations;

namespace QianShiChat.Server.Migrations
{
    public partial class RenameTb_DirectMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_directMessage",
                table: "directMessage");

            migrationBuilder.RenameTable(
                name: "directMessage",
                newName: "direct_message");

            migrationBuilder.RenameIndex(
                name: "IX_directMessage_SenderId",
                table: "direct_message",
                newName: "IX_direct_message_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_directMessage_Receiver",
                table: "direct_message",
                newName: "IX_direct_message_Receiver");

            migrationBuilder.AddPrimaryKey(
                name: "PK_direct_message",
                table: "direct_message",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_direct_message",
                table: "direct_message");

            migrationBuilder.RenameTable(
                name: "direct_message",
                newName: "directMessage");

            migrationBuilder.RenameIndex(
                name: "IX_direct_message_SenderId",
                table: "directMessage",
                newName: "IX_directMessage_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_direct_message_Receiver",
                table: "directMessage",
                newName: "IX_directMessage_Receiver");

            migrationBuilder.AddPrimaryKey(
                name: "PK_directMessage",
                table: "directMessage",
                column: "Id");
        }
    }
}
