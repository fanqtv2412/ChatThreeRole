using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatThreeRole.Migrations
{
    public partial class ImageOfGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageOfGroup",
                table: "tbl_group",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageOfGroup",
                table: "tbl_group");
        }
    }
}
