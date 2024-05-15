using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rookie.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Clonedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1f76e0b5-214f-4744-a592-e02aaa188494"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("55201118-a7dd-45c2-906b-e8515ebfc494"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("dcebd3e6-0baa-4da2-9374-d08e3d421f09"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ed4b2b06-ee12-44f3-bfc9-54096597c2e9"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 14, 14, 6, 7, 51, DateTimeKind.Utc).AddTicks(6715),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 14, 14, 4, 13, 490, DateTimeKind.Utc).AddTicks(8357));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 14, 14, 6, 7, 51, DateTimeKind.Utc).AddTicks(6001),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 14, 14, 4, 13, 490, DateTimeKind.Utc).AddTicks(7611));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 14, 14, 6, 7, 50, DateTimeKind.Utc).AddTicks(747),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 14, 14, 4, 13, 489, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 14, 14, 6, 7, 50, DateTimeKind.Utc).AddTicks(80),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 14, 14, 4, 13, 489, DateTimeKind.Utc).AddTicks(779));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("52f8ee8f-ba82-4834-9676-65fea71e94d2"), "Shirts for men and women", "Shirts" },
                    { new Guid("5c2411bf-6003-49e7-a06e-12a5bc3f17f6"), "Shoes all sizes", "Shoes" },
                    { new Guid("688b0418-36a2-42ca-aec0-e20a0c18cea2"), "Accessories for women", "Accessories" },
                    { new Guid("72371cfb-e97b-4af0-ab6c-9753ff3a7e63"), "Pants for adults", "Pants" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("52f8ee8f-ba82-4834-9676-65fea71e94d2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5c2411bf-6003-49e7-a06e-12a5bc3f17f6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("688b0418-36a2-42ca-aec0-e20a0c18cea2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("72371cfb-e97b-4af0-ab6c-9753ff3a7e63"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 14, 14, 4, 13, 490, DateTimeKind.Utc).AddTicks(8357),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 14, 14, 6, 7, 51, DateTimeKind.Utc).AddTicks(6715));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 14, 14, 4, 13, 490, DateTimeKind.Utc).AddTicks(7611),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 14, 14, 6, 7, 51, DateTimeKind.Utc).AddTicks(6001));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 14, 14, 4, 13, 489, DateTimeKind.Utc).AddTicks(1554),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 14, 14, 6, 7, 50, DateTimeKind.Utc).AddTicks(747));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 14, 14, 4, 13, 489, DateTimeKind.Utc).AddTicks(779),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 14, 14, 6, 7, 50, DateTimeKind.Utc).AddTicks(80));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1f76e0b5-214f-4744-a592-e02aaa188494"), "Shirts for men and women", "Shirts" },
                    { new Guid("55201118-a7dd-45c2-906b-e8515ebfc494"), "Pants for adults", "Pants" },
                    { new Guid("dcebd3e6-0baa-4da2-9374-d08e3d421f09"), "Accessories for women", "Accessories" },
                    { new Guid("ed4b2b06-ee12-44f3-bfc9-54096597c2e9"), "Shoes all sizes", "Shoes" }
                });
        }
    }
}
