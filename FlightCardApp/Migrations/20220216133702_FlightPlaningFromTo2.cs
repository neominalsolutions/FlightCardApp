using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightCardApp.Migrations
{
    public partial class FlightPlaningFromTo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromCode",
                table: "FlightPlanings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToCode",
                table: "FlightPlanings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromCode",
                table: "FlightPlanings");

            migrationBuilder.DropColumn(
                name: "ToCode",
                table: "FlightPlanings");
        }
    }
}
