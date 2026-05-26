using App.API.Models.Campgrounds;
using App.API.Data;
using App.API.Exceptions;

using Microsoft.EntityFrameworkCore;

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
                //throw new RepositoryException("Failed to delete the campground from the database.", ex); ;
                _logger.LogError(ex, "Error deleting the campground from the database.");
                return Result<bool>.Failure("Failed to delete the campground from the database.");
            }
        }
        //public async Task<IEnumerable<Campground>> GetAllCampgroundsAsync()
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

        public async Task<Campground> UpdateCampgroundAsync(Campground originalCampground)
        {
            if(originalCampground == null)
            {
                _logger.LogError("Campground cannot be null...");
                throw new Exception("Campground not updated. Try again.");
            }

            var originalCampgroundCopy = originalCampground;

            _context.Update<Campground>(originalCampground);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Campground Successfully updated...");

            return originalCampground;
        }

        public async Task AddCampgroundAsync(Campground newCampground)
        {
            if(newCampground == null)
            {
                _logger.LogError("Campground is currently null...");
                throw new Exception("Campground not added. Try again.");
            }

            await _context.AddAsync<Campground>(newCampground);
        }
    }
}