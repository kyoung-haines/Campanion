using App.API.Models.Campgrounds;
using App.API.Repositories;
using System.Security.Cryptography.X509Certificates;

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

                var isExists = await CampgroundIdIsExistsAsync(id);

                if(isExists == true)
                {
                    await _campgroundRepo.DeleteCampground(id);
                    _logger.LogInformation($"Campground deleted from the system. ID: {id}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error deleting the campground from the system. ID {id}. See Exception for details.");
                throw new Exception (e.Message);
            }
        }

        public async Task<IEnumerable<Campground>> GetAllCampgroundsAsync()
        {
            try
            {
                _logger.LogInformation("Service Layer: GetAllCampgroundsAsync called...");

                var campgrounds = await _campgroundRepo.GetAllCampgroundsAsync();

                if(campgrounds != null)
                {
                    _logger.LogInformation("Campgrounds retrieved successfully!");
                    
                }

                return campgrounds;
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving all campgrounds. See Exception.");
                throw new Exception(e.Message);
            }
        }

        // QUICK COMMENT - needs doc comments still
        // This will reference a given CampgroundId (ID of newly added campground)
        // against all campgrounds to ensure that it has been added to the system.
        // uses Repo layer FindCampgroundByIdAsync method
        public async Task<bool> CampgroundIdIsExistsAsync(int newCampgroundId)
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

        public async Task<bool> IsCampgroundUpdatedAsync(Campground originalCampground, Campground updatedCampground)
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

        public async Task<bool> IsCampgroundAddedAsync(int id)
        {
            _logger.LogInformation("Verifying campground has been added...");

            var result = await CampgroundIdIsExistsAsync(id);

            if(result != true)
            {
                _logger.LogError($"ID: {id} not in the system. Campground not added.");
                throw new Exception("Campground not added to the system. Please try again.");
            }

            _logger.LogInformation($"ID: {id} - Campground Added!");

            return result;
        }
    }
}