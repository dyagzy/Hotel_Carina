using AutoMapper;
using Hotel_Carina.Data;
using Hotel_Carina.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApiUser> userManager,
            SignInManager<ApiUser> signInManager
            , ILogger<AccountController> logger,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            _logger.LogInformation($"Reigstrtaion attempt for {userDto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<ApiUser>(userDto);
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest($"User registration attempt failed");
                }
                return Accepted();

            }
            catch (Exception)
            {
                _logger.LogInformation($"Something went wrong in the{nameof(Register)}");
                return Problem($"Something went wrong in the{nameof(Register)}", statusCode: 500);

            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            _logger.LogInformation($"Reigstrtaion attempt for {loginDto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Paswword, false, false);
              
                if (!result.Succeeded)
                {
                    return Unauthorized(loginDto);
                }
                return Accepted();

            }
            catch (Exception)
            {
                _logger.LogInformation($"Something went wrong in the{nameof(Login)}");
                return Problem($"Something went wrong in the{nameof(Login)}", statusCode: 500);

            }
        }
    }
}
