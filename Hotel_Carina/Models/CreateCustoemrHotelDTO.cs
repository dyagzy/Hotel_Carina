using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Models
{
    public class CreateCustoemrHotelDTO
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int HotelId { get; set; }
    }
}
