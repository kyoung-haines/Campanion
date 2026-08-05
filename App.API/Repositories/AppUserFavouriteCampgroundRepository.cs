using App.API.Data;
using App.API.Models.Campgrounds;
using App.API.Exceptions.AppUserExceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using App.API.Models.Identity;
using App.API.Exceptions.CampgroundExceptions;

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
        public async Task<List<AppUserFavouriteCampground>> GetAllFavouriteCampgroundsAsync(string appUserId)
        {
            try
            {
                var favCampgrounds = new List<AppUserFavouriteCampground>();

                var appUser = await _userManager.FindByIdAsync(appUserId);

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
        public async Task<AppUserFavouriteCampground> GetFavouriteCampgroundByPrimaryKey(int campId, string userId)
        {
            var appUserFavourites = new AppUserFavouriteCampground();
            return appUserFavourites;
        }

        public async Task<AppUserFavouriteCampground> UpdateFavouriteCampgroundAsync(AppUserFavouriteCampground favouriteCampground)
        {
            try
            {
                _logger.LogInformation("AppUserFavouriteCampgroundRepository method called: UpdateFavouriteCampgroundAsync...");
                _logger.LogInformation($"Attempting to update favourite campground: {favouriteCampground.CampgroundId} for user: {favouriteCampground.AppUserId}...");

                if (favouriteCampground == null)
                {
                    _logger.LogWarning("Unable to edit favourite campground: object is null...");
                    throw new NullReferenceException("Failed to update favourite campground. The record is null.");
                }
                else
                {
                    _context.Update<AppUserFavouriteCampground>(favouriteCampground);
                    var saveTransactoin = await _context.SaveChangesAsync();

                    _logger.LogInformation("Successfully updates the favourite campground record...");
                    return favouriteCampground;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update favourite campground...");
                throw;
            }   
        }

        public async Task<AppUserFavouriteCampground> GetFavouriteCampgroundByCampgroundId(int campgroundId)
        {
            try
            {
                _logger.LogInformation("AppUserFavouriteCampgroundRepository method called: GetFavouriteCampgroundByCampgroundId...");
                _logger.LogInformation($"Attempting to retrieve favourite campground with campground ID: {campgroundId}...");

                var campgrounds = await _context.AppUserFavouriteCampgrounds.ToListAsync();

                var favouriteCampground = campgrounds.Where(campground => campground.CampgroundId == campgroundId).FirstOrDefault();

                if (favouriteCampground == null)
                {
                    throw new InvalidCampgroundIdException();
                }

                return favouriteCampground;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<AppUserFavouriteCampground> AddNewAppUserFavouriteCampgroundAsync(AppUserFavouriteCampground appUserFavouriteCampground)
        {
            try
            {
                _logger.LogInformation("AppUserFavouriteCampgroundRepository method called: AddNewAppUserFavouriteCampgroundAsync...");
                _logger.LogInformation($"Attempting to add new favourite campground for user: {appUserFavouriteCampground.AppUserId}...");



                await _context.AddAsync(appUserFavouriteCampground);

                await _context.SaveChangesAsync();

                return appUserFavouriteCampground;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}