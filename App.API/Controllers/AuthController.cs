using App.API.Models.Identity;
using App.API.Services;
using Campanion.Shared.Dtos.AuthDtos;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private IAppUserService _userService;

        public AuthController(UserManager<AppUser> userManager, ITokenService tokenService, IAppUserService userService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> UserLogin(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Unauthorized();
            }

            var token = await _tokenService.GenerateTokenAsync(user);

            return Ok(new AuthResponseDto { Token = token });
        }

        // take in reg dto -> create profile dto
        // create profile from create profile dto
        // serve profile response dto?
        /*
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<ProfileResponseDto>> RegisterUser(RegistrationDto regDto)
        {
            AppUser newUser = new AppUser()
            {
                Email = regDto.AppUserEmail,
                AppUserFirstName = regDto.AppUserFirstName,
                AppUserLastName = regDto.AppUserLastName,
                AppUserProvince = regDto.AppUserProvince,
                AppUserCountry = regDto.AppUserCountry
            };

            var saveUserResult = await _userService.CreateAppUserAsync(newUser);

            if (saveUserResult.Succeeded == true)
            {
                var user = await _userService.GetAppUserByEmailAsync(newUser.Email);
                var profileDto = new ProfileResponseDto()
                {
                    
                };
            }
        }
        */
    }

    public class RegistrationDto()
    {
        public string AppUserPassword = string.Empty;
        public string AppUserEmail = string.Empty;
        public string AppUserPhone = string.Empty;
        public string AppUserFirstName = string.Empty;
        public string AppUserLastName = string.Empty;
        public string AppUserStreetAddress = string.Empty;
        public string AppUserCity = string.Empty;
        public string AppUserProvince = string.Empty;
        public string AppUserCountry = string.Empty;
        public string AppUserPostalCode = string.Empty;
    }

    public class ProfileResponseDto()
    {
        public string ProfileUsername = string.Empty;
    }
}
