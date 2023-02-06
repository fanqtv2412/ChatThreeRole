using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatThreeRole.Migrations
{
    public partial class NewKeyFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_message_tbl_group_groupid",
                table: "tbl_message");

            migrationBuilder.RenameColumn(
                name: "messsage",
                table: "tbl_message",
                newName: "Messsage");

            migrationBuilder.RenameColumn(
                name: "groupid",
                table: "tbl_message",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_message_groupid",
                table: "tbl_message",
                newName: "IX_tbl_message_GroupId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tbl_group",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_message_tbl_group_GroupId",
                table: "tbl_message",
                column: "GroupId",
                principalTable: "tbl_group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_message_tbl_group_GroupId",
                table: "tbl_message");

            migrationBuilder.RenameColumn(
                name: "Messsage",
                table: "tbl_message",
                newName: "messsage");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "tbl_message",
                newName: "groupid");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_message_GroupId",
                table: "tbl_message",
                newName: "IX_tbl_message_groupid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tbl_group",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_message_tbl_group_groupid",
                table: "tbl_message",
                column: "groupid",
                principalTable: "tbl_group",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
