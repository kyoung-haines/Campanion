using App.API.Models.Campgrounds;
using Campanion.Shared.Dtos.AppUserDtos;
using App.API.Repositories;
using System.Data;
using System.Runtime.CompilerServices;

namespace App.API.Services
{
    public class AppUserFavouriteCampgroundService : IAppUserFavouriteCampgroundService
    {
        private readonly ILogger<AppUserFavouriteCampgroundService> _logger;
        private readonly IAppUserFavouriteCampgroundRepository _favCampgroundRepo;

        public AppUserFavouriteCampgroundService(ILogger<AppUserFavouriteCampgroundService> logger, IAppUserFavouriteCampgroundRepository _repo)
        {
            _logger = logger;
            _favCampgroundRepo = _repo;
        }

        public async Task<Result<bool>> DeleteFavouriteCampgroundAsync(AppUserFavouriteCampgroundDto favCampgroundDto)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete campground from favourites...");

                if(favCampgroundDto == null)
                {
                    _logger.LogWarning($"Favourite Campground cannot be deleted. Object is null...");
                }

                var favouriteCampground = await _favCampgroundRepo.GetFavouriteCampgroundByCampgroundId(Convert.ToInt32(favCampgroundDto.CampgroundId));

                var result = await _favCampgroundRepo.DeleteFavouriteCampgroundAsync(favouriteCampground);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting campground from favourites...");
                return Result<bool>.Failure("Failed to delete campground from favourites.");

            }
        }

        public async Task<Result<AppUserFavouriteCampgroundDto>> AddFavouriteCampgroundAsync(AppUserFavouriteCampgroundDto favCampgroundDto)
        {
            try
            {
                _logger.LogInformation("AppUserFavouriteCampgroundService method called: AddNewFavouriteCampgroundAscync...");
                _logger.LogInformation($"Attempting to add new favourite campground for user: {favCampgroundDto.AppUserId}...");

                var favouriteCampgroundObject = await _favCampgroundRepo.GetFavouriteCampgroundByPrimaryKeyAsync(Convert.ToInt32(favCampgroundDto.CampgroundId), favCampgroundDto.AppUserId);

                await _favCampgroundRepo.AddNewAppUserFavouriteCampgroundAsync(favouriteCampgroundObject);

                

                return Result<AppUserFavouriteCampgroundService>.Success(favouriteCampgroundDto);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Result<List<AppUserFavouriteCampground>>> GetAllFavouriteCampgroundsAsync(string appUserId)
        {
            try
            {
                _logger.LogInformation($"Attempting to retrieve current user's favourite campgrounds...");
                var favCampgrounds = await _favCampgroundRepo.GetAllFavouriteCampgroundsAsync(appUserId);

                if(favCampgrounds.Succeeded == true)
                {
                    _logger.LogInformation($"Successfully retrieved favourite campgrounds for user {appUserId}...");
                }
                
                if(favCampgrounds.Succeeded == true && favCampgrounds.Data.Count() == 0)
                {
                    _logger.LogInformation($"Favourite campgrounds list is empty for user {appUserId}...");
                    _logger.LogInformation("Returning the empty list...");
                }

                return Result<List<AppUserFavouriteCampground>>.Success(favCampgrounds.Data);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to retrieve user's favourite campgrounds. See exception for details...");

                return Result<List<AppUserFavouriteCampground>>.Failure("Failed to retrieve user's favourite campgrounds.");
            }            
        }
        public async Task<Result<AppUserFavouriteCampground>> GetFavouriteCampgroundByPrimaryKey(int campId, string userId)
        {
            return Result<AppUserFavouriteCampground>.Failure("Testing...");
        }
    }
}