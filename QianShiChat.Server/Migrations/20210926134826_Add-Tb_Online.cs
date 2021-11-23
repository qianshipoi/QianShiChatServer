using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QianShiChat.Server.Migrations
{
    public partial class AddTb_Online : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "online",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "在线表主键"),
                    UserId = table.Column<int>(type: "int", nullable: false, comment: "用户编号"),
                    ConnectionId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_online", x => x.Id);
                    table.ForeignKey(
                        name: "FK_online_userinfo_UserId",
                        column: x => x.UserId,
                        principalTable: "userinfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_online_UserId",
                table: "online",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "online");
        }
    }
}
