using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerHotel> CustomerHotels { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Configure Compisite key for the many to many join table CustomerHotel


            builder.Entity<CustomerHotel>()
                .HasKey(ch => new { ch.CustomerId, ch.HotelId });


            // seeds data inot the following tables

            builder.Entity<Country>()
                .HasData(
                new Country
                {
                    Id = 1,
                    Name = "Nigeria",
                    ShortName = "NGN"

                },
                new Country
                {
                    Id = 2,
                    Name = "Ghana",
                    ShortName = "GHN"
                },

                new Country
                {
                    Id = 3,
                    Name = "Aberden",
                    ShortName = "ABD"
                });


            builder.Entity<Hotel>()
                .HasData(
                new Hotel
                {
                    Id = 1,
                    Price = 1503m,
                    CountryId = 1,
                    Ratings = 3.5,
                    Address = "Ketu Lagos",
                    Name = "Five Fouth by Sheraton"

                },
                new Hotel
                {
                    Id = 2,
                    Price = 432.10m,
                    CountryId = 1,
                    Ratings = 5.0,
                    Address = "Lekki Lagos",
                    Name = "Protea Hotel"
                },

                new Hotel
                {
                    Id = 3,
                    Price = 780.33m,
                    CountryId = 1,
                    Ratings = 4.5,
                    Address = "Ogudu Lagos",
                    Name = "Sheraton Hills and Towers"

                });

            builder.Entity<Customer>()
                .HasData(
                new Customer
                {
                    Id = 1,
                    Name ="King Judge",
                    IsBooked = true,
                    IsCanceled = false

                },
                new Customer

                {
                    Id = 2,
                    Name = "King Nothighame",
                    IsBooked = true,
                    IsCanceled = false
                },

                new Customer
                {
                    Id = 3,
                    Name = "Bob Neil",
                    IsBooked = true,
                    IsCanceled = true

                });
            

        }
       

    }
}
