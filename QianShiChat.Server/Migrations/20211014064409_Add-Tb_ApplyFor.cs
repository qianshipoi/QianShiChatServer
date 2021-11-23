using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QianShiChat.Server.Migrations
{
    public partial class AddTb_ApplyFor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "apply_for",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "申请表主键")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false, comment: "申请用户"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "申请类型 1 用户 2 群组"),
                    ToUserId = table.Column<int>(type: "int", nullable: false, comment: "被申请用户"),
                    GroupId = table.Column<long>(type: "bigint", nullable: true, comment: "被申请群组"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "申请时间"),
                    Remark = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "申请备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "申请状态 1 已申请 2 已通过 3 已驳回 4 已忽略 "),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "最后修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apply_for", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_apply_for_GroupId",
                table: "apply_for",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_apply_for_ToUserId",
                table: "apply_for",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_apply_for_UserId",
                table: "apply_for",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apply_for");
        }
    }
}
