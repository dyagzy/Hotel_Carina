using AutoMapper;
using Hotel_Carina.Data;
using Hotel_Carina.Models;
using Hotel_Carina.Services;
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
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AccountController(UserManager<ApiUser> userManager, IMapper mapper, 
                                                                      IAuthManager authManager,
                                                                        ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
            
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
                user.UserName = userDto.Email;
                var result = await _userManager.CreateAsync(user, userDto.Paswword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest($"User registration attempt failed");
                }
                
                await _userManager.AddToRolesAsync(user, userDto.Roles);
                return Accepted(result);

            }
            catch (Exception)
            {
                _logger.LogInformation($"Something went wrong in the{nameof(Register)}");
                return Problem($"Something went wrong in the{nameof(Register)}", statusCode: 500);

            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto userDto)
        {
            _logger.LogInformation($"Login attempt for {userDto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Paswword, false, false);

                if (!await _authManager.ValidateUser(userDto))
                {
                    return Unauthorized(userDto);
                }
                return Accepted( new { Token = _authManager.CreateToken()});

            }
            catch (Exception)
            {
                _logger.LogInformation($"Something went wrong in the{nameof(Login)}");
                return Problem($"Something went wrong in the{nameof(Login)}", statusCode: 500);

            }
        }
    }
}
