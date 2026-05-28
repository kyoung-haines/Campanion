using App.API.Data;
using App.API.Models.Campgrounds;
using App.API.Exceptions.AppUserExceptions;
using Microsoft.EntityFrameworkCore;

namespace App.API.Repositories
{
    public class AppUserFavouriteCampgroundRepository
    {
        private readonly ILogger _logger;
        private readonly CampanionDbContext _context;

        public AppUserFavouriteCampgroundRepository(ILogger logger, CampanionDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<bool>> DeleteFavouriteCampgroundAsync(AppUserFavouriteCampground favouriteCampground)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete favourite campground...");

                _context.Remove<AppUserFavouriteCampground>(favouriteCampground);

                await _context.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Unable to delete the favourite campground...");
                return Result<bool>.Failure($"Failed to delete the favourite campground.");
            }
        }
        public async Task<Result<List<AppUserFavouriteCampground>>> GetAllFavouriteCampgroundsAsync(int appUserId)
        {
            try
            {
                var favCampgrounds = new List<AppUserFavouriteCampground>();

                var appUser = await _context.AppUsers.FindAsync(appUserId);

                if(appUser != null)
                {
                    _logger.LogInformation($"Attempting to retrieve all favourite campgrounds for current user...");

                    favCampgrounds = await _context.AppUserFavouriteCampgrounds
                        .Where(fav => fav.AppUserId == appUserId)
                        .ToListAsync();


                    if (favCampgrounds.Count() == 0)
                    {
                        _logger.LogWarning($"Favourites list is empty. User either has no favourites or an error occurred...");
                    }
                }
                else
                {
                    _logger.LogError($"AppUserId: {appUserId} does not exist.");
                    throw new AppUserException($"Invalid UserID. Verify that ID: {appUserId} exists.");
                }

                return Result<List<AppUserFavouriteCampground>>.Success(favCampgrounds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to retrieve user's favourite campgrounds. See exception for details.");
                return Result<List<AppUserFavouriteCampground>>.Failure(ex.Message);
            }
        }
        public async Task<Result<AppUserFavouriteCampground>> GetFavouriteCampgroundByPrimaryKey(int campId, int userId)
        {
            return Result<AppUserFavouriteCampground>.Failure("Testing...");
        }
    }
}