using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Data
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public double Ratings { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public IEnumerable<CustomerHotel> CustomerHotels { get; set; }
       //public IEnumerable<CustomerHotel> Customers { get; set; }
    }
}
