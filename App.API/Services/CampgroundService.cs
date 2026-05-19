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

        public async Task DeleteCampground(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete campground with ID: {id}");

                var isExists = await CampgroundIdIsExists(id);
            }
            catch (Exception e)
            {

                throw new Exception (e.Message);
            }
        }

        // QUICK COMMENT - needs doc comments still
        // This will reference a given CampgroundId (ID of newly added campground)
        // against all campgrounds to ensure that it has been added to the system.
        // uses Repo layer FindCampgroundByIdAsync method
        public async Task<bool> CampgroundIdIsExists(int newCampgroundId)
        {
            _logger.LogInformation("Verifying the ID is valid...");
            
            var campground = await _campgroundRepo.GetCampgroundByIdAsync(newCampgroundId);

            var isValidId = false;

            if(isValidId != true)
            {
                _logger.LogError("ID invalid...No campground found...");
                throw new Exception($"Campground with ID: {newCampgroundId}, does not exist.");
            }

            _logger.LogInformation($"ID: {newCampgroundId} is valid!");

            return isValidId;
        }

        public async Task<bool> IsCampgroundUpdated(Campground originalCampground, Campground updatedCampground)
        {
            _logger.LogInformation("Checking if campground has been updated...");

            var result = originalCampground.Equals(updatedCampground);

            if(result == true)
            {
                _logger.LogError("Campground unsuccessfully updated...");
                throw new Exception("Campground failed to update. Objects are the same.");
            }

            _logger.LogInformation("Campground has been updated...");

            return result;
        }
    }
}