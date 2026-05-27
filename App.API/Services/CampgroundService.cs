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

        public async Task<Result<bool>> DeleteCampgroundAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete campground with ID: {id}");

                var campground = await _campgroundRepo.GetCampgroundByIdAsync(id);

                if(!campground.Succeeded || campground.Data == null)
                {
                    _logger.LogWarning($"Campground with ID: {id} not found...");
                    return Result<bool>.Failure("Campground not found...");
                }

                var result = await _campgroundRepo.DeleteCampgroundAsync(id);

                if(!result.Succeeded)
                {
                    _logger.LogError($"Failed to delete campground with ID: {id}...");
                    return Result<bool>.Failure(result.Error.ToString());
                }

                _logger.LogInformation($"Campground with ID: {id} deleted...");
                return Result<bool>.Success(true);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(ex, $"Error deleting the campground from the system. ID {id}. See Exception for details.");
                return Result<bool>.Failure("Failed to delete the campground from the database.");
            }
        }

        public async Task<Result<List<Campground>>> GetAllCampgroundsAsync()
        {
            var campgroundsResult = new Result<List<Campground>>();

            try
            {
                _logger.LogInformation("Service Layer: GetAllCampgroundsAsync called...");

                campgroundsResult = await _campgroundRepo.GetAllCampgroundsAsync();

                return campgroundsResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all campgrounds. See Exception.");
                return campgroundsResult;
            }
        }

        public async Task<Result<Campground>> GetCampgroundByIdAsync(int id)
        {
            var campgroundResult = new Result<Campground>();
            try
            {
                _logger.LogInformation($"Attempting to retrieve Campground with ID: {id}...");
                campgroundResult = await _campgroundRepo.GetCampgroundByIdAsync(id);

                if(campgroundResult == null)
                {
                    _logger.LogError($"Result object is null. Check the ID Value: {id}...");
                }

                if (campgroundResult.Succeeded != true)
                {
                    _logger.LogInformation($"Campground found. Returning campground with ID: {id}...");
                }

                return campgroundResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving campground with ID: {id}. See exception for details.");
                return campgroundResult;
            }
        }

        public async Task<Result<Campground>> UpdateCampgroundAsync(Campground originalCampground)
        {
            var updatedCampground = new Result<Campground>();
            try
            {
                _logger.LogInformation($"Attempting to update campground with ID: {originalCampground.CampgroundId}...");

                if (originalCampground != null)
                {
                    updatedCampground = await _campgroundRepo.UpdateCampgroundAsync(originalCampground);

                    if(updatedCampground.Succeeded == true)
                    {
                        _logger.LogInformation($"Campground updated successfully...");
                    }
                }

                return updatedCampground;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update campground. See Exception");
                return updatedCampground;
            }
        }

        public async Task<Result<Campground>> AddCampgroundAsync(Campground newCampground)
        {
            Result<Campground> result = new Result<Campground>();
            try
            {
                _logger.LogInformation($"Attempting to add new campground with ID: {newCampground.CampgroundId}...");
                _logger.LogInformation($"Checking if the campground already exists in the system...");

                var isExists = await CampgroundIdIsExistsAsync(newCampground.CampgroundId);

                if(isExists.Succeeded == false)
                {
                    _logger.LogInformation("Adding campground to the system...");
                    result = await _campgroundRepo.AddCampgroundAsync(newCampground);
                }

                if(await IsCampgroundAddedAsync(newCampground.CampgroundId) == true)
                {
                    _logger.LogInformation("Campground has been added to the system...");
                }

                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error adding campground. See exception.");
                return result;
            }
        }

        // QUICK COMMENT - needs doc comments still
        // This will reference a given CampgroundId (ID of newly added campground)
        // against all campgrounds to ensure that it has been added to the system.
        // uses Repo layer FindCampgroundByIdAsync method
        public async Task<Result<bool>> CampgroundIdIsExistsAsync(int newCampgroundId)
        {
            try
            {
                _logger.LogInformation($"Verifying ID: {newCampgroundId} is valid...");

                var campgroundResult = await _campgroundRepo.GetCampgroundByIdAsync(newCampgroundId);

                if(campgroundResult.Succeeded == true)
                {
                    _logger.LogInformation($"Campground with ID: {newCampgroundId} exists in the system...");
                }

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Campground with ID: {newCampgroundId} does not exist...");
                return Result<bool>.Failure($"Campground with ID: {newCampgroundId} does not exists.");
            }
        }

        public async Task<Result<bool>> IsCampgroundUpdatedAsync(Campground originalCampground, Campground updatedCampground)
        {
            try
            {
                _logger.LogInformation($"Validating campground with ID: {originalCampground.CampgroundId} has been updated...");
                var isEqual = originalCampground.Equals(updatedCampground);

                if(isEqual == false)
                {
                    _logger.LogInformation($"Campground with ID: {originalCampground.CampgroundId} has been updated...");
                }

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Campground with ID: {originalCampground.CampgroundId} failed to update...");
                return Result<bool>.Failure($"Failed to update campground with ID: {originalCampground.CampgroundId}.");
            }
        }

        public async Task<bool> IsCampgroundAddedAsync(int id)
        {
            var result = new Result<bool>();

            try
            {
                result = await CampgroundIdIsExistsAsync(id);

                if(result.Succeeded == true)
                {
                    _logger.LogInformation($"Campground with ID: {id} has been added to the system...");
                }

                return result.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Campground with ID: {id} has not been added to the system...");
                return result.Succeeded;
            }
        }
    }
}