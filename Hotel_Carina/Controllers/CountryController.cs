using AutoMapper;
using Hotel_Carina.Data;
using Hotel_Carina.IRepository;
using Hotel_Carina.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitofWork unitofWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("Countries/Paginated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {
            try
            {
                var countries = await _unitofWork.Countries.GetPagedList(requestParams);
                IList<CountryDTO> results = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong {nameof(GetCountries)}");
                return StatusCode(500, "Internal server error, please try again later");
            }
        }

        [HttpGet("Countries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _unitofWork.Countries.GetAll();
                IList<CountryDTO> results = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong {nameof(GetCountries)}");
                return StatusCode(500, "Internal server error, please try again later");
            }
        }

        [HttpGet("Country/{id:int}", Name ="GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _unitofWork.Countries.Get(q => q.Id == id, new List<string> { "Hotels"});
               CountryDTO result = _mapper.Map<CountryDTO>(country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong {nameof(GetCountry)}");
                return StatusCode(500, "Internal server error, please try again later");
            }
        }


        [HttpPost("CreateCountry")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatCountry([FromBody] CreateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Inavlid POST attempt in {nameof(CreatCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var country = _mapper.Map<Country>(countryDTO);
                await _unitofWork.Countries.Insert(country);
                await _unitofWork.Save();


                return CreatedAtAction("GetCountry", new { id = country.Id }, country);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(CreatCountry)}");
                return StatusCode(500, "Internal Sserver Error. Please Try Again Later.");

            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Inavlid UPDATE attaempt  in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);

            }

            try
            {
                var country = await _unitofWork.Countries.Get(x => x.Id == id);
                if (country == null)
                {
                    _logger.LogError($"Inavlid UPDATE attaempt  in {nameof(UpdateCountry)}");
                    return BadRequest(ModelState);
                }
                _mapper.Map(countryDTO, country);
                _unitofWork.Countries.Update(country);
                await _unitofWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something Went Wrong in the {nameof(UpdateCountry)}");
                return StatusCode(500, "Internal Sserver Error. Please Try Again Later."); ;
            }

        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteContry(int id)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Inavlid DELETE attaempt  in {nameof(DeleteContry)}");
                return BadRequest(ModelState);

            }

            try
            {
                var hotel = await _unitofWork.Hotels.Get(x => x.Id == id);
                if (hotel == null)
                {
                    _logger.LogError($"Inavlid UPDATE attaempt  in {nameof(DeleteContry)}");
                    return BadRequest("Submitted Data is Invalid");
                }

                await _unitofWork.Countries.Delete(id);
                await _unitofWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something Went Wrong in the {nameof(DeleteContry)}");
                return StatusCode(500, "Internal Sserver Error. Please Try Again Later."); ;
            }

        }

    }
}
