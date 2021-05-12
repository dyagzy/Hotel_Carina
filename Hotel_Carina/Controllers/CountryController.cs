using AutoMapper;
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
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitofWork unitofWork, ILogger<CountryController> logger, IMapper mapper )
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries =  await _unitofWork.Countries.GetAll();
                IList<CountryDTO> results = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(results);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something went wrong {nameof(GetCountries)}");
                return StatusCode(500, "Internal server error, please try again later");
            }
        }

    }
}
