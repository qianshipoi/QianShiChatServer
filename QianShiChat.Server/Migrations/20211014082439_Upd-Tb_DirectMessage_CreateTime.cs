using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QianShiChat.Server.Migrations
{
    public partial class UpdTb_DirectMessage_CreateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "direct_message",
                type: "datetime(6)",
                nullable: false,
                comment: "发送时间",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "发送时间");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CreateTime",
                table: "direct_message",
                type: "int",
                nullable: false,
                comment: "发送时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "发送时间");
        }
    }
}
