using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VehicleQuotes.Migrations
{
    public partial class AddVehicleModelTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "models",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    make_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_models", x => x.id);
                    table.ForeignKey(
                        name: "fk_models_makes_make_id",
                        column: x => x.make_id,
                        principalTable: "makes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "model_styles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    model_id = table.Column<int>(type: "integer", nullable: false),
                    body_type_id = table.Column<int>(type: "integer", nullable: false),
                    size_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_model_styles", x => x.id);
                    table.ForeignKey(
                        name: "fk_model_styles_body_types_body_type_id",
                        column: x => x.body_type_id,
                        principalTable: "body_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_model_styles_models_model_id",
                        column: x => x.model_id,
                        principalTable: "models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_model_styles_sizes_size_id",
                        column: x => x.size_id,
                        principalTable: "sizes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "model_style_years",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    year = table.Column<string>(type: "text", nullable: true),
                    model_style_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_model_style_years", x => x.id);
                    table.ForeignKey(
                        name: "fk_model_style_years_model_styles_model_style_id",
                        column: x => x.model_style_id,
                        principalTable: "model_styles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_model_style_years_model_style_id",
                table: "model_style_years",
                column: "model_style_id");

            migrationBuilder.CreateIndex(
                name: "ix_model_styles_body_type_id",
                table: "model_styles",
                column: "body_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_model_styles_model_id",
                table: "model_styles",
                column: "model_id");

            migrationBuilder.CreateIndex(
                name: "ix_model_styles_size_id",
                table: "model_styles",
                column: "size_id");

            migrationBuilder.CreateIndex(
                name: "ix_models_make_id",
                table: "models",
                column: "make_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "model_style_years");

            migrationBuilder.DropTable(
                name: "model_styles");

            migrationBuilder.DropTable(
                name: "models");
        }
    }
}
