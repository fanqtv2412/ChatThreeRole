using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatThreeRole.Migrations
{
    public partial class AddNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_message_tbl_group_GroupId",
                table: "tbl_message");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "tbl_message",
                newName: "GroupID");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_message_GroupId",
                table: "tbl_message",
                newName: "IX_tbl_message_GroupID");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "tbl_message",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_message",
                table: "tbl_message",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_message_tbl_group_GroupID",
                table: "tbl_message",
                column: "GroupID",
                principalTable: "tbl_group",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_message_tbl_group_GroupID",
                table: "tbl_message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_message",
                table: "tbl_message");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "tbl_message");

            migrationBuilder.RenameColumn(
                name: "GroupID",
                table: "tbl_message",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_message_GroupID",
                table: "tbl_message",
                newName: "IX_tbl_message_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_message_tbl_group_GroupId",
                table: "tbl_message",
                column: "GroupId",
                principalTable: "tbl_group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
