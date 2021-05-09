using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Data
{
    public class CustomerHotel
    {
        public int CustomerId { get; set; }
        public int HotelId { get; set; }
        public Customer Customer { get; set; }
        public Hotel Hotel { get; set; }

    }
}
