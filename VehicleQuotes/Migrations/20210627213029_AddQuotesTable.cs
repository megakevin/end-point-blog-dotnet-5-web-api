using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VehicleQuotes.Migrations
{
    public partial class AddQuotesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quotes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    model_style_year_id = table.Column<int>(type: "integer", nullable: true),
                    year = table.Column<string>(type: "text", nullable: true),
                    make = table.Column<string>(type: "text", nullable: true),
                    model = table.Column<string>(type: "text", nullable: true),
                    body_type_id = table.Column<int>(type: "integer", nullable: false),
                    size_id = table.Column<int>(type: "integer", nullable: false),
                    it_moves = table.Column<bool>(type: "boolean", nullable: false),
                    has_all_wheels = table.Column<bool>(type: "boolean", nullable: false),
                    has_alloy_wheels = table.Column<bool>(type: "boolean", nullable: false),
                    has_all_tires = table.Column<bool>(type: "boolean", nullable: false),
                    has_key = table.Column<bool>(type: "boolean", nullable: false),
                    has_title = table.Column<bool>(type: "boolean", nullable: false),
                    requires_pickup = table.Column<bool>(type: "boolean", nullable: false),
                    has_engine = table.Column<bool>(type: "boolean", nullable: false),
                    has_transmission = table.Column<bool>(type: "boolean", nullable: false),
                    has_complete_interior = table.Column<bool>(type: "boolean", nullable: false),
                    offered_quote = table.Column<int>(type: "integer", nullable: false),
                    message = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quotes", x => x.id);
                    table.ForeignKey(
                        name: "fk_quotes_body_types_body_type_id",
                        column: x => x.body_type_id,
                        principalTable: "body_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quotes_model_style_years_model_style_year_id",
                        column: x => x.model_style_year_id,
                        principalTable: "model_style_years",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_quotes_sizes_size_id",
                        column: x => x.size_id,
                        principalTable: "sizes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_quotes_body_type_id",
                table: "quotes",
                column: "body_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_quotes_model_style_year_id",
                table: "quotes",
                column: "model_style_year_id");

            migrationBuilder.CreateIndex(
                name: "ix_quotes_size_id",
                table: "quotes",
                column: "size_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quotes");
        }
    }
}
