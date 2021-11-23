using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QianShiChat.Server.Migrations
{
    public partial class Addtb_ChatGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chat_group",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "群组表主键")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NickName = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "群昵称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    Avatar = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "群头像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "软删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_group", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_user_relationship_UserId",
                table: "user_relationship",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_chat_group_UserId",
                table: "chat_group",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chat_group");

            migrationBuilder.DropIndex(
                name: "IX_user_relationship_UserId",
                table: "user_relationship");
        }
    }
}
