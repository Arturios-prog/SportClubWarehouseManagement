using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportClubWarehouseManagement.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedCustomersAndSportGoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    SubscribeStatus = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "SportGoods",
                columns: table => new
                {
                    SportGoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportGoods", x => x.SportGoodId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSportGoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SportGoodId = table.Column<int>(type: "int", nullable: false),
                    SportGoodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSportGoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSportGoods_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSportGood",
                columns: table => new
                {
                    CustomersCustomerId = table.Column<int>(type: "int", nullable: false),
                    SportGoodsSportGoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSportGood", x => new { x.CustomersCustomerId, x.SportGoodsSportGoodId });
                    table.ForeignKey(
                        name: "FK_CustomerSportGood_Customers_CustomersCustomerId",
                        column: x => x.CustomersCustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerSportGood_SportGoods_SportGoodsSportGoodId",
                        column: x => x.SportGoodsSportGoodId,
                        principalTable: "SportGoods",
                        principalColumn: "SportGoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Age", "Email", "FirstName", "Gender", "RegistrationDate", "SecondName", "SubscribeStatus" },
                values: new object[,]
                {
                    { 1, "St.Johnes St, 57", 24, "tom-jackson.@gmail.com", "Tom", 0, new DateTime(2023, 4, 18, 14, 59, 13, 240, DateTimeKind.Local).AddTicks(9618), "Jackson", 0 },
                    { 2, "TK-center, 5", 60, "meina-gladston.@gmail.com", "Meina", 1, new DateTime(2023, 4, 18, 14, 59, 13, 240, DateTimeKind.Local).AddTicks(9657), "Gladston", 1 },
                    { 3, "Jackson St., 24", 50, "John-batista.@gmail.com", "John", 0, new DateTime(2023, 4, 18, 14, 59, 13, 240, DateTimeKind.Local).AddTicks(9668), "Batista", 1 },
                    { 4, "Britain St., 77", 52, "Boris-Johnson.@gmail.com", "Boris", 0, new DateTime(2023, 4, 18, 14, 59, 13, 240, DateTimeKind.Local).AddTicks(9677), "Johnson", 0 },
                    { 5, "Ladozhskaya St., 15", 22, "Vlad-Kutepov.@gmail.com", "Vladislav", 0, new DateTime(2023, 4, 18, 14, 59, 13, 240, DateTimeKind.Local).AddTicks(9687), "Kutepov", 0 },
                    { 6, "Moskovskaya St., 15", 30, "Vlad-Kutepov.@gmail.com", "Evgeniy", 0, new DateTime(2023, 4, 18, 14, 59, 13, 240, DateTimeKind.Local).AddTicks(9699), "Bazhenov", 1 },
                    { 7, "Komsomolskaya St., 15", 40, "elena-kapustkina.@gmail.com", "Elena", 1, new DateTime(2023, 4, 18, 14, 59, 13, 240, DateTimeKind.Local).AddTicks(9708), "Kapustovna", 1 }
                });

            migrationBuilder.InsertData(
                table: "SportGoods",
                columns: new[] { "SportGoodId", "Category", "Description", "Name", "Quantity" },
                values: new object[,]
                {
                    { 1, 0, "A ball that is used in a football game", "Football ball", 20L },
                    { 2, 1, "A ball that is used in a basketball game", "Basketball ball", 15L },
                    { 3, 5, "A good that is used for a faster swimming", "Slippers", 25L },
                    { 4, 4, "It is used for punching a tennis ball", "Tennis crocket", 30L },
                    { 5, 4, "A ball that is used in a tennis game", "Tennis ball", 15L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSportGood_SportGoodsSportGoodId",
                table: "CustomerSportGood",
                column: "SportGoodsSportGoodId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSportGoods_CustomerId",
                table: "CustomerSportGoods",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerSportGood");

            migrationBuilder.DropTable(
                name: "CustomerSportGoods");

            migrationBuilder.DropTable(
                name: "SportGoods");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
