using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TourAgency.DAL.Migrations
{
    public partial class ChangedPictureEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Pictures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Pictures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Pictures");
        }
    }
}
