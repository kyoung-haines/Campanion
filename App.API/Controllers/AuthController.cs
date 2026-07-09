using App.API.Models.Identity;
using App.API.Services;
using Campanion.Shared.Dtos.AuthDtos;
using Campanion.Shared.Dtos.ProfileDtos;
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
        private IAuthService _authService;

        public AuthController(UserManager<AppUser> userManager, ITokenService tokenService, IAppUserService userService, IAuthService authService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userService = userService;
            _authService = authService;
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<ProfileResponseDto>> RegisterUser(RegistrationDto registrationDto)
        {
            var regResponseDto = await _authService.RegisterNewUserAsync(registrationDto);
            var regResponse = regResponseDto.Data;
            var profileDto = regResponse.Profile;

            var profileResponseDto = new ProfileResponseDto
            {
                ProfileId = Convert.ToString(profileDto.ProfileId),
                ProfileUsername = profileDto.ProfileUsername,
                ProfileCreatedAt = Convert.ToString(profileDto.ProfileCreatedAt),
                ProfileImagePath = profileDto.ProfileImagePath,
            };

            return Ok(profileResponseDto);
        }
    }
}
