using App.API.Models.Campgrounds;
using App.API.Data;

using Microsoft.EntityFrameworkCore;
using App.API.Exceptions.RepositoryExceptions;

namespace App.API.Repositories
{
    public class CampgroundRepository
    {
        private readonly ILogger<CampgroundRepository> _logger;
        private readonly CampanionDbContext _context;

        public CampgroundRepository(ILogger<CampgroundRepository> logger, CampanionDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // TO-DO: REFACTOR METHOD RETURN TYPES TO Result<T>
        // NOTE: USE bool as the data type of the generic specification where applicable

        public async Task<Result<bool>> DeleteCampgroundAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete campground with ID: {id}...");
                _logger.LogInformation($"Looking for campground in the system...");

                var campground = await _context.FindAsync<Campground>(id);

                _logger.LogInformation("Campground found. Attempting to delete...");

                _context.Remove<Campground>(campground);

                await _context.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
            catch(RepositoryException ex)
            {
                _logger.LogError(ex, "Error deleting the campground from the database.");
                return Result<bool>.Failure("Failed to delete the campground from the database.");
            }
        }

        public async Task<Result<List<Campground>>> GetAllCampgroundsAsync()
        {
            try
            {
                _logger.LogInformation("GetAllCampgroundsAsync method called...");
                _logger.LogInformation("Retrieving all campgrounds...");

                var campgrounds = await _context.Campgrounds.ToListAsync();

                return Result<List<Campground>>.Success(campgrounds);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error retrieving all Campgrounds from database...");
                return Result<List<Campground>>.Failure("Error retrieving all campgrounds.");
            }
        }

        public async Task<Result<Campground>> GetCampgroundByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to retrieve campground ID: {id}...");
                var campgroundResult = await _context.FindAsync<Campground>(id);

                return Result<Campground>.Success(campgroundResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve campground with ID: {id}...");
                return Result<Campground>.Failure("Failed to retrieve the campground from the database");
            }
        }

        public async Task<Result<Campground>> UpdateCampgroundAsync(Campground originalCampground)
        {
            try
            {
                _logger.LogInformation($"Attempting to update campground with ID: {originalCampground.CampgroundId}...");
                _context.Update(originalCampground);
                _context.SaveChangesAsync();
                _logger.LogInformation($"Campground with ID: {originalCampground.CampgroundId} successfully updated...");
                return Result<Campground>.Success(originalCampground);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Campground failed to update. See stack trace for details...");
                return Result<Campground>.Failure("Failed to update the campground in the database.");
            }
        }

        public async Task<Result<Campground>> AddCampgroundAsync(Campground newCampground)
        {
            try
            {
                _logger.LogInformation($"Attempting to add new campgroundwith ID: {newCampground.CampgroundId}...");
                _context.Add<Campground>(newCampground);
                _context.SaveChangesAsync();
                _logger.LogInformation($"Campground successfully added...");
                return Result<Campground>.Success(newCampground);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add the campground to the database. See stack trace for details...");
                return Result<Campground>.Failure("Failed to add the campground to the database");
            }
        }
    }
}