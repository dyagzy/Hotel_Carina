using Hotel_Carina.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Data
{
    public class DataBaseContext : IdentityDbContext<ApiUser>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Customer> Customers { get; set; }
        //public DbSet<CustomerHotel> CustomerHotels { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            //Role Seeding : 
            //adds the abstracted RoleConfigurated to the data conext for adequate seeding into the database

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new HotelConfiguration());

            //   Data seeding old implementation

            //Configure Compisite key for the many to many join table CustomerHotel

            //builder.Entity<CustomerHotel>()
            //    .HasKey(ch => new { ch.CustomerId, ch.HotelId });

            //// seeds data into the following tables

            //builder.Entity<Country>()
            //    .HasData(
            //    new Country
            //    {
            //        Id = 1,
            //        Name = "Nigeria",
            //        ShortName = "NGN"

            //    },
            //    new Country
            //    {
            //        Id = 2,
            //        Name = "Ghana",
            //        ShortName = "GHN"
            //    },

            //    new Country
            //    {
            //        Id = 3,
            //        Name = "Aberden",
            //        ShortName = "ABD"
            //    });


            //builder.Entity<Hotel>()
            //    .HasData(
            //    new Hotel
            //    {
            //        Id = 1,
            //        Price = 1503m,
            //        CountryId = 1,
            //        Ratings = 3.5,
            //        Address = "Ketu Lagos",
            //        Name = "Five Fouth by Sheraton"

            //    },
            //    new Hotel
            //    {
            //        Id = 2,
            //        Price = 432.10m,
            //        CountryId = 1,
            //        Ratings = 5.0,
            //        Address = "Lekki Lagos",
            //        Name = "Protea Hotel"
            //    },
            //    new Hotel
            //    {
            //        Id = 3,
            //        Price = 780.33m,
            //        CountryId = 2,
            //        Ratings = 4.5,
            //        Address = "Ogudu Lagos",
            //        Name = "Sheraton Hills and Towers"

            //    },
            //     new Hotel
            //     {
            //         Id = 4,
            //         Price = 580.13m,
            //         CountryId = 2,
            //         Ratings = 3.5,
            //         Address = "Abuja Qrt",
            //         Name = "Choice Gate Towers"

            //     },
            //new Hotel
            //{
            //    Id = 5,
            //    Price = 780.33m,
            //    CountryId = 3,
            //    Ratings = 4.5,
            //    Address = "Mandela Prims street Uganda ",
            //    Name = "New Horizon Towers"

            //},
            //new Hotel
            //{
            //    Id = 6,
            //    Price = 220.33m,
            //    CountryId = 3,
            //    Ratings = 1.5,
            //    Address = "Havilah Close Austria ",
            //    Name = "Susan Wesly Hotel"

            //},
            //new Hotel
            //{
            //    Id = 7,
            //    Price = 180.33m,
            //    CountryId = 2,
            //    Ratings = 4.5,
            //    Address = "Khinshasha Kenya street",
            //    Name = "Zanzibar Towers & Suits"

            //},
            //new Hotel
            //{
            //    Id = 8,
            //    Price = 80.33m,
            //    CountryId = 3,
            //    Ratings = 4.5,
            //    Address = "Jburg South Africa",
            //    Name = "BurgeKhalif Hotel & Suits"

            //});

            //builder.Entity<Customer>()
            //    .HasData(
            //    new Customer
            //    {
            //        Id = 1,
            //        Name = "King Judge",
            //        IsBooked = true,
            //        IsCanceled = false
            //    },
            //    new Customer

            //    {
            //        Id = 2,
            //        Name = "King Nothighame",
            //        IsBooked = true,
            //        IsCanceled = false
            //    },

            //    new Customer
            //    {
            //        Id = 3,
            //        Name = "Bob Neil",
            //        IsBooked = true,
            //        IsCanceled = true
            //    });



            //Many to many seeding wrong implememtation

            //builder.Entity<CustomerHotel>()
            //    .HasData(
            //    new CustomerHotel
            //    {
            //        HotelId = 1,
            //        CustomerId = 1
            //    },
            //     new CustomerHotel
            //     {
            //         HotelId = 2,
            //         CustomerId = 1
            //     },
            //      new CustomerHotel
            //      {
            //          HotelId = 3,
            //          CustomerId = 2
            //      });

        }


    }
}
