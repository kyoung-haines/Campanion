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
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private IAppUserService _userService;
        private IAuthService _authService;

        public AuthController(UserManager<AppUser> userManager, ITokenService tokenService, IAppUserService userService, IAuthService authService, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userService = userService;
            _authService = authService;
            _logger = logger;
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

            return Created("/api/v1/profile", regResponseData);
        }

        /*
         * not sure if I want to keep this here or not...
         * retrieving the user profile isn't exactly an Authorization function but
         * there is an argument for there being a function to retrieve the user profile directly
         * at successful user registration time - however, 
         * the logic will likely overlap largely with a similar method in the AppUserController instead...
        [Authorize(Roles="Role.Member", "Role.Admin")]
        [HttpGet("profile")]
        public async Task<ActionResult><ProfileResponseDto> GetNewUserProfile()
        {

        }
        */
    }
}
