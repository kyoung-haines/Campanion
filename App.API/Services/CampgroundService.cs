using App.API.Models.Campgrounds;
using App.API.Repositories;

namespace App.API.Services
{
    public class CampgroundService
    {
        private readonly ILogger<CampgroundService> _logger;
        private readonly ICampgroundRepository _campgroundRepo;

        public CampgroundService(ILogger<CampgroundService> logger, ICampgroundRepository campgroundRepo)
        {
            _logger = logger;
            _campgroundRepo = campgroundRepo;
        }

        // QUICK COMMENT - needs doc comments still
        // This will reference a given CampgroundId (ID of newly added campground)
        // against all campgrounds to ensure that it has been added to the system.
        // uses Repo layer FindCampgroundByIdAsync method
        public async Task<bool> CampgroundIdIsExists(int newCampgroundId)
        {
            _logger.LogInformation("Verifying the new campground is added...");
            
            var campground = await _campgroundRepo.GetCampgroundByIdAsync(newCampgroundId);

            var isAdded = false;
            if(isAdded != true)
            {
                _logger.LogError("Campground not added...ID not found...");
                throw new Exception("Campground unsucessfully added. Please try again.");
            }

            _logger.LogInformation("Campground added to the system...");

            return isAdded;
        }

        public async Task<bool> IsCampgroundUpdated(Campground originalCampground, Campground updatedCampground)
        {
            _logger.LogInformation("Checking if campground has been updated...");

            var result = originalCampground.Equals(updatedCampground);

            if(result == true)
            {
                _logger.LogError("Campground unsuccessfully updated...");
                throw new Exception("Campground failed to update. Try again.");
            }

            _logger.LogInformation("Campground has been updated...");

            return result;
        }
    }
}