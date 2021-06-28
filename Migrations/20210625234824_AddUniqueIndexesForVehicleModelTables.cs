using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleQuotes.Migrations
{
    public partial class AddUniqueIndexesForVehicleModelTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_model_styles_model_id",
                table: "model_styles");

            migrationBuilder.CreateIndex(
                name: "ix_models_name_make_id",
                table: "models",
                columns: new[] { "name", "make_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_model_styles_model_id_body_type_id_size_id",
                table: "model_styles",
                columns: new[] { "model_id", "body_type_id", "size_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_model_style_years_year_model_style_id",
                table: "model_style_years",
                columns: new[] { "year", "model_style_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_models_name_make_id",
                table: "models");

            migrationBuilder.DropIndex(
                name: "ix_model_styles_model_id_body_type_id_size_id",
                table: "model_styles");

            migrationBuilder.DropIndex(
                name: "ix_model_style_years_year_model_style_id",
                table: "model_style_years");

            migrationBuilder.CreateIndex(
                name: "ix_model_styles_model_id",
                table: "model_styles",
                column: "model_id");
        }
    }
}
