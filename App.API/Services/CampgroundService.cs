using App.API.Models.Campgrounds;
using App.API.Repositories;
using App.API.Exceptions;

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

        public async Task<Result<Campground>> DeleteCampground(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete campground with ID: {id}");

                var campground = await _campgroundRepo.GetCampgroundByIdAsync(id);

                await _campgroundRepo.DeleteCampgroundAsync(id);

                if(campground == null)
                {
                    _logger.LogInformation($"Campground deleted from the system.");
                }

                return Result<Campground>.Success(campground);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError($"Error deleting the campground from the system. ID {id}. See Exception for details.");
                return Result<Campground>.Failure("Failed to delete the campground from the database.");
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

        public async Task<Campground> GetCampgroundByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to retrieve Campground with ID: {id}...");
                var campground = await _campgroundRepo.GetCampgroundByIdAsync(id);

                if(campground != null)
                {
                    _logger.LogInformation($"Campground found. Returning campground with ID: {id}...");
                }

                return campground;
            }
            catch(Exception e)
            {
                _logger.LogError($"Error retrieving campground with ID: {id}. See exception for details.");
                throw new Exception(e.Message);
            } 
        }

        public async Task UpdateCampgroundAsync(Campground originalCampground)
        {
            try
            {
                _logger.LogInformation($"Attempting to update campground with ID: {originalCampground.CampgroundId}...");
              
                if(originalCampground != null)
                {
                   Campground updatedCampground =  await _campgroundRepo.UpdateCampgroundAsync(originalCampground);

                    if (await IsCampgroundUpdatedAsync(originalCampground, updatedCampground))
                    {
                        _logger.LogInformation("Campground has been updated...");
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogError("Failed to update campground. See Exception");
                throw new Exception(e.Message);
            }
        }

        public async Task AddCampgroundAsync(Campground newCampground)
        {
            try
            {
                _logger.LogInformation($"Attempting to add new campground with ID: {newCampground.CampgroundId}...");
                await _campgroundRepo.AddCampgroundAsync(newCampground);

                var newCampgroundId = newCampground.CampgroundId;

                if(await IsCampgroundAddedAsync(newCampgroundId) == true)
                {
                    _logger.LogInformation("Campground has been added to the system...");
                }
            }
            catch(Exception e)
            {
                _logger.LogError("Error adding campground. See exception.");
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
                throw new Exception("Campground failed to update. No changes were recognized.");
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