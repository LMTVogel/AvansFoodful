using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EF.Migrations
{
    public partial class FirstSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Canteens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanteenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotMeals = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canteens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContainsAlcohol = table.Column<bool>(type: "bit", nullable: false),
                    ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentNr = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeNr = table.Column<int>(type: "int", nullable: false),
                    CanteenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanteenId = table.Column<int>(type: "int", nullable: false),
                    PickupDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxPickupTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContainsAlcohol = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "smallmoney", nullable: false),
                    MealType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Packages_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PackageProduct",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageProduct", x => new { x.ProductId, x.PackageId });
                    table.ForeignKey(
                        name: "FK_PackageProduct_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Canteens",
                columns: new[] { "Id", "CanteenName", "City", "HotMeals" },
                values: new object[,]
                {
                    { 1, "LA", "Breda", true },
                    { 2, "LD", "Breda", true },
                    { 3, "HA", "Breda", true },
                    { 4, "TB", "Tilburg", false },
                    { 5, "DB", "Den Bosch", false }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ContainsAlcohol", "Name", "ProductImage" },
                values: new object[,]
                {
                    { 1, false, "Banaan smoothie", "https://i.imgur.com/hJHKdzi.jpg" },
                    { 2, false, "Broodje bacon, ei, kaas", "https://i.imgur.com/rA8TaIL.jpg" },
                    { 3, false, "Broodje Unox", "https://i.imgur.com/BcdzaoG.jpg" },
                    { 4, false, "Cheeseburger", "https://i.imgur.com/YX5YYKB.jpg" },
                    { 5, false, "Plant based burger", "https://i.imgur.com/K1LfUCP.jpg" },
                    { 6, true, "Gluhwein", "https://i.imgur.com/T5rn9hV.jpg" },
                    { 7, false, "Banaan", "https://i.imgur.com/ccLWJP7.jpg" },
                    { 8, false, "Appel", "https://i.imgur.com/bMMCN0r.jpg" },
                    { 9, false, "Broodje gezond", "https://i.imgur.com/bUOh9Cc.jpg" },
                    { 10, false, "Tomatensoep", "https://i.imgur.com/CQKfs4O.jpg" },
                    { 11, false, "Kippensoep", "https://i.imgur.com/FOdxUth.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "City", "Email", "Name", "PhoneNumber", "StudentNr" },
                values: new object[,]
                {
                    { 1, new DateTime(2001, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Breda", "lmt.vogel@student.avans.nl", "Luuk Vogel", "+31640942653", 2181163 },
                    { 2, new DateTime(2001, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Breda", "rm.vandergaag@student.avans.nl", "Rogier van der Gaag", "+31612345678", 2181162 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CanteenId", "Email", "EmployeeNr", "Name" },
                values: new object[,]
                {
                    { 1, 1, "arend@avans.nl", 1000, "Arend Vliet" },
                    { 2, 2, "gerard@avans.nl", 2000, "Gerard Kok" },
                    { 3, 3, "thomas@avans.nl", 3000, "Thomas Trein" }
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CanteenId", "ContainsAlcohol", "MaxPickupTime", "MealType", "Name", "PickupDateTime", "Price", "StudentId" },
                values: new object[,]
                {
                    { 1, 1, false, new DateTime(2022, 12, 22, 15, 0, 0, 0, DateTimeKind.Unspecified), "Brood", "Gezond pakket", new DateTime(2022, 12, 22, 13, 0, 0, 0, DateTimeKind.Unspecified), 6m, null },
                    { 2, 2, false, new DateTime(2022, 12, 22, 15, 0, 0, 0, DateTimeKind.Unspecified), "Warme vondmaaltijd", "Lekker in de avond", new DateTime(2022, 12, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), 7m, null }
                });

            migrationBuilder.InsertData(
                table: "PackageProduct",
                columns: new[] { "PackageId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 3 },
                    { 2, 6 },
                    { 1, 7 },
                    { 1, 8 },
                    { 1, 9 },
                    { 2, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CanteenId",
                table: "Employees",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageProduct_PackageId",
                table: "PackageProduct",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_CanteenId",
                table: "Packages",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_StudentId",
                table: "Packages",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "PackageProduct");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Canteens");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
