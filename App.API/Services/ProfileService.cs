using App.API.Models.Identity;
using App.API.Repositories;
using Campanion.Shared.Dtos.ProfileDtos;

namespace App.API.Services
{
	public class ProfileService : IProfileService
	{
		private ILogger<ProfileService> _logger;
		private IProfileRepository _profileRepository;

		public ProfileService(ILogger<ProfileService> logger, IProfileRepository profileRepository)
		{
			_logger = logger;
			_profileRepository = profileRepository;
		}

		public async Task<Result<Profile>> GetProfileByProfileIdAsync(int id)
		{
			try
			{
				_logger.LogInformation("ProfileService method called: GetProfileByIdAsync...");
				var profile = await _profileRepository.GetProfileByIdAsync(id);

				Result<Profile> profileResult = Result<Profile>.Success(profile);

				return profileResult;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to get profile by ID: {id}...");
				return Result<Profile>.Failure($"An error occurred while retrieving the profile: {ex.Message}");
			}
		}

		public async Task<Result<Profile>> UpdateProfileAsync(int id)
		{
			try
			{
				_logger.LogInformation("ProfileService method called: UpdateProfileAsync...");

				var profile = await _profileRepository.UpdateProfileAsync(id);

				Result<Profile> profileResult = Result<Profile>.Success(profile);

				return profileResult;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to update profile with ID: {id}...");
				return Result<Profile>.Failure($"An error occurred while updating the profile: {ex.Message}");
			}
		}

		public async Task<Result<bool>> DeleteProfileAsync(int id)
		{
			try
			{
				_logger.LogInformation("ProfileService method called: DeleteProfileAsync...");

				var profileResult = await _profileRepository.DeleteProfileAsync(id);

				return Result<bool>.Success(true);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to delete profile with ID: {id}...");
				return Result<bool>.Failure("An error occurred attempting to delete the profile. Try again");
			}
		}

		public async Task<Result<Profile>> CreateNewProfileAsync(AppUser newUser)
		{
			try
			{
				_logger.LogInformation("ProfileService method called: CreateNewProfileAsync...");
				var newProfile = await _profileRepository.CreateNewProfileAsync(newUser);

				var newProfileResult = Result<Profile>.Success(newProfile);

				return newProfileResult;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to create new profile in the system)");

				return Result<Profile>.Failure($"Failed to create new profile in the system: {ex.Message}");
			}
		}

		public async Task<Result<Profile>> GetProfileByAppUserIdAsync(AppUser appUser)
		{
			try
			{
				_logger.LogInformation("ProfileService method called: GetProfileByAppUserIdAsync...");
				int appUserId = Convert.ToInt32(appUser.Id);

				Profile profile = await _profileRepository.GetProfileByAppUserIdAsync(appUser);

				_logger.LogInformation("Profile retrieved...");

				return Result<Profile>.Success(profile);
			}
			catch (Exception ex)
			{

				return Result<Profile>.Failure("Failed to retrieve the user profile. Try again.");
			}
		}

		public async Task<ProfileResponseDto> ConvertProfileObjectToResponseDto(Profile profile)
		{
			_logger.LogInformation("ProfileService method called: ConvertProfileObjectToResponseDto...");
			_logger.LogInformation("Attempting to convert Profile object...");

			ProfileResponseDto profileResponseDto = new ProfileResponseDto
			{
				ProfileId = profile.ProfileId.ToString(),
				ProfileUsername = profile.ProfileUsername,
				ProfileImagePath = profile.ProfileImagePath,
				ProfileCreatedAt = profile.ProfileCreatedAt.ToString(),
				AppUserId = profile.AppUserId.ToString()
			};

			return profileResponseDto;
		}
	}
}