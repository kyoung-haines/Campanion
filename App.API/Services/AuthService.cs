using App.API.Models.Identity;
using App.API.Data;
using Campanion.Shared.Dtos.AuthDtos;
using Campanion.Shared.Dtos.ProfileDtos;

namespace App.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IAppUserService _userService;
        private readonly IProfileService _profileService;
        private readonly ITokenService _tokenService;
        private readonly CampanionDbContext _dbContext;

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
                AppUser newUser = new AppUser
                {
                    UserName = regDto.AppUserUsername,
                    Email = regDto.AppUserEmail,
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

                var newProfileResult = await _profileService.CreateNewProfileAsync(newUser);

                if (!newProfileResult.Succeeded)
                {
                    return Result<RegisterResponseDto>.Failure("Failed to register new user.");
                }

                await transaction.CommitAsync();

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
                _logger.LogError(ex.Message);
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
