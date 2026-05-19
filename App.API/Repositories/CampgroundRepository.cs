using App.API.Models.Campgrounds;
using App.API.Data;
using Microsoft.AspNetCore.Http.HttpResults;

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

        async Task DeleteCampground(int id)
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
        Task<IEnumerable<Campground>> GetAllCampgroundsAsync();
        Task<Campground> GetCampgroundByIdAsync(int id);
        Task UpdateCampgroundAsync(int id);
    }
}