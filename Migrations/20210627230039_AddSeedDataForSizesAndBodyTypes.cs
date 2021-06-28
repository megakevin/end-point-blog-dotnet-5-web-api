using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleQuotes.Migrations
{
    public partial class AddSeedDataForSizesAndBodyTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "body_types",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Coupe" },
                    { 2, "Sedan" },
                    { 3, "Hatchback" },
                    { 4, "Wagon" },
                    { 5, "Convertible" },
                    { 6, "SUV" },
                    { 7, "Truck" },
                    { 8, "Mini Van" },
                    { 9, "Roadster" }
                });

            migrationBuilder.InsertData(
                table: "sizes",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Subcompact" },
                    { 2, "Compact" },
                    { 3, "Mid Size" },
                    { 5, "Full Size" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "body_types",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "body_types",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "body_types",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "body_types",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "body_types",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "body_types",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "body_types",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "body_types",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "body_types",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "sizes",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "sizes",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "sizes",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "sizes",
                keyColumn: "id",
                keyValue: 5);
        }
    }
}
