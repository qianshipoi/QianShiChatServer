using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QianShiChat.Server.Migrations
{
    public partial class AddTb_DirectMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "directMessage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "私信表主键")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SenderId = table.Column<int>(type: "int", nullable: false, comment: "发送者"),
                    Content = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false, comment: "内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContentType = table.Column<int>(type: "int", nullable: false, comment: "内容类型 1 文字  2 图片"),
                    Receiver = table.Column<int>(type: "int", nullable: false, comment: "接收者"),
                    CreateTime = table.Column<int>(type: "int", nullable: false, comment: "发送时间"),
                    Read = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否已读"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_directMessage", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_directMessage_Receiver",
                table: "directMessage",
                column: "Receiver");

            migrationBuilder.CreateIndex(
                name: "IX_directMessage_SenderId",
                table: "directMessage",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "directMessage");
        }
    }
}
