using Maps.src.Maps.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Maps.src.Maps.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Branch> Branches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed some initial data
            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    Id = 1,
                    Name = "Googleplex",
                    Address = "1600 Amphitheatre Parkway",
                    City = "Mountain View",
                    State = "CA",
                    PostalCode = "94043",
                    Country = "United States",
                    Latitude = 37.4220,
                    Longitude = -122.0841,
                    PhoneNumber = "+1 650-253-0000",
                    Email = "info@google.com",
                    OpeningDate = new TimeOnly(8, 0),
                    Duration = TimeSpan.FromHours(10)
                },
                new Branch
                {
                    Id = 2,
                    Name = "Google New York",
                    Address = "111 8th Avenue",
                    City = "New York",
                    State = "NY",
                    PostalCode = "10011",
                    Country = "United States",
                    Latitude = 40.7405,
                    Longitude = -74.0018,
                    PhoneNumber = "+1 212-565-0000",
                    Email = "nyc-office@google.com",
                    OpeningDate = new TimeOnly(9, 0),
                    Duration = TimeSpan.FromHours(9)
                },
                new Branch
                {
                    Id = 3,
                    Name = "Google London",
                    Address = "6 Pancras Square",
                    City = "London",
                    State = "",
                    PostalCode = "N1C 4AG",
                    Country = "United Kingdom",
                    Latitude = 51.5348,
                    Longitude = -0.1257,
                    PhoneNumber = "+44 20-7031-3000",
                    Email = "london-office@google.com",
                    OpeningDate = new TimeOnly(8, 30),
                    Duration = TimeSpan.FromHours(9, 30, 0)
                },
                new Branch
                {
                    Id = 4,
                    Name = "Google Tokyo",
                    Address = "Roppongi Hills Mori Tower, 6-10-1 Roppongi",
                    City = "Minato-ku",
                    State = "Tokyo",
                    PostalCode = "106-6126",
                    Country = "Japan",
                    Latitude = 35.6600,
                    Longitude = 139.7294,
                    PhoneNumber = "+81 3-6384-9000",
                    Email = "tokyo-office@google.com",
                    OpeningDate = new TimeOnly(9, 0),
                    Duration = TimeSpan.FromHours(8)
                },
                new Branch
                {
                    Id = 5,
                    Name = "Google Sydney",
                    Address = "48 Pirrama Road",
                    City = "Pyrmont",
                    State = "NSW",
                    PostalCode = "2009",
                    Country = "Australia",
                    Latitude = -33.8666,
                    Longitude = 151.1958,
                    PhoneNumber = "+61 2-9374-4000",
                    Email = "sydney-office@google.com",
                    OpeningDate = new TimeOnly(8, 30),
                    Duration = TimeSpan.FromHours(8, 30, 0)
                },
                new Branch
                {
                    Id = 6,
                    Name = "Google Berlin",
                    Address = "Friedrichstraße 68",
                    City = "Berlin",
                    State = "",
                    PostalCode = "10117",
                    Country = "Germany",
                    Latitude = 52.5206,
                    Longitude = 13.3861,
                    PhoneNumber = "+49 30-303986100",
                    Email = "berlin-office@google.com",
                    OpeningDate = new TimeOnly(8, 0),
                    Duration = TimeSpan.FromHours(9)
                },
                new Branch
                {
                    Id = 7,
                    Name = "Google Singapore",
                    Address = "70 Pasir Panjang Road, #03-71",
                    City = "Singapore",
                    State = "",
                    PostalCode = "117371",
                    Country = "Singapore",
                    Latitude = 1.2763,
                    Longitude = 103.7947,
                    PhoneNumber = "+65 6521-8000",
                    Email = "singapore-office@google.com",
                    OpeningDate = new TimeOnly(9, 0),
                    Duration = TimeSpan.FromHours(8)
                }
            );
        }
    }
}
