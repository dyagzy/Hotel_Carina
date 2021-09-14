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


      
        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize]
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
        
        //[Authorize (Roles = "Administrator")]
        //[Authorize(Policy  = "Administrator")]

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatHotel([FromBody] CreateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Inavlid POST attempt in {nameof(CreatHotel)}");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = _mapper.Map<Hotel>(hotelDTO);
                await _unitofWork.Hotels.Insert(hotel);
                await _unitofWork.Save();

                 
                return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(CreatHotel)}");
                return StatusCode(500, "Internal Sserver Error. Please Try Again Later.");
                
            }
        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid || id  < 1)
            {
                _logger.LogError($"Inavlid UPDATE attaempt  in {nameof(UpdateHotel)}");
                return BadRequest(ModelState);

            }

            try
            {
                var hotel = await _unitofWork.Hotels.Get(x => x.Id == id);
                if (hotel == null)
                {
                    _logger.LogError($"Inavlid UPDATE attaempt  in {nameof(UpdateHotel)}");
                    return BadRequest(ModelState);
                }
                _mapper.Map(hotelDTO, hotel);
                 _unitofWork.Hotels.Update(hotel);
               await  _unitofWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something Went Wrong in the {nameof(UpdateHotel)}");
                return StatusCode(500, "Internal Sserver Error. Please Try Again Later."); ;
            }

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Inavlid DELETE attaempt  in {nameof(DeleteHotel)}");
                return BadRequest(ModelState);

            }

            try
            {
                var hotel = await _unitofWork.Hotels.Get(x => x.Id == id);
                if (hotel == null)
                {
                    _logger.LogError($"Inavlid UPDATE attaempt  in {nameof(DeleteHotel)}");
                    return BadRequest("Submitted Data is Invalid");
                }
               
                await _unitofWork.Hotels.Delete(id);
                await _unitofWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something Went Wrong in the {nameof(DeleteHotel)}");
                return StatusCode(500, "Internal Sserver Error. Please Try Again Later."); ;
            }

        }

    }
}
