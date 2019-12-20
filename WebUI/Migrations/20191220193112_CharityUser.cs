using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class CharityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CharityUserId",
                table: "Donations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_CharityUserId",
                table: "Donations",
                column: "CharityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_AspNetUsers_CharityUserId",
                table: "Donations",
                column: "CharityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_AspNetUsers_CharityUserId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_CharityUserId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "CharityUserId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");
        }
    }
}
