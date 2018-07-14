using Microsoft.EntityFrameworkCore.Migrations;

namespace Meetup.Data.Migrations
{
    public partial class Add_User_Founder_To_Meetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupFounder",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupFounder",
                table: "Users",
                column: "GroupFounder",
                unique: true,
                filter: "[GroupFounder] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_GroupFounder",
                table: "Users",
                column: "GroupFounder",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_GroupFounder",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GroupFounder",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GroupFounder",
                table: "Users");
        }
    }
}
