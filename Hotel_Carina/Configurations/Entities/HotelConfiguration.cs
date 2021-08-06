using Hotel_Carina.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
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
                    CountryId = 2,
                    Ratings = 4.5,
                    Address = "Ogudu Lagos",
                    Name = "Sheraton Hills and Towers"

                },
                 new Hotel
                 {
                     Id = 4,
                     Price = 580.13m,
                     CountryId = 2,
                     Ratings = 3.5,
                     Address = "Abuja Qrt",
                     Name = "Choice Gate Towers"

                 },
            new Hotel
            {
                Id = 5,
                Price = 780.33m,
                CountryId = 3,
                Ratings = 4.5,
                Address = "Mandela Prims street Uganda ",
                Name = "New Horizon Towers"

            },
            new Hotel
            {
                Id = 6,
                Price = 220.33m,
                CountryId = 3,
                Ratings = 1.5,
                Address = "Havilah Close Austria ",
                Name = "Susan Wesly Hotel"

            },
            new Hotel
            {
                Id = 7,
                Price = 180.33m,
                CountryId = 2,
                Ratings = 4.5,
                Address = "Khinshasha Kenya street",
                Name = "Zanzibar Towers & Suits"

            },
            new Hotel
            {
                Id = 8,
                Price = 80.33m,
                CountryId = 3,
                Ratings = 4.5,
                Address = "Jburg South Africa",
                Name = "BurgeKhalif Hotel & Suits"

            });
        }
    }
}
