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

        public async Task<Result<Campground>> DeleteCampgroundAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete campground with ID: {id}...");
                _logger.LogInformation($"Looking for campground in the system...");

                var campground = await _context.FindAsync<Campground>(id);

                _logger.LogInformation("Campground found. Attempting to delete...");

                _context.Remove<Campground>(campground);

                await _context.SaveChangesAsync();

                return Result<Campground>.Success(campground); // this should be null if successful
            }
            catch(RepositoryException ex)
            {
                //throw new RepositoryException("Failed to delete the campground from the database.", ex); ;
                _logger.LogError(ex, "Error deleting the campground from the database.");
                return Result<Campground>.Failure("An error occured while deleting the campground from the database.");
            }
        }
        public async Task<IEnumerable<Campground>> GetAllCampgroundsAsync()
        {
            _logger.LogInformation("GetAllCampgroundsAsync method called...");
            _logger.LogInformation("Retrieving all campgrounds...");

            var campgrounds = await _context.Campgrounds.ToListAsync();

            if (campgrounds == null)
            {
                _logger.LogInformation("There are no campgrounds to retrieve...");
                throw new Exception("There are no campgrounds to retrieve.");
            }

            return campgrounds;
        }
        public async Task<Campground> GetCampgroundByIdAsync(int id)
        {
            var campground = await _context.FindAsync<Campground>(id);

            if(campground == null)
            {
                _logger.LogError("Campground is null...");
                throw new Exception("Campground not found Please try again.");
            }

            _logger.LogInformation($"Campground found. Returning campground with ID: {id}");

            return campground;
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