using App.API.Models.Identity;
using Campanion.Shared.Dtos.AuthDtos;

namespace App.API.Services
{
    public class AuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IAppUserService _userService;
        private readonly IProfileService _profileService;
        private readonly ITokenService _tokenService;

        public async Task<Result<RegisterResponseDto>> RegisterNewUser(RegistrationDto regDto)
        {
            try
            {
                _logger.LogInformation("AuthService method called: RegisterNewUser...");
                _logger.LogInformation("Attempting to register a new user...");

                _logger.LogInformation("Creating a new user...");
                AppUser newUser = new AppUser
                {
                    UserName = regDto.AppUserUsername,
                    AppUserFirstName = regDto.AppUserFirstName,
                    AppUserLastName = regDto.AppUserLastName,
                    AppUserProvince = regDto.AppUserProvince,
                    AppUserCountry = regDto.AppUserCountry
                };

                var createUserResult = await _userService.CreateAppUserAsync(newUser, regDto.AppUserPassword);

                if (!createUserResult.Succeeded)
                {
                    return Result<RegisterResponseDto>.Failure("Failed to register new user.");
                }

                // this is probably not even neessary
                //var createProfileDto = new CreateProfileDto
                //{

                //};

                var newProfileResult = await _profileService.CreateNewProfileAsync(newUser);

                if (!newProfileResult.Succeeded)
                {
                    return Result<RegisterResponseDto>.Failure("Failed to register new user.");
                }

                var profile = newProfileResult.Data;
                var token = await _tokenService.GenerateTokenAsync(newUser);
                var profileResponseDto = new ProfileResponseDto
                {
                    ProfileId = Convert.ToString(profile.ProfileId),
                    ProfileUsername = profile.ProfileUsername,
                    ProfileImagePath = profile.ProfileImagePath,
                    ProfileCreatedAt = Convert.ToString(profile.ProfileCreatedAt),
                    AppUserId = Convert.ToString(profile.AppUserId)
                };
                var regResponseDto = new RegisterResponseDto
                {
                    Token = token,
                    Profile = profileResponseDto
                };

                return Result<RegisterResponseDto>.Success(regResponseDto);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Result<bool>> IsValidPassword(string password)
        {
            _logger.LogInformation("Validating user password against validation criteria...");

            (int length, bool hasCapital) validationCriteria = (8, true);

            if (password.Length != validationCriteria.length)
            {
                _logger.LogWarning("User password is too short. Doesn't meet criteria and failed validation...");
                return Result<bool>.Failure("Password must be a minimum of 8 characters long.");
            }

            return Result<bool>.Success(true);
        }

    }
}
