using App.API.Services;
using Campanion.Shared.Dtos.ProfileDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly ILogger<ProfilesController> _logger;
        private readonly IAppUserService _userService;
        private readonly IProfileService _profileService;

        public ProfilesController(ILogger<ProfilesController> logger, IAppUserService userService, IProfileService profileService)
        {
            _logger = logger;
            _userService = userService;
            _profileService = profileService;
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<ActionResult<ProfileResponseDto>> GetCurrentUserProfile()
        {

            _logger.LogInformation("AuthController method called: GetCurrentUserProfile...");

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userResult = await _userService.GetAppUserByIdAsync(userIdClaim);

            if (userResult.Succeeded == false)
            {
                _logger.LogWarning("Failed to retrieve the user profile...");
                return BadRequest(userResult.Error);
            }

            var userProfileResult = await _profileService.GetProfileByAppUserIdAsync(userResult.Data);

            if (userProfileResult.Succeeded == false)
            {
                _logger.LogWarning($"Error retrieving profile. Ensure the ProfileId is valid...");
                return BadRequest(userProfileResult.Error);
            }

            _logger.LogInformation("Profile successfully retrieved....");

            var profileResponseDto = await _profileService.ConvertProfileObjectToResponseDto(userProfileResult.Data);

            return Ok(profileResponseDto);
        }
    }
}
