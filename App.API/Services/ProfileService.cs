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
    }
}
