using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightCardApp.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airport_FromId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airport_ToId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Company_FlightCompanyId",
                table: "Flight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Airport",
                table: "Airport");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "Companies");

            migrationBuilder.RenameTable(
                name: "Airport",
                newName: "AirPorts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AirPorts",
                table: "AirPorts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_AirPorts_FromId",
                table: "Flight",
                column: "FromId",
                principalTable: "AirPorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_AirPorts_ToId",
                table: "Flight",
                column: "ToId",
                principalTable: "AirPorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Companies_FlightCompanyId",
                table: "Flight",
                column: "FlightCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_AirPorts_FromId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_AirPorts_ToId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Companies_FlightCompanyId",
                table: "Flight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AirPorts",
                table: "AirPorts");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Company");

            migrationBuilder.RenameTable(
                name: "AirPorts",
                newName: "Airport");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Airport",
                table: "Airport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Airport_FromId",
                table: "Flight",
                column: "FromId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Airport_ToId",
                table: "Flight",
                column: "ToId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Company_FlightCompanyId",
                table: "Flight",
                column: "FlightCompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
