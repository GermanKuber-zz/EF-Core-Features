using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Meetup.Data.Migrations
{
    public partial class Add_Shadow_Property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Meetups",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Meetups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Meetups");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Meetups");
        }
    }
}
