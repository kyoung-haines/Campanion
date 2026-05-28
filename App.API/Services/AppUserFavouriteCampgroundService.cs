using App.API.Models.Campgrounds;
using App.API.Repositories;
using System.Data;
using System.Runtime.CompilerServices;

namespace App.API.Services
{
    public class AppUserFavouriteCampgroundService
    {
        private readonly ILogger _logger;
        private readonly IAppUserFavouriteCampgroundRepository _favCampgroundRepo;

        public AppUserFavouriteCampgroundService(ILogger logger, IAppUserFavouriteCampgroundRepository _repo)
        {
            _logger = logger;
            _favCampgroundRepo = _repo;
        }

        public async Task<Result<bool>> DeleteFavouriteCampgroundAsync(AppUserFavouriteCampground favCampground)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete campground from favourites...");

                if(favCampground == null)
                {
                    _logger.LogWarning($"Favourite Campground cannot be deleted. Object is null...");
                }

                var result = await _favCampgroundRepo.DeleteFavouriteCampgroundAsync(favCampground);

                if(result.Succeeded == true)
                {
                    _logger.LogInformation($"Campground successfully removed from favourites...");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting campground from favourites...");
                return Result<bool>.Failure("Failed to delete campground from favourites.");

            }
        }

        public async Task<Result<AppUserFavouriteCampground>> AddFavouriteCampgroundAsync(AppUserFavouriteCampground favCampground)
        {
            return Result<AppUserFavouriteCampground>.Failure("Testing...");
        }
        public async Task<Result<List<AppUserFavouriteCampground>>> GetAllFavouriteCampgroundsAsync(int appUserId)
        {
            try
            {
                _logger.LogInformation($"Attempting to retrieve current user's favourite campgrounds...");
                var favCampgrounds = await _favCampgroundRepo.GetAllFavouriteCampgroundsAsync(appUserId);

                if(favCampgrounds.Succeeded == true)
                {
                    _logger.LogInformation($"Successfully retrieved user's favourite campgrounds...");
                }

                return Result<List<AppUserFavouriteCampground>>.Success(favCampgrounds.Data);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to retrieve user's favourite campgrounds. See exception for details...");

                return Result<List<AppUserFavouriteCampground>>.Failure("Failed to retrieve user's favourite campgrounds.");
            }            
        }
        public async Task<Result<AppUserFavouriteCampground>> GetFavouriteCampgroundByPrimaryKey(int campId, int userId)
        {
            return Result<AppUserFavouriteCampground>.Failure("Testing...");
        }
    }
}