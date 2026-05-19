using App.API.Models.Campgrounds;
using App.API.Data;
using App.API.Services;

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

        public async Task DeleteCampground(int id)
        {
            _logger.LogInformation($"Attempting to delete user with ID: {id}...");
            _logger.LogInformation($"Looking for campground in the system...");

            var campground = await _context.FindAsync<Campground>(id);

            if(campground == null)
            {
                _logger.LogError("Campground not found. Check the ID value.");
                throw new Exception("Campground not found. Please try again.");
            }

            _logger.LogInformation("Campground found. Attempting to delete...");

            _context.Remove<Campground>(campground);

            
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
                throw new Exception("Campground cannot be null.");
            }

            _logger.LogInformation($"Campground found. Returning campground with ID: {id}");

            return campground;
        }

        public async Task UpdateCampgroundAsync(Campground originalCampground)
        {
            if(originalCampground == null)
            {
                _logger.LogError("Campground cannot be null...");
                throw new Exception("Campground does not exist.");
            }

            _logger.LogInformation("Campground Successfully updated...");
        }
    }
}