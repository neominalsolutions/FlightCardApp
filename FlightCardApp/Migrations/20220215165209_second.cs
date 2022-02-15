using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightCardApp.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromId",
                table: "Flight",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToId",
                table: "Flight",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Airport",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airport", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flight_FromId",
                table: "Flight",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_ToId",
                table: "Flight",
                column: "ToId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airport_FromId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airport_ToId",
                table: "Flight");

            migrationBuilder.DropTable(
                name: "Airport");

            migrationBuilder.DropIndex(
                name: "IX_Flight_FromId",
                table: "Flight");

            migrationBuilder.DropIndex(
                name: "IX_Flight_ToId",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "ToId",
                table: "Flight");
        }
    }
}
