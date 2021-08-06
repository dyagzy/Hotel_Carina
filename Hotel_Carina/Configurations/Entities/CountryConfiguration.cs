using Hotel_Carina.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Configurations.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {

        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
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
        }
    }
}
