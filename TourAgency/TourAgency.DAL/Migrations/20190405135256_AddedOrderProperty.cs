using Microsoft.EntityFrameworkCore.Migrations;

namespace TourAgency.DAL.Migrations
{
    public partial class AddedOrderProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TourName",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TourName",
                table: "Orders");
        }
    }
}
