using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Models
{
    public class CreateHotelDTO
    {
        [Required]
        [StringLength(maximumLength: 70, ErrorMessage = "Country name is too long")]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        [Range(1,5)]
        public double Ratings { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
