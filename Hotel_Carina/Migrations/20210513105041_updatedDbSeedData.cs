using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel_Carina.Migrations
{
    public partial class updatedDbSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ratings = table.Column<double>(type: "float", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerHotels",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerHotels", x => new { x.CustomerId, x.HotelId });
                    table.ForeignKey(
                        name: "FK_CustomerHotels_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerHotels_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Nigeria", "NGN" },
                    { 2, "Ghana", "GHN" },
                    { 3, "Aberden", "ABD" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "IsBooked", "IsCanceled", "Name" },
                values: new object[,]
                {
                    { 1, true, false, "King Judge" },
                    { 2, true, false, "King Nothighame" },
                    { 3, true, true, "Bob Neil" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "CountryId", "Name", "Price", "Ratings" },
                values: new object[,]
                {
                    { 1, "Ketu Lagos", 1, "Five Fouth by Sheraton", 1503m, 3.5 },
                    { 2, "Lekki Lagos", 1, "Protea Hotel", 432.10m, 5.0 },
                    { 3, "Ogudu Lagos", 2, "Sheraton Hills and Towers", 780.33m, 4.5 },
                    { 4, "Abuja Qrt", 2, "Choice Gate Towers", 580.13m, 3.5 },
                    { 7, "Khinshasha Kenya street", 2, "Zanzibar Towers & Suits", 180.33m, 4.5 },
                    { 5, "Mandela Prims street Uganda ", 3, "New Horizon Towers", 780.33m, 4.5 },
                    { 6, "Havilah Close Austria ", 3, "Susan Wesly Hotel", 220.33m, 1.5 },
                    { 8, "Jburg South Africa", 3, "BurgeKhalif Hotel & Suits", 80.33m, 4.5 }
                });

            migrationBuilder.InsertData(
                table: "CustomerHotels",
                columns: new[] { "CustomerId", "HotelId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "CustomerHotels",
                columns: new[] { "CustomerId", "HotelId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "CustomerHotels",
                columns: new[] { "CustomerId", "HotelId" },
                values: new object[] { 2, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerHotels_HotelId",
                table: "CustomerHotels",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CountryId",
                table: "Hotels",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerHotels");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
