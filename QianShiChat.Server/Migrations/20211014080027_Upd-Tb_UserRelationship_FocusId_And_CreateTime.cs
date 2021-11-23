using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QianShiChat.Server.Migrations
{
    public partial class UpdTb_UserRelationship_FocusId_And_CreateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<sbyte>(
                name: "FocusType",
                table: "user_relationship",
                type: "tinyint",
                nullable: false,
                comment: "关注类型 1 用户 2 群组",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "关注类型 1 用户 2 群组");

            migrationBuilder.AlterColumn<long>(
                name: "FocusId",
                table: "user_relationship",
                type: "bigint",
                nullable: false,
                comment: "关注的编号",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "关注的编号");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "user_relationship",
                type: "datetime(6)",
                nullable: false,
                comment: "关注时间",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "关注时间");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FocusType",
                table: "user_relationship",
                type: "int",
                nullable: false,
                comment: "关注类型 1 用户 2 群组",
                oldClrType: typeof(sbyte),
                oldType: "tinyint",
                oldComment: "关注类型 1 用户 2 群组");

            migrationBuilder.AlterColumn<int>(
                name: "FocusId",
                table: "user_relationship",
                type: "int",
                nullable: false,
                comment: "关注的编号",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "关注的编号");

            migrationBuilder.AlterColumn<int>(
                name: "CreateTime",
                table: "user_relationship",
                type: "int",
                nullable: false,
                comment: "关注时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "关注时间");
        }
    }
}
