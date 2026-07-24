using App.API.Models.Identity;
using App.API.Services;
using Campanion.Shared.Dtos.AuthDtos;
using Campanion.Shared.Dtos.ProfileDtos;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private IAppUserService _userService;
        private IAuthService _authService;
        private IProfileService _profileService;

        public AuthController(UserManager<AppUser> userManager, ITokenService tokenService, IAppUserService userService, IAuthService authService, ILogger<AuthController> logger, IProfileService profileService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userService = userService;
            _authService = authService;
            _logger = logger;
            _profileService = profileService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> UserLogin(LoginDto loginDto)
        {
            _logger.LogInformation("HTTP REQUEST RECEIVED...");
            _logger.LogInformation("AuthController method called: UserLogin...");
            _logger.LogInformation($"USER EMAIL:\n{loginDto.Email}");

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
        public async Task<ActionResult<RegisterResponseDto>> RegisterUser(RegistrationDto registrationDto)
        {
            _logger.LogInformation("AuthController method called: RegisterUser...");
            _logger.LogInformation("Contacting: /api/v1/auth/register...");

            var regResponseDto = await _authService.RegisterNewUserAsync(registrationDto);

            if (regResponseDto.Succeeded != true)
            {
                _logger.LogWarning("Error creating the RegistrationResponseDto...Process aborted...");

                return BadRequest(regResponseDto.Error); // returns error message from the Result object
            }

            var regResponseData = regResponseDto.Data;

            return Created("/api/v1/profiles/profile", regResponseData);
        }
    }
}
