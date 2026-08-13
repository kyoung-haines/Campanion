using App.API.Models.Campgrounds;
using Campanion.Shared.Dtos.AppUserDtos;
using App.API.Repositories;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using App.API.Models.Identity;

namespace App.API.Services
{
    public class AppUserFavouriteCampgroundService : IAppUserFavouriteCampgroundService
    {
        private readonly ILogger<AppUserFavouriteCampgroundService> _logger;
        private readonly IAppUserFavouriteCampgroundRepository _favCampgroundRepo;
        private readonly ICampgroundService _campgroundService;

        public AppUserFavouriteCampgroundService(
            ILogger<AppUserFavouriteCampgroundService> logger, 
            IAppUserFavouriteCampgroundRepository _repo,
            ICampgroundService campgroundService)
        {
            _logger = logger;
            _favCampgroundRepo = _repo;
            _campgroundService = campgroundService;
        }

        public async Task<Result<bool>> DeleteFavouriteCampgroundAsync(AppUserFavouriteCampgroundDto favCampgroundDto)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete campground from favourites...");

                if(favCampgroundDto == null)
                {
                    _logger.LogWarning($"Favourite Campground cannot be deleted. Object is null...");
                    return Result<bool>.Failure("Favourite Campground cannot be null.");
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

        public async Task<Result<AppUserFavouriteCampgroundDto>> AddFavouriteCampgroundAsync(AppUserFavouriteCampgroundDto favouriteCampgroundDto)
        {
            try
            {
                _logger.LogInformation("AppUserFavouriteCampgroundService method called: AddNewFavouriteCampgroundAscync...");
                _logger.LogInformation($"Attempting to add new favourite campground for user: {favouriteCampgroundDto.AppUserId}...");

                var favouriteCampground = await _favCampgroundRepo.GetFavouriteCampgroundByPrimaryKeyAsync(Convert.ToInt32(favouriteCampgroundDto.CampgroundId), favouriteCampgroundDto.AppUserId);

                await _favCampgroundRepo.AddNewAppUserFavouriteCampgroundAsync(favouriteCampground);

                return Result<AppUserFavouriteCampgroundDto>.Success(favouriteCampgroundDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to add new favourite campground for user: {favouriteCampgroundDto.AppUserId}");
                return Result<AppUserFavouriteCampgroundDto>.Failure("Failed to save the campground to favourites. Please try again.");
            }
        }

        public async Task<Result<List<AppUserFavouriteCampgroundDto>>> GetAllFavouriteCampgroundsByUserIdAsync(string appUserId)
        {
            try
            {
                _logger.LogInformation("AppUserFavouriteCampgroundService method called: GetAllFavouriteCampgroundsByUserIdAsync...");
                _logger.LogInformation($"Attempting to retrieve current user's favourite campgrounds...");
                var favCampgrounds = await _favCampgroundRepo.GetAllFavouriteCampgroundsByUserIdAsync(appUserId);

                if(favCampgrounds != null)
                {
                    _logger.LogInformation($"Successfully retrieved favourite campgrounds for user {appUserId}...");
                    if (favCampgrounds.Count() == 0)
                    {
                        _logger.LogInformation($"FavouriteCampgrounds list is empty for user: {appUserId}...");
                    }
                }

                var favCampgroundsDto = new AppUserFavouriteCampgroundsDto();
                var favCampgroundsDtoList = new List<AppUserFavouriteCampgroundDto>();

                foreach (var fav in favCampgrounds)
                {
                    var newFavCampDto = await this.ToDtoAsync(fav);

                    favCampgroundsDtoList.Add(newFavCampDto);
                }

                return Result<List<AppUserFavouriteCampgroundDto>>.Success(favCampgroundsDtoList);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to retrieve user's favourite campgrounds. See exception for details...");

                return Result<List<AppUserFavouriteCampgroundDto>>.Failure("Failed to retrieve user's favourite campgrounds.");
            }            
        }

        public async Task<Result<List<AppUserFavouriteCampgroundDto>>> GetAllAppUsersFavouriteCampgroundDtos()
        {
            try
            {
                _logger.LogInformation("AppUserFavouriteCampgroundService method called: GetAllAppUserFavouriteCampgrounDtos...");
                _logger.LogInformation("Attempting to retrieve all DTOs for all users...");

                var allFavouriteCampgrounds = await _favCampgroundRepo.GetAllAppUsersFavouriteCampgrounds();

                List<AppUserFavouriteCampgroundDto> allFavouriteCampgroundsDto = new();

                foreach (var campground in allFavouriteCampgrounds)
                {
                    var favouriteCampgroundDto = await this.ToDtoAsync(campground);
                    allFavouriteCampgroundsDto.Add(favouriteCampgroundDto);
                }

                return Result<List<AppUserFavouriteCampgroundDto>>.Success(allFavouriteCampgroundsDto);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Failed to retrieve all favourite campgrounds for all users...");
                throw;
            }
        }

        // HELPET METHOD TO BE MOVED - EITHER INTO MODEL OR INTO SHARED LIBRARY
        public async Task<AppUserFavouriteCampgroundDto> ToDtoAsync(AppUserFavouriteCampground favCampground)
        {
            var favCampgroundDto = new AppUserFavouriteCampgroundDto
            {
                AppUserId = favCampground.AppUserId,
                CampgroundId = favCampground.CampgroundId.ToString(),
                CampgroundName = favCampground.Campground.CampgroundName,
                CampgroundImagePath = favCampground.Campground.CampgroundImagePath
            };

            return favCampgroundDto;
        }
    }
}