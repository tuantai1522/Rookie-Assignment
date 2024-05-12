using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rookie.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initprodscatstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 12, 16, 7, 38, 865, DateTimeKind.Utc).AddTicks(9003)),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 12, 16, 7, 38, 865, DateTimeKind.Utc).AddTicks(9673))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 12, 16, 7, 38, 867, DateTimeKind.Utc).AddTicks(1139)),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 12, 16, 7, 38, 867, DateTimeKind.Utc).AddTicks(1682))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("2a70de53-a74e-4281-9c5f-960aa3d11d6c"), new DateTime(2024, 5, 12, 23, 7, 38, 866, DateTimeKind.Local).AddTicks(8736), "Accessories for women", "Accessories", new DateTime(2024, 5, 12, 23, 7, 38, 866, DateTimeKind.Local).AddTicks(8736) },
                    { new Guid("2bc12574-4d0a-4f88-8489-dd2f63ac41e5"), new DateTime(2024, 5, 12, 23, 7, 38, 866, DateTimeKind.Local).AddTicks(8697), "Shirts for men and women", "Shirts", new DateTime(2024, 5, 12, 23, 7, 38, 866, DateTimeKind.Local).AddTicks(8707) },
                    { new Guid("35803ab3-5c35-443e-b033-24ccb7aadf0f"), new DateTime(2024, 5, 12, 23, 7, 38, 866, DateTimeKind.Local).AddTicks(8716), "Pants for adults", "Pants", new DateTime(2024, 5, 12, 23, 7, 38, 866, DateTimeKind.Local).AddTicks(8717) },
                    { new Guid("71668136-768d-44f7-9941-5f3795c07442"), new DateTime(2024, 5, 12, 23, 7, 38, 866, DateTimeKind.Local).AddTicks(8732), "Shoes all sizes", "Shoes", new DateTime(2024, 5, 12, 23, 7, 38, 866, DateTimeKind.Local).AddTicks(8732) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
