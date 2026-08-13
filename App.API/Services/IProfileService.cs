using App.API.Models.Campgrounds;
using App.API.Models.Identity;
using App.API.Models.Trips;
using Campanion.Shared.Dtos.ProfileDtos;
using Campanion.Shared.Dtos.AppUserDtos;

namespace App.API.Services
{
    public interface IProfileService
    {
        public Task<Result<ProfileResponseDto>> GetProfileByProfileIdAsync(int id);
        public Task<Result<ProfileResponseDto>> UpdateProfileAsync(ProfileResponseDto profileDto);
        public Task<Result<bool>> DeleteProfileAsync(int id);
        public Task<Result<ProfileResponseDto>> CreateNewProfileAsync(AppUser newUser);
        public Task<Result<ProfileResponseDto>> GetProfileByAppUserIdAsync(string appUserId);
        public Task<Result<List<AppUserFavouriteCampgroundDto>>> RetrieveProfileUserFavouriteCampgrounds(AppUserFavouriteCampgroundService appUserFavouriteCampgroundService);
        public Task<Result<List<AppUserTripDto>>> RetrieveProfileOwnerUpcomingTrips(AppUser appUser);
    }
}