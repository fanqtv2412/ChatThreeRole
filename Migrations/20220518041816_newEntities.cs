using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatThreeRole.Migrations
{
    public partial class newEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_group",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_message",
                columns: table => new
                {
                    messsage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    groupid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_tbl_message_tbl_group_groupid",
                        column: x => x.groupid,
                        principalTable: "tbl_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_message_groupid",
                table: "tbl_message",
                column: "groupid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_message");

            migrationBuilder.DropTable(
                name: "tbl_group");
        }
    }
}
