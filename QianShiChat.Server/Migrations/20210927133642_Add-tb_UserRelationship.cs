using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QianShiChat.Server.Migrations
{
    public partial class Addtb_UserRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_relationship",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "用户关系表主键")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false, comment: "用户"),
                    FocusId = table.Column<int>(type: "int", nullable: false, comment: "关注的编号"),
                    FocusType = table.Column<int>(type: "int", nullable: false, comment: "关注类型 1 用户 2 群组"),
                    CreateTime = table.Column<int>(type: "int", nullable: false, comment: "关注时间"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "软删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_relationship", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_relationship");
        }
    }
}
