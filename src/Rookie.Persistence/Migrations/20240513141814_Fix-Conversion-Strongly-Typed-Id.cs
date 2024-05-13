using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rookie.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixConversionStronglyTypedId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1f56e652-043a-4e4b-ab0d-71fcebaad524"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("40da6871-857b-409b-91f2-3ffc4908a5c7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("73262904-e345-45dd-aa61-0b5dfa4424c3"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e801fadf-6a0e-40d6-b0e8-2c631bd94cf4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0f9aeb05-208d-4b0b-9874-6861327ad3fd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1ff00f70-0acd-4327-a28e-b50d1a529246"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3f580c8c-fa3d-4bb5-a7d6-38f66c37c32e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4a835721-a305-4c6a-b772-372ff73cca4e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("59c7f393-86c8-4dc5-99c7-a7fcd4933561"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("63044c2d-8758-4b60-ad21-586bec28f105"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6341c1bd-c821-4a9f-9f5e-3f9bddbcf746"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("720b146e-a010-49ca-b642-53ef9ecf4373"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7d5a0655-5413-4e93-afad-aea1a931ca85"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7d877264-a4cb-42dc-b300-1abdf87e7760"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8214a1b2-8861-40f6-a2e6-853db1e2e79e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("90b16ace-af85-4637-bd69-8ea5dc44d7cf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("988e0b21-d96c-4046-ae43-f1cb8546655e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9ba4001b-ad1f-4c75-8e6a-cf35d796a469"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9e5149ba-b317-4ad4-8df4-931d12d12ac6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c425c625-2dea-4f24-b577-cb6d56b3ce24"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c6678cd9-f150-40c3-8d24-ccf37b5f0888"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("edce6d3f-d1f9-4478-8302-108402fcd23b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("efb2d770-0ff8-4f5e-8143-7968b6f6695a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fe8735b9-aa34-4dcb-9696-fff59886730d"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 695, DateTimeKind.Utc).AddTicks(8660),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 0, 58, 162, DateTimeKind.Utc).AddTicks(5241));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 695, DateTimeKind.Utc).AddTicks(6972),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 0, 58, 162, DateTimeKind.Utc).AddTicks(4322));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 692, DateTimeKind.Utc).AddTicks(9579),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 0, 58, 160, DateTimeKind.Utc).AddTicks(5336));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 692, DateTimeKind.Utc).AddTicks(8089),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 0, 58, 160, DateTimeKind.Utc).AddTicks(4462));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                defaultValue: new DateTime(2024, 5, 13, 14, 0, 58, 162, DateTimeKind.Utc).AddTicks(5241),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 695, DateTimeKind.Utc).AddTicks(8660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 0, 58, 162, DateTimeKind.Utc).AddTicks(4322),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 695, DateTimeKind.Utc).AddTicks(6972));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 0, 58, 160, DateTimeKind.Utc).AddTicks(5336),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 692, DateTimeKind.Utc).AddTicks(9579));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 14, 0, 58, 160, DateTimeKind.Utc).AddTicks(4462),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 14, 18, 13, 692, DateTimeKind.Utc).AddTicks(8089));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1f56e652-043a-4e4b-ab0d-71fcebaad524"), "Pants for adults", "Pants" },
                    { new Guid("40da6871-857b-409b-91f2-3ffc4908a5c7"), "Accessories for women", "Accessories" },
                    { new Guid("73262904-e345-45dd-aa61-0b5dfa4424c3"), "Shoes all sizes", "Shoes" },
                    { new Guid("e801fadf-6a0e-40d6-b0e8-2c631bd94cf4"), "Shirts for men and women", "Shirts" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Images", "Price", "ProductName" },
                values: new object[,]
                {
                    { new Guid("0f9aeb05-208d-4b0b-9874-6861327ad3fd"), new Guid("00000000-0000-0000-0000-000000000000"), "Belt for women", "https://m.media-amazon.com/images/I/71AOYEcwVyL._AC_SX679_.jpg", 32.71m, "Eddie Bauer Women's Casual Fashion Leather Belt" },
                    { new Guid("1ff00f70-0acd-4327-a28e-b50d1a529246"), new Guid("00000000-0000-0000-0000-000000000000"), "Shoes for men", "https://m.media-amazon.com/images/I/712jIRO8smL._AC_SY695_.jpg", 107.48m, "MOZO Men's Slip Resistant Chef Natural Shoes" },
                    { new Guid("3f580c8c-fa3d-4bb5-a7d6-38f66c37c32e"), new Guid("00000000-0000-0000-0000-000000000000"), "Pants for men", "https://m.media-amazon.com/images/I/71wbSqIyuEL._AC_SY741_.jpg", 67.49m, "HUDSON Men's Blake Slim Straight" },
                    { new Guid("4a835721-a305-4c6a-b772-372ff73cca4e"), new Guid("00000000-0000-0000-0000-000000000000"), "Socks for kids", "https://m.media-amazon.com/images/I/81v0TUjL2kL._AC_SX679_.jpg", 8.95m, "K. Bell Women's Fun Sport" },
                    { new Guid("59c7f393-86c8-4dc5-99c7-a7fcd4933561"), new Guid("00000000-0000-0000-0000-000000000000"), "Hat for women", "https://m.media-amazon.com/images/I/81FR3EO3-wL._AC_SX679_.jpg", 47.00m, "Carve Designs Women's Dundee Crushable" },
                    { new Guid("63044c2d-8758-4b60-ad21-586bec28f105"), new Guid("00000000-0000-0000-0000-000000000000"), "Pants for men", "https://m.media-amazon.com/images/I/51LtnVXcodL._SX425_.jpg", 66.37m, "LAPCOFR unisex adult" },
                    { new Guid("6341c1bd-c821-4a9f-9f5e-3f9bddbcf746"), new Guid("00000000-0000-0000-0000-000000000000"), "Pants for men", "https://m.media-amazon.com/images/I/71Z1Tina-LL._AC_SX679_.jpg", 30.48m, "Haggar Men's Cool 18" },
                    { new Guid("720b146e-a010-49ca-b642-53ef9ecf4373"), new Guid("00000000-0000-0000-0000-000000000000"), "Shirt for men and women", "https://img.fantaskycdn.com/cf56af93a6490ab8b6831b9271859224_750x.jpg", 49.90m, "Abstract print shirt" },
                    { new Guid("7d5a0655-5413-4e93-afad-aea1a931ca85"), new Guid("00000000-0000-0000-0000-000000000000"), "Shirt for kids", "https://img.fantaskycdn.com/cf56af93a6490ab8b6831b9271859224_750x.jpg", 49.90m, "Linen shirt" },
                    { new Guid("7d877264-a4cb-42dc-b300-1abdf87e7760"), new Guid("00000000-0000-0000-0000-000000000000"), "Shoes for adults", "https://m.media-amazon.com/images/I/81TxPZimMaL._AC_SX679_.jpg", 67.12m, "Ringside Diablo Wrestling Boxing Shoes" },
                    { new Guid("8214a1b2-8861-40f6-a2e6-853db1e2e79e"), new Guid("00000000-0000-0000-0000-000000000000"), "Pants for men", "https://m.media-amazon.com/images/I/51OvWUWbfvL._AC_SX679_.jpg", 22.49m, "Ergodyne Men's Standard Lightweight Base Layer" },
                    { new Guid("90b16ace-af85-4637-bd69-8ea5dc44d7cf"), new Guid("00000000-0000-0000-0000-000000000000"), "Pants for men", "https://m.media-amazon.com/images/I/81HVw7Pzw9L._AC_SX679_.jpg", 8.70m, "Essentials Men's Classic-Fit Wrinkle-Resistant" },
                    { new Guid("988e0b21-d96c-4046-ae43-f1cb8546655e"), new Guid("00000000-0000-0000-0000-000000000000"), "Shoes for men", "https://m.media-amazon.com/images/I/61RHHzP07hL._AC_SY695_.jpg", 119.95m, "Merrell Men's Moab 3 Tactical Industrial Shoe" },
                    { new Guid("9ba4001b-ad1f-4c75-8e6a-cf35d796a469"), new Guid("00000000-0000-0000-0000-000000000000"), "Shoes for women", "https://m.media-amazon.com/images/I/61dM5wEQN1L._AC_SX695_.jpg", 22.80m, "Amazon Essentials Women's Loafer Flat" },
                    { new Guid("9e5149ba-b317-4ad4-8df4-931d12d12ac6"), new Guid("00000000-0000-0000-0000-000000000000"), "Shoes for men", "https://m.media-amazon.com/images/I/81kHSg8x6jL._AC_SX695_.jpg", 47.99m, "Skechers Men's Afterburn M. Fit" },
                    { new Guid("c425c625-2dea-4f24-b577-cb6d56b3ce24"), new Guid("00000000-0000-0000-0000-000000000000"), "Shirt for men", "https://img.fantaskycdn.com/cf56af93a6490ab8b6831b9271859224_750x.jpg", 69.90m, "Tie-dye print shirt" },
                    { new Guid("c6678cd9-f150-40c3-8d24-ccf37b5f0888"), new Guid("00000000-0000-0000-0000-000000000000"), "Earrings for kids", "https://m.media-amazon.com/images/I/71VCEYzU-pL._AC_SY741_.jpg", 26.99m, "Betsey Johnson Skull Earrings" },
                    { new Guid("edce6d3f-d1f9-4478-8302-108402fcd23b"), new Guid("00000000-0000-0000-0000-000000000000"), "Shirt for men", "https://img.fantaskycdn.com/cf56af93a6490ab8b6831b9271859224_750x.jpg", 49.90m, "Plaid cropped shirt" },
                    { new Guid("efb2d770-0ff8-4f5e-8143-7968b6f6695a"), new Guid("00000000-0000-0000-0000-000000000000"), "Sunglasses for women", "https://m.media-amazon.com/images/I/51AGz57VsjL._AC_SX679_.jpg", 11.95m, "French Connection Flex Sunglasses For Women" },
                    { new Guid("fe8735b9-aa34-4dcb-9696-fff59886730d"), new Guid("00000000-0000-0000-0000-000000000000"), "Shirt for men", "https://img.fantaskycdn.com/cf56af93a6490ab8b6831b9271859224_750x.jpg", 59.90m, "Watercolor print shirt" }
                });
        }
    }
}
