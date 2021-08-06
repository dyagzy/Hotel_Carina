using AutoMapper;
using Hotel_Carina.Data;
using Hotel_Carina.IRepository;
using Hotel_Carina.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelController> _logger;

        public HotelController(IUnitofWork unitofWork, ILogger<HotelController> logger, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                var hotels =  await _unitofWork.Hotels.GetAll();
                IList<HotelDTO> hotel = _mapper.Map<IList<HotelDTO>>(hotels);
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong while retreiving data from the database{nameof(GetHotels)}");
                return StatusCode(500, "Internal server error, please try again later.......");
            }

        }


        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                var hotel = await _unitofWork.Hotels.Get(h => h.Id == id, new List<string> {"Country"});
                HotelDTO selected_hotel = _mapper.Map<HotelDTO>(hotel);
                return Ok(selected_hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong while retreiving data from the database{nameof(GetHotels)}");
                return StatusCode(500, "Internal server error, please try again later......");
            }

        }

    }
}
