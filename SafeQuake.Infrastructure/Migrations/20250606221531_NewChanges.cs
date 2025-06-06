using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafeQuake.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EarthquakeEvents_Users_UserId",
                table: "EarthquakeEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EarthquakeEvents",
                table: "EarthquakeEvents");

            migrationBuilder.RenameTable(
                name: "EarthquakeEvents",
                newName: "Earthquakes");

            migrationBuilder.RenameIndex(
                name: "IX_EarthquakeEvents_UserId",
                table: "Earthquakes",
                newName: "IX_Earthquakes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Earthquakes",
                table: "Earthquakes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Earthquakes_Users_UserId",
                table: "Earthquakes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Earthquakes_Users_UserId",
                table: "Earthquakes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Earthquakes",
                table: "Earthquakes");

            migrationBuilder.RenameTable(
                name: "Earthquakes",
                newName: "EarthquakeEvents");

            migrationBuilder.RenameIndex(
                name: "IX_Earthquakes_UserId",
                table: "EarthquakeEvents",
                newName: "IX_EarthquakeEvents_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EarthquakeEvents",
                table: "EarthquakeEvents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EarthquakeEvents_Users_UserId",
                table: "EarthquakeEvents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
