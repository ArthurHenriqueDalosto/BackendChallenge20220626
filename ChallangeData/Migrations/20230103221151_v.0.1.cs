using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChallangeData.Migrations
{
    /// <inheritdoc />
    public partial class v01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    code = table.Column<long>(type: "bigint", nullable: false),
                    barcode = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    importedt = table.Column<DateTime>(name: "imported_t", type: "timestamp with time zone", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    productname = table.Column<string>(name: "product_name", type: "text", nullable: false),
                    quantity = table.Column<string>(type: "text", nullable: false),
                    categories = table.Column<string>(type: "text", nullable: false),
                    packaging = table.Column<string>(type: "text", nullable: false),
                    brands = table.Column<string>(type: "text", nullable: false),
                    imageurl = table.Column<string>(name: "image_url", type: "text", nullable: false)
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
