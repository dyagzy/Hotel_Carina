using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Models
{
    public class UpdateCountryDTO  : CreateCountryDTO
    {
        public IList<CreateHotelDTO> Hotels { get; set; }
    }
}
