using App.API.Models.Campgrounds;
using App.API.Models.Identity;
using App.API.Models.Trips;
using App.API.Repositories;
using Campanion.Shared.Dtos.AppUserDtos;
using Campanion.Shared.Dtos.ProfileDtos;
using System.Security.Cryptography;

namespace App.API.Services
{
	public class ProfileService : IProfileService
	{
		private ILogger<ProfileService> _logger;
		private IProfileRepository _profileRepository;
		private IAppUserTripService _appUserTripService;

		public ProfileService(ILogger<ProfileService> logger, IProfileRepository profileRepository, IAppUserTripService userTripService)
		{
			_logger = logger;
			_profileRepository = profileRepository;
			_appUserTripService = userTripService;

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

		public async Task<Result<Profile>> UpdateProfileAsync(ProfileResponseDto profileDto)
		{
			try
			{
				_logger.LogInformation("ProfileService method called: UpdateProfileAsync...");
				var profileResult = await this.GetProfileByProfileIdAsync(Convert.ToInt32(profileDto.ProfileId));

				Profile profile = profileResult.Data;

				var profileUpdate = await _profileRepository.UpdateProfileAsync(profile);

				return Result<Profile>.Success(profile);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to update profile with ID: {profileDto.ProfileId}...");
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

		public async Task<Result<ProfileResponseDto>> GetProfileByAppUserIdAsync(AppUser appUser)
		{
			try
			{
				_logger.LogInformation("ProfileService method called: GetProfileByAppUserIdAsync...");

				Profile profile = await _profileRepository.GetProfileByAppUserIdAsync(appUser.Id);

				_logger.LogInformation("Profile retrieved...");

				var profileResponseDto = await profile.ConvertProfileObjectToResponseDto(profile, appUser);

				return Result<ProfileResponseDto>.Success(profileResponseDto);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to retrieve profile by AppUserId: {appUser.Id}...");
				return Result<ProfileResponseDto>.Failure("Failed to retrieve the user profile. Try again.");
			}
		}

        public async Task<Result<List<AppUserFavouriteCampgroundDto>>> RetrieveProfileUserFavouriteCampgrounds(AppUserFavouriteCampgroundService appUserFavouriteCampgroundService)
        {
			var allFavouriteCampgroundDtosResult = await appUserFavouriteCampgroundService.GetAllAppUsersFavouriteCampgroundDtos();
			var allFavouriteCampgrounds = allFavouriteCampgroundDtosResult.Data;

			return Result<List<AppUserFavouriteCampgroundDto>>.Success(allFavouriteCampgrounds);
        }

        public async Task<Result<List<AppUserTripDto>>> RetrieveProfileOwnerUpcomingTrips(AppUser appUser)
        {
			var appUserTripDtosResult = await _appUserTripService.RetrieveAllAppUserTripsByUserIdAsync(appUser.Id);
			var appUserTripDtos = appUserTripDtosResult.Data.ToList();

			return Result<List<AppUserTripDto>>.Success(appUserTripDtos);
        }
    }
}