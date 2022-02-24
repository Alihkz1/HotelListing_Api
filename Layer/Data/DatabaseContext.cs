 using Microsoft.EntityFrameworkCore;

namespace HotelListing_webAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating (ModelBuilder builder)
        {

            builder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "UnitedStates",
                    ShortName= "US"

                },
                new Country
                {
                    Id = 2,
                    Name = "UnitedKingdom",
                    ShortName = "UK"
                },
                new Country
                {
                    Id = 3,
                    Name = "Norway",
                    ShortName = "NW"
                }
             );
            
            builder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "spinas",
                    Address = "LA/Beverly Hills",
                    CountryId = 1,
                    Rating = 4.5

                },
                new Hotel
                {
                    Id = 2,
                    Name = "five star",
                    Address = "london",
                    CountryId = 2,
                    Rating = 5
                },
                new Hotel
                {
                    Id = 3,
                    Name = "grand hotel",
                    Address = "capital",
                    CountryId =3,
                    Rating = 4
                }
             );
        }

    }
}
