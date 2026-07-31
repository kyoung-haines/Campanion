using App.API.Data;
using App.API.Models.Identity;
using Campanion.Shared.Dtos.AuthDtos;
using Campanion.Shared.Dtos.ProfileDtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IAppUserService _userService;
        private readonly IProfileService _profileService;
        private readonly ITokenService _tokenService;
        private readonly CampanionDbContext _dbContext;
        private AppUser _newUser = null;
        private Profile _profile = null;

        public AuthService(ILogger<AuthService> logger, IAppUserService userService, IProfileService profileService, ITokenService tokenService, CampanionDbContext dbContext)
        {
            _logger = logger;
            _userService = userService;
            _profileService = profileService;
            _tokenService = tokenService;
            _dbContext = dbContext;
        }

        public async Task<Result<RegisterResponseDto>> RegisterNewUserAsync(RegistrationDto regDto)
        {
            _logger.LogInformation("AuthService method called: RegisterNewUser...");
            _logger.LogInformation("Attempting to register a new user...");
            _logger.LogInformation("Starting a new database transaction...");

            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                _logger.LogInformation("Creating a new user...");
                _newUser = new AppUser
                {
                    UserName = regDto.AppUserUsername,
                    Email = regDto.AppUserEmail,
                    AppUserFirstName = regDto.AppUserFirstName,
                    AppUserLastName = regDto.AppUserLastName,
                    AppUserProvince = regDto.AppUserProvince,
                    AppUserCountry = regDto.AppUserCountry
                };

                var createUserResult = await _userService.CreateAppUserAsync(_newUser, regDto.AppUserPassword);

                if (!createUserResult.Succeeded)
                {
                    _logger.LogError("Failed to create and persist new user. Operation failed...");
                    await transaction.RollbackAsync();
                    return Result<RegisterResponseDto>.Failure("Failed to register new user.");
                }

                var newProfileResult = await _profileService.CreateNewProfileAsync(_newUser);

                if (!newProfileResult.Succeeded)
                {
                    _logger.LogInformation("Failed to create and persist the new user profile. Operation failed...");
                    await transaction.RollbackAsync();
                    return Result<RegisterResponseDto>.Failure("Failed to register new user.");
                }

                _profile = newProfileResult.Data;

                await transaction.CommitAsync();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to register new user. Entire operation aborted...");
                await transaction.RollbackAsync();
                return Result<RegisterResponseDto>.Failure("An unexpected error occurred. Failed to register new user.");
            }

            var token = await _tokenService.GenerateTokenAsync(_newUser);
            var profileResponseDto = new ProfileResponseDto
            {
                ProfileId = Convert.ToString(_profile.ProfileId),
                ProfileUsername = _profile.ProfileUsername,
                ProfileImagePath = _profile.ProfileImagePath,
                ProfileCreatedAt = Convert.ToString(_profile.ProfileCreatedAt),
                AppUserId = Convert.ToString(_newUser.Id)
            };
            var regResponseDto = new RegisterResponseDto
            {
                Token = token,
                Profile = profileResponseDto
            };

            return Result<RegisterResponseDto>.Success(regResponseDto);

        }

    }
}
