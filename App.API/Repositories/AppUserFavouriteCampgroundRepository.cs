using App.API.Data;
using App.API.Models.Campgrounds;
using App.API.Exceptions.AppUserExceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using App.API.Models.Identity;

namespace App.API.Repositories
{
    public class AppUserFavouriteCampgroundRepository : IAppUserFavouriteCampgroundRepository
    {
        private readonly ILogger<AppUserFavouriteCampgroundRepository> _logger;
        private readonly CampanionDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AppUserFavouriteCampgroundRepository(ILogger<AppUserFavouriteCampgroundRepository> logger, CampanionDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> DeleteFavouriteCampgroundAsync(AppUserFavouriteCampground favouriteCampground)
        {
            try
            {
                _logger.LogInformation("AppUserFavouriteCampgroundRepository method called: DeleteFavouriteCampgroundAsync...");
                _logger.LogInformation($"Attempting to delete favourite campground...");

                if (favouriteCampground == null)
                {
                    _logger.LogWarning("Campground passed is empty. No changes made...");
                    throw new ArgumentNullException("The campground passed was null. Cannot delete a campground of null value.");
                }

                _context.Remove<AppUserFavouriteCampground>(favouriteCampground);

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Campground with ID: {favouriteCampground.CampgroundId} deleted...");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Unable to delete the favourite campground...");
                throw;
            }
        }
        public async Task<List<AppUserFavouriteCampground>> GetAllFavouriteCampgroundsAsync(int appUserId)
        {
            try
            {
                var favCampgrounds = new List<AppUserFavouriteCampground>();

                var appUser = await _userManager.FindByIdAsync(Convert.ToString(appUserId));

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

                return favCampgrounds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to retrieve user's favourite campgrounds. See exception for details.");
                throw;
            }
        }
        public async Task<AppUserFavouriteCampground> GetFavouriteCampgroundByPrimaryKey(int campId, int userId)
        {
            var appUserFavourites = new AppUserFavouriteCampground();
            return appUserFavourites;
        }

        Task<Result<bool>> IAppUserFavouriteCampgroundRepository.DeleteFavouriteCampgroundAsync(AppUserFavouriteCampground favouriteCampground)
        {
            throw new NotImplementedException();
        }

        Task<Result<List<AppUserFavouriteCampground>>> IAppUserFavouriteCampgroundRepository.GetAllFavouriteCampgroundsAsync(int appUserId)
        {
            throw new NotImplementedException();
        }

        Task<Result<AppUserFavouriteCampground>> IAppUserFavouriteCampgroundRepository.GetFavouriteCampgroundByPrimaryKey(int campId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}