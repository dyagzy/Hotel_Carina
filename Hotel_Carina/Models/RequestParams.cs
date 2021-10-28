using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Models
{
    public class RequestParams
    {
        const int maxPageSize = 50;
        private int _pageSize = 10;
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get
            {
                return PageSize;
            }
            set
            {
                _pageSize =  value > maxPageSize ? maxPageSize : value;
            }
        }
           
           
     
       
    }
}
