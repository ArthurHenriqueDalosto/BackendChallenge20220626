using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChallangeData.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: true)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    code = table.Column<int>(type: "integer", nullable: true),
                    barcode = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: true),
                    importedt = table.Column<DateTime>(name: "imported_t", type: "timestamp with time zone", nullable: true),
                    url = table.Column<string>(type: "text", nullable: true),
                    productname = table.Column<string>(name: "product_name", type: "text", nullable: true),
                    quantity = table.Column<string>(type: "text", nullable: true),
                    categories = table.Column<string>(type: "text", nullable: true),
                    packaging = table.Column<string>(type: "text", nullable: true),
                    brands = table.Column<string>(type: "text", nullable: true),
                    imageurl = table.Column<string>(name: "image_url", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
