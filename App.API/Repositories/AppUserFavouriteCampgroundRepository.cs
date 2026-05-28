using App.API.Data;
using App.API.Models.Campgrounds;
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
        public async Task<Result<List<AppUserFavouriteCampground>>> GetAllFavouriteCampgroundsAsync()
        {
            try
            {
                _logger.LogInformation($"Attempting to retrieve all favourite campgrounds for current user...");

                var favCampgrounds = await _context.AppUserFavouriteCampgrounds.ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Result<AppUserFavouriteCampground>> GetFavouriteCampgroundByPrimaryKey(int campId, int userId)
        {
            return Result<AppUserFavouriteCampground>.Failure("Testing...");
        }
    }
}