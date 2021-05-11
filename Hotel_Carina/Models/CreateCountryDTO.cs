using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Models
{
    public class CreateCountryDTO
    {
        [Required]
        [StringLength(maximumLength: 70, ErrorMessage = "Country name is too long")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 70, ErrorMessage = "Short Country name is too long")]
        public string ShortName { get; set; }
    }
}
