using Microsoft.EntityFrameworkCore.Migrations;

namespace TourAgency.DAL.Migrations
{
    public partial class AddedPropertyTourOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Сurrency",
                table: "Tours",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TourName",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Сurrency",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TourName",
                table: "Orders");
        }
    }
}
