using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rookie.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixConversionStronglyTypedIdv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1c8db438-a94d-4b6e-8e5c-286d3d924b78"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3f5346cc-f801-4a10-afb4-315345ea4eec"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e41c00b-d0e3-43f1-b75c-f91ee95b54af"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f5947809-4f8e-4339-bb67-7ecb11aa31bd"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 56, 51, 598, DateTimeKind.Utc).AddTicks(2741),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 695, DateTimeKind.Utc).AddTicks(8660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 56, 51, 598, DateTimeKind.Utc).AddTicks(1654),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 695, DateTimeKind.Utc).AddTicks(6972));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 56, 51, 596, DateTimeKind.Utc).AddTicks(214),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 692, DateTimeKind.Utc).AddTicks(9579));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 56, 51, 595, DateTimeKind.Utc).AddTicks(9075),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 692, DateTimeKind.Utc).AddTicks(8089));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1cf7d4a0-2c4c-417b-a98e-14f0b8dec7b6"), "Accessories for women", "Accessories" },
                    { new Guid("a6274d63-8a81-4876-9ab8-65144be86dbb"), "Shirts for men and women", "Shirts" },
                    { new Guid("d61f3d42-07f0-47ce-977e-d48f5e9bacc9"), "Pants for adults", "Pants" },
                    { new Guid("fa8dfc3c-c071-4652-bcdd-70b669a64b28"), "Shoes all sizes", "Shoes" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1cf7d4a0-2c4c-417b-a98e-14f0b8dec7b6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a6274d63-8a81-4876-9ab8-65144be86dbb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d61f3d42-07f0-47ce-977e-d48f5e9bacc9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fa8dfc3c-c071-4652-bcdd-70b669a64b28"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 695, DateTimeKind.Utc).AddTicks(8660),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 56, 51, 598, DateTimeKind.Utc).AddTicks(2741));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 695, DateTimeKind.Utc).AddTicks(6972),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 56, 51, 598, DateTimeKind.Utc).AddTicks(1654));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 692, DateTimeKind.Utc).AddTicks(9579),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 56, 51, 596, DateTimeKind.Utc).AddTicks(214));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 692, DateTimeKind.Utc).AddTicks(8089),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 56, 51, 595, DateTimeKind.Utc).AddTicks(9075));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1c8db438-a94d-4b6e-8e5c-286d3d924b78"), "Shirts for men and women", "Shirts" },
                    { new Guid("3f5346cc-f801-4a10-afb4-315345ea4eec"), "Shoes all sizes", "Shoes" },
                    { new Guid("9e41c00b-d0e3-43f1-b75c-f91ee95b54af"), "Accessories for women", "Accessories" },
                    { new Guid("f5947809-4f8e-4339-bb67-7ecb11aa31bd"), "Pants for adults", "Pants" }
                });
        }
    }
}
