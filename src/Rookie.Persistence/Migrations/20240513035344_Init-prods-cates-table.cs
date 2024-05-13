using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rookie.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initprodscatestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2b3f90ea-c103-4b72-af8b-5d524f5b7321"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("82696366-5ec4-4d8e-b9c9-2c64078824a1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a4b693ea-3150-42b7-b743-752693a2e514"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b9cd9706-f211-4fa4-89f5-d534909b4872"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0af97edf-9adc-4d91-9505-6ccf2e1eba3f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0ed239ad-aaf1-4c7d-8080-7dfb76a07523"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1e316a3b-3f9f-440b-8da5-4e0b98ae53ab"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("25f03c3f-5489-47a5-b40a-b5ffb1ce751b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("27d34b95-35ef-4149-a730-41fd45c220f3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2fd9c307-a215-4ef5-ad5b-4675852b3113"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("318fe3a5-5990-4675-9115-8e65745470de"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33fd2f91-6e33-4ef5-9d42-c63211cc3bd2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("46cd20e7-4787-421f-bdf8-647c299bf2ee"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4b311eca-76af-4275-b27e-a68267552c03"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("84f0ecf3-0844-4391-a2f1-77da097bbf44"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8e893e98-adc5-4e55-804a-06742916246f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("95817ae6-7867-4449-b0a1-9ba8088349cd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9bf1091d-f680-4850-8372-717efc1760ad"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a9a02889-6e07-473a-b173-3cc7b95edec7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ae284c6f-bbd3-478e-b231-a43616b65926"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1490695-bd48-4bff-aaf1-730222b6c6c8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bd22d70e-bc0f-4cd4-a7d8-00ca24d78b59"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d641ce72-583c-492f-bd71-08491ff00006"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fbe2bd24-fd26-401c-86d1-126e2cd48d74"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 3, 53, 43, 984, DateTimeKind.Utc).AddTicks(3563),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 3, 52, 10, 177, DateTimeKind.Utc).AddTicks(2094));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 3, 53, 43, 984, DateTimeKind.Utc).AddTicks(2464),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 3, 52, 10, 177, DateTimeKind.Utc).AddTicks(1124));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 3, 53, 43, 982, DateTimeKind.Utc).AddTicks(479),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 3, 52, 10, 175, DateTimeKind.Utc).AddTicks(5097));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 3, 53, 43, 981, DateTimeKind.Utc).AddTicks(9782),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 3, 52, 10, 175, DateTimeKind.Utc).AddTicks(4240));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("05a25075-e930-4f41-a83b-d53b25e49f72"), "Pants for adults", "Pants" },
                    { new Guid("9b3928ca-90dc-4cd5-b960-9424ee066bf8"), "Shirts for men and women", "Shirts" },
                    { new Guid("aa72dcec-20a7-4494-8d2c-80ab41eff32b"), "Shoes all sizes", "Shoes" },
                    { new Guid("cd9a0845-f5f7-4f93-9914-04ad3d644776"), "Accessories for women", "Accessories" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("05a25075-e930-4f41-a83b-d53b25e49f72"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9b3928ca-90dc-4cd5-b960-9424ee066bf8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("aa72dcec-20a7-4494-8d2c-80ab41eff32b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cd9a0845-f5f7-4f93-9914-04ad3d644776"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 3, 52, 10, 177, DateTimeKind.Utc).AddTicks(2094),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 3, 53, 43, 984, DateTimeKind.Utc).AddTicks(3563));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 3, 52, 10, 177, DateTimeKind.Utc).AddTicks(1124),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 3, 53, 43, 984, DateTimeKind.Utc).AddTicks(2464));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 3, 52, 10, 175, DateTimeKind.Utc).AddTicks(5097),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 3, 53, 43, 982, DateTimeKind.Utc).AddTicks(479));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 3, 52, 10, 175, DateTimeKind.Utc).AddTicks(4240),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 3, 53, 43, 981, DateTimeKind.Utc).AddTicks(9782));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2b3f90ea-c103-4b72-af8b-5d524f5b7321"), "Shirts for men and women", "Shirts" },
                    { new Guid("82696366-5ec4-4d8e-b9c9-2c64078824a1"), "Pants for adults", "Pants" },
                    { new Guid("a4b693ea-3150-42b7-b743-752693a2e514"), "Shoes all sizes", "Shoes" },
                    { new Guid("b9cd9706-f211-4fa4-89f5-d534909b4872"), "Accessories for women", "Accessories" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Images", "Price", "ProductName" },
                values: new object[,]
                {
                    { new Guid("0af97edf-9adc-4d91-9505-6ccf2e1eba3f"), new Guid("00000000-0000-0000-0000-000000000000"), "Shirt for men", "https://img.fantaskycdn.com/cf56af93a6490ab8b6831b9271859224_750x.jpg", 49.90m, "Plaid cropped shirt" },
                    { new Guid("0ed239ad-aaf1-4c7d-8080-7dfb76a07523"), new Guid("00000000-0000-0000-0000-000000000000"), "Belt for women", "https://m.media-amazon.com/images/I/71AOYEcwVyL._AC_SX679_.jpg", 32.71m, "Eddie Bauer Women's Casual Fashion Leather Belt" },
                    { new Guid("1e316a3b-3f9f-440b-8da5-4e0b98ae53ab"), new Guid("00000000-0000-0000-0000-000000000000"), "Shirt for men and women", "https://img.fantaskycdn.com/cf56af93a6490ab8b6831b9271859224_750x.jpg", 49.90m, "Abstract print shirt" },
                    { new Guid("25f03c3f-5489-47a5-b40a-b5ffb1ce751b"), new Guid("00000000-0000-0000-0000-000000000000"), "Shirt for men", "https://img.fantaskycdn.com/cf56af93a6490ab8b6831b9271859224_750x.jpg", 59.90m, "Watercolor print shirt" },
                    { new Guid("27d34b95-35ef-4149-a730-41fd45c220f3"), new Guid("00000000-0000-0000-0000-000000000000"), "Shoes for men", "https://m.media-amazon.com/images/I/81kHSg8x6jL._AC_SX695_.jpg", 47.99m, "Skechers Men's Afterburn M. Fit" },
                    { new Guid("2fd9c307-a215-4ef5-ad5b-4675852b3113"), new Guid("00000000-0000-0000-0000-000000000000"), "Pants for men", "https://m.media-amazon.com/images/I/51LtnVXcodL._SX425_.jpg", 66.37m, "LAPCOFR unisex adult" },
                    { new Guid("318fe3a5-5990-4675-9115-8e65745470de"), new Guid("00000000-0000-0000-0000-000000000000"), "Earrings for kids", "https://m.media-amazon.com/images/I/71VCEYzU-pL._AC_SY741_.jpg", 26.99m, "Betsey Johnson Skull Earrings" },
                    { new Guid("33fd2f91-6e33-4ef5-9d42-c63211cc3bd2"), new Guid("00000000-0000-0000-0000-000000000000"), "Hat for women", "https://m.media-amazon.com/images/I/81FR3EO3-wL._AC_SX679_.jpg", 47.00m, "Carve Designs Women's Dundee Crushable" },
                    { new Guid("46cd20e7-4787-421f-bdf8-647c299bf2ee"), new Guid("00000000-0000-0000-0000-000000000000"), "Pants for men", "https://m.media-amazon.com/images/I/81HVw7Pzw9L._AC_SX679_.jpg", 8.70m, "Essentials Men's Classic-Fit Wrinkle-Resistant" },
                    { new Guid("4b311eca-76af-4275-b27e-a68267552c03"), new Guid("00000000-0000-0000-0000-000000000000"), "Shirt for kids", "https://img.fantaskycdn.com/cf56af93a6490ab8b6831b9271859224_750x.jpg", 49.90m, "Linen shirt" },
                    { new Guid("84f0ecf3-0844-4391-a2f1-77da097bbf44"), new Guid("00000000-0000-0000-0000-000000000000"), "Pants for men", "https://m.media-amazon.com/images/I/71wbSqIyuEL._AC_SY741_.jpg", 67.49m, "HUDSON Men's Blake Slim Straight" },
                    { new Guid("8e893e98-adc5-4e55-804a-06742916246f"), new Guid("00000000-0000-0000-0000-000000000000"), "Socks for kids", "https://m.media-amazon.com/images/I/81v0TUjL2kL._AC_SX679_.jpg", 8.95m, "K. Bell Women's Fun Sport" },
                    { new Guid("95817ae6-7867-4449-b0a1-9ba8088349cd"), new Guid("00000000-0000-0000-0000-000000000000"), "Sunglasses for women", "https://m.media-amazon.com/images/I/51AGz57VsjL._AC_SX679_.jpg", 11.95m, "French Connection Flex Sunglasses For Women" },
                    { new Guid("9bf1091d-f680-4850-8372-717efc1760ad"), new Guid("00000000-0000-0000-0000-000000000000"), "Shoes for men", "https://m.media-amazon.com/images/I/712jIRO8smL._AC_SY695_.jpg", 107.48m, "MOZO Men's Slip Resistant Chef Natural Shoes" },
                    { new Guid("a9a02889-6e07-473a-b173-3cc7b95edec7"), new Guid("00000000-0000-0000-0000-000000000000"), "Shoes for women", "https://m.media-amazon.com/images/I/61dM5wEQN1L._AC_SX695_.jpg", 22.80m, "Amazon Essentials Women's Loafer Flat" },
                    { new Guid("ae284c6f-bbd3-478e-b231-a43616b65926"), new Guid("00000000-0000-0000-0000-000000000000"), "Shoes for men", "https://m.media-amazon.com/images/I/61RHHzP07hL._AC_SY695_.jpg", 119.95m, "Merrell Men's Moab 3 Tactical Industrial Shoe" },
                    { new Guid("b1490695-bd48-4bff-aaf1-730222b6c6c8"), new Guid("00000000-0000-0000-0000-000000000000"), "Shirt for men", "https://img.fantaskycdn.com/cf56af93a6490ab8b6831b9271859224_750x.jpg", 69.90m, "Tie-dye print shirt" },
                    { new Guid("bd22d70e-bc0f-4cd4-a7d8-00ca24d78b59"), new Guid("00000000-0000-0000-0000-000000000000"), "Shoes for adults", "https://m.media-amazon.com/images/I/81TxPZimMaL._AC_SX679_.jpg", 67.12m, "Ringside Diablo Wrestling Boxing Shoes" },
                    { new Guid("d641ce72-583c-492f-bd71-08491ff00006"), new Guid("00000000-0000-0000-0000-000000000000"), "Pants for men", "https://m.media-amazon.com/images/I/71Z1Tina-LL._AC_SX679_.jpg", 30.48m, "Haggar Men's Cool 18" },
                    { new Guid("fbe2bd24-fd26-401c-86d1-126e2cd48d74"), new Guid("00000000-0000-0000-0000-000000000000"), "Pants for men", "https://m.media-amazon.com/images/I/51OvWUWbfvL._AC_SX679_.jpg", 22.49m, "Ergodyne Men's Standard Lightweight Base Layer" }
                });
        }
    }
}
