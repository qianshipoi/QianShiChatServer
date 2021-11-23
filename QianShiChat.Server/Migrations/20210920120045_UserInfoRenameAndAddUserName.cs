using Microsoft.EntityFrameworkCore.Migrations;

namespace QianShiChat.Server.Migrations
{
    public partial class UserInfoRenameAndAddUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos");

            migrationBuilder.RenameTable(
                name: "UserInfos",
                newName: "userinfo");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "userinfo",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                comment: "用户登录名")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userinfo",
                table: "userinfo",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userinfo",
                table: "userinfo");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "userinfo");

            migrationBuilder.RenameTable(
                name: "userinfo",
                newName: "UserInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos",
                column: "Id");
        }
    }
}
