using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPortal.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Exchange = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Isin = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                          .Annotation("SqlServer:Check", "LEN(Isin) = 12 AND Isin LIKE '[A-Z][A-Z]%'")
                          .Annotation("SqlServer:Unique", true),
                    Website = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.UniqueConstraint("AK_Companies_Isin", x => x.Isin);
                    table.CheckConstraint("CHK_ISIN", "LEN(Isin) = 12 AND Isin LIKE '[A-Z][A-Z]%'");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            var hashedPassword = HashPassword("123");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name", "Password", "Surname", "Username" },
                values: new object[] { 1, new DateTime(2024, 9, 30, 22, 29, 15, 552, DateTimeKind.Utc).AddTicks(873), new DateTime(2024, 9, 30, 22, 29, 15, 552, DateTimeKind.Utc).AddTicks(878), "Admin", hashedPassword, "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name", "Ticker", "Exchange", "Isin", "Website", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, "Apple Inc.", "AAPL", "NASDAQ", "US0378331005", "http://www.apple.com", DateTime.UtcNow, DateTime.UtcNow },
                    { 2, "British Airways Plc", "BAIRY", "Pink Sheets", "US1104193065", null, DateTime.UtcNow, DateTime.UtcNow },
                    { 3, "Heineken NV", "HEIA", "Euronext Amsterdam", "NL0000009165", null, DateTime.UtcNow, DateTime.UtcNow },
                    { 4, "Panasonic Corp", "6752", "Tokyo Stock Exchange", "JP3866800000", "http://www.panasonic.co.jp", DateTime.UtcNow, DateTime.UtcNow },
                    { 5, "Porsche Automobil", "PAH3", "Deutsche Börse", "DE000PAH0038", "https://www.porsche.com/", DateTime.UtcNow, DateTime.UtcNow },
                    { 6, "Microsoft Corporation", "MSFT", "NASDAQ", "US5949181045", "http://www.microsoft.com", DateTime.UtcNow, DateTime.UtcNow },
                    { 7, "Toyota Motor Corporation", "7203", "Tokyo Stock Exchange", "JP3633400001", "http://www.toyota-global.com", DateTime.UtcNow, DateTime.UtcNow },
                    { 8, "Samsung Electronics Co., Ltd.", "005930", "Korea Exchange", "KR7005930003", "http://www.samsung.com", DateTime.UtcNow, DateTime.UtcNow },
                    { 9, "Nestle S.A.", "NESN", "SIX Swiss Exchange", "CH0038863350", "http://www.nestle.com", DateTime.UtcNow, DateTime.UtcNow },
                    { 10, "Alphabet Inc.", "GOOGL", "NASDAQ", "US02079K3059", "http://www.abc.xyz", DateTime.UtcNow, DateTime.UtcNow },
                    { 11, "Amazon.com, Inc.", "AMZN", "NASDAQ", "US0231351067", "http://www.amazon.com", DateTime.UtcNow, DateTime.UtcNow },
                    { 12, "Tesla, Inc.", "TSLA", "NASDAQ", "US88160R1014", "http://www.tesla.com", DateTime.UtcNow, DateTime.UtcNow },
                    { 13, "Sony Corporation", "6758", "Tokyo Stock Exchange", "JP3435000009", "http://www.sony.net", DateTime.UtcNow, DateTime.UtcNow },
                    { 14, "Unilever PLC", "ULVR", "London Stock Exchange", "GB00B10RZP78", "http://www.unilever.com", DateTime.UtcNow, DateTime.UtcNow },
                    { 15, "Volkswagen AG", "VOW3", "Frankfurt Stock Exchange", "DE0007664039", "http://www.volkswagenag.com", DateTime.UtcNow, DateTime.UtcNow },
                    { 16, "Intel Corporation", "INTC", "NASDAQ", "US4581401001", "http://www.intel.com", DateTime.UtcNow, DateTime.UtcNow },
                    { 17, "IBM Corporation", "IBM", "NYSE", "US4592001014", "http://www.ibm.com", DateTime.UtcNow, DateTime.UtcNow }
                });
        }

        private string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            return passwordHasher.HashPassword(null, password);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
