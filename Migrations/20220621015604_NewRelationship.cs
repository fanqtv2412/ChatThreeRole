using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatThreeRole.Migrations
{
    public partial class NewRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountModelGroupModel",
                columns: table => new
                {
                    AccountsEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountModelGroupModel", x => new { x.AccountsEmail, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_AccountModelGroupModel_tbl_account_AccountsEmail",
                        column: x => x.AccountsEmail,
                        principalTable: "tbl_account",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountModelGroupModel_tbl_group_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "tbl_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountModelGroupModel_GroupsId",
                table: "AccountModelGroupModel",
                column: "GroupsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountModelGroupModel");
        }
    }
}
