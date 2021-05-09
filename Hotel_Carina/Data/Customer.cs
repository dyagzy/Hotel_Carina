using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsBooked { get; set; }
        public bool IsCanceled { get; set; }


        public IEnumerable<Hotel>   Hotels  { get; set; }
    }
}
