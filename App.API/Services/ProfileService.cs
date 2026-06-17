using App.API.Models.Identity;
using App.API.Repositories;

namespace App.API.Services
{
    public class ProfileService
    {
		private ILogger<ProfileService> _logger;
		private IProfileRepository _profileRepository;

		public ProfileService(ILogger<ProfileService> logger, IProfileRepository profileRepository)
		{
			_logger = logger;
			_profileRepository = profileRepository;
        }

        public async Task<Result<Profile>> GetProfileByIdAsync(int id)
        {
			try
			{
				_logger.LogInformation("ProfileService method called: GetProfileByIdAsync...");
				var profileResult = await _profileRepository.GetProfileByIdAsync(id);
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

				var profileResult = await _profileRepository.UpdateProfileAsync(id);

				return Result<Profile>.Success(profileResult.Data);
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
				var newUserResult = await _profileRepository.CreateNewProfileAsync(newUser);

				return Result<Profile>.Success(newUserResult.Data);
            }
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to create new profile in the system)");

				return Result<Profile>.Failure($"Failed to create new profile in the system: {ex.Message}");
            }
		}
    }
}
