using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VehicleQuotes.Migrations
{
    public partial class AddQuoteRulesAndOverridesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quote_overides",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    model_style_year_id = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quote_overides", x => x.id);
                    table.ForeignKey(
                        name: "fk_quote_overides_model_style_years_model_style_year_id",
                        column: x => x.model_style_year_id,
                        principalTable: "model_style_years",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quote_rules",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    feature_type = table.Column<string>(type: "text", nullable: true),
                    feature_value = table.Column<string>(type: "text", nullable: true),
                    price_modifier = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quote_rules", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_quote_overides_model_style_year_id",
                table: "quote_overides",
                column: "model_style_year_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_quote_rules_feature_type_feature_value",
                table: "quote_rules",
                columns: new[] { "feature_type", "feature_value" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quote_overides");

            migrationBuilder.DropTable(
                name: "quote_rules");
        }
    }
}
