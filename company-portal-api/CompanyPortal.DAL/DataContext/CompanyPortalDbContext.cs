using CompanyPortal.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.DAL.DataContext
{
    public class CompanyPortalDbContext : DbContext
    {
        public CompanyPortalDbContext(DbContextOptions<CompanyPortalDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Sample Data for User
            modelBuilder.Entity<User>().HasData(
                 new User
                 {
                     Id = 1,
                     Username = "admin",
                     Password = "123",
                     Name = "Admin",
                     Surname = "Admin",
                 }
             );
            #endregion Sample Data for User

            #region Sample Data for Company
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "Apple Inc.",
                    Ticker = "AAPL",
                    Exchange = "NASDAQ",
                    Isin = "US0378331005",
                    Website = "http://www.apple.com",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 2,
                    Name = "British Airways Plc",
                    Ticker = "BAIRY",
                    Exchange = "Pink Sheets",
                    Isin = "US1104193065",
                    Website = null,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 3,
                    Name = "Heineken NV",
                    Ticker = "HEIA",
                    Exchange = "Euronext Amsterdam",
                    Isin = "NL0000009165",
                    Website = null,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 4,
                    Name = "Panasonic Corp",
                    Ticker = "6752",
                    Exchange = "Tokyo Stock Exchange",
                    Isin = "JP3866800000",
                    Website = "http://www.panasonic.co.jp",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 5,
                    Name = "Porsche Automobil",
                    Ticker = "PAH3",
                    Exchange = "Deutsche Börse",
                    Isin = "DE000PAH0038",
                    Website = "https://www.porsche.com/",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 6,
                    Name = "Microsoft Corporation",
                    Ticker = "MSFT",
                    Exchange = "NASDAQ",
                    Isin = "US5949181045",
                    Website = "http://www.microsoft.com",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 7,
                    Name = "Toyota Motor Corporation",
                    Ticker = "7203",
                    Exchange = "Tokyo Stock Exchange",
                    Isin = "JP3633400001",
                    Website = "http://www.toyota-global.com",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 8,
                    Name = "Samsung Electronics Co., Ltd.",
                    Ticker = "005930",
                    Exchange = "Korea Exchange",
                    Isin = "KR7005930003",
                    Website = "http://www.samsung.com",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 9,
                    Name = "Nestle S.A.",
                    Ticker = "NESN",
                    Exchange = "SIX Swiss Exchange",
                    Isin = "CH0038863350",
                    Website = "http://www.nestle.com",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 10,
                    Name = "Alphabet Inc.",
                    Ticker = "GOOGL",
                    Exchange = "NASDAQ",
                    Isin = "US02079K3059",
                    Website = "http://www.abc.xyz",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 11,
                    Name = "Amazon.com, Inc.",
                    Ticker = "AMZN",
                    Exchange = "NASDAQ",
                    Isin = "US0231351067",
                    Website = "http://www.amazon.com",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 12,
                    Name = "Tesla, Inc.",
                    Ticker = "TSLA",
                    Exchange = "NASDAQ",
                    Isin = "US88160R1014",
                    Website = "http://www.tesla.com",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 13,
                    Name = "Sony Corporation",
                    Ticker = "6758",
                    Exchange = "Tokyo Stock Exchange",
                    Isin = "JP3435000009",
                    Website = "http://www.sony.net",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 14,
                    Name = "Unilever PLC",
                    Ticker = "ULVR",
                    Exchange = "London Stock Exchange",
                    Isin = "GB00B10RZP78",
                    Website = "http://www.unilever.com",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 15,
                    Name = "Volkswagen AG",
                    Ticker = "VOW3",
                    Exchange = "Frankfurt Stock Exchange",
                    Isin = "DE0007664039",
                    Website = "http://www.volkswagenag.com",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 16,
                    Name = "Intel Corporation",
                    Ticker = "INTC",
                    Exchange = "NASDAQ",
                    Isin = "US4581401001",
                    Website = "http://www.intel.com",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Company
                {
                    Id = 17,
                    Name = "IBM Corporation",
                    Ticker = "IBM",
                    Exchange = "NYSE",
                    Isin = "US4592001014",
                    Website = "http://www.ibm.com",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
            );
            #endregion Sample Data for Company
        }
    }
}
