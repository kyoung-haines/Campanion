using App.API.Exceptions.CampgroundExceptions;
using App.API.Exceptions.RepositoryExceptions;
using App.API.Models.Campgrounds;
using App.API.Repositories;
using Campanion.Shared.Dtos.CampgroundDtos;

namespace App.API.Services
{
    public class CampgroundService : ICampgroundService
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
                _logger.LogInformation("CampgroundService method called: DeleteCampgroundAsync...");
                _logger.LogInformation($"Attempting to delete campground with ID: {id}");

                // Ensure the campground exists (repository may throw for invalid id)
                var campground = await _campgroundRepo.GetCampgroundByIdAsync(id);

                // Attempt delete via repository and return actual delete result
                var deleteSucceeded = await _campgroundRepo.DeleteCampgroundAsync(id);

                if (deleteSucceeded)
                {
                    _logger.LogInformation($"Campground with ID: {id} deleted...");
                    return Result<bool>.Success(true);
                }

                _logger.LogWarning($"Repository reported failure deleting campground with ID: {id}.");
                return Result<bool>.Failure("Failed to delete the campground.");
            }
            catch (InvalidCampgroundIdException ex)
            {
                _logger.LogError(ex, $"Invalid campground id: {id} specified for deletion.");
                return Result<bool>.Failure("Campground not found.");
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(ex, $"Error deleting the campground from the system. ID {id}. See Exception for details.");
                return Result<bool>.Failure("Failed to delete the campground from the database.");
            }
        }

        public async Task<Result<List<CampgroundDto>>> GetAllCampgroundsAsync()
        {
            try
            {
                _logger.LogInformation("CampgroundService method called: GetAllCampgroundsAsync...");
                _logger.LogInformation("Service Layer: GetAllCampgroundsAsync called...");

                var campgrounds = await _campgroundRepo.GetAllCampgroundsAsync();

                var campgroundDtosList = new List<CampgroundDto>();

                foreach (var campground in campgrounds)
                {
                    var campgroudDto = 
                }

                Result<List<CampgroundDto>> campgroundResults = Result<List<CampgroundDto>>.Success(campgrounds.ToList<Campground>());

                return campgroundResults;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all campgrounds. See Exception.");
                return Result<List<CampgroundDto>>.Failure("Failed to retrieve all campgrounds.");
            }
        }

        public async Task<Result<CampgroundDto>> GetCampgroundByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("CampgroundService method called: GetCampgroundByIdAsync...");
                _logger.LogInformation($"Attempting to retrieve Campground with ID: {id}...");
                var campground = await _campgroundRepo.GetCampgroundByIdAsync(id);

                Result<CampgroundDto> campgroundResult = Result<CampgroundDto>.Success(campground);

                return campgroundResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving campground with ID: {id}. See exception for details.");
                return Result<CampgroundDto>.Failure("Failed to retrieve the campground.");
            }
        }

        public async Task<Result<CampgroundDto>> UpdateCampgroundAsync(CampgroundDto originalCampground)
        {
            try
            {
                _logger.LogInformation("CampgroundService method called: UpdateCampgroundAsync...");
                _logger.LogInformation($"Attempting to update campground with ID: {originalCampground.CampgroundId}...");

                var updatedCampground = await _campgroundRepo.UpdateCampgroundAsync(originalCampground);
                var updatedResult = Result<CampgroundDto>.Success(updatedCampground);

                return updatedResult;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update campground. See Exception");
                return Result<CampgroundDto>.Failure("Failed to update the campground.");
            }
        }

        public async Task<Result<CampgroundDto>> AddCampgroundAsync(CampgroundDto newCampground)
        {
            try
            {
                _logger.LogInformation("CampgroundService method called: AddCampgroundAsync...");
                _logger.LogInformation($"Attempting to add new campground with ID: {newCampground.CampgroundId}...");
                _logger.LogInformation($"Checking if the campground already exists in the system...");

                var isExists = await CampgroundIdIsExistsAsync(newCampground.CampgroundId);

                if(isExists.Succeeded == false)
                {
                    _logger.LogInformation("Adding campground to the system...");
                    var result = await _campgroundRepo.AddCampgroundAsync(newCampground);
                }

                var isCampgroundAdded = await IsCampgroundAddedAsync(newCampground.CampgroundId);

                if(isCampgroundAdded.Data == true)
                {
                    _logger.LogInformation("Campground has been added to the system...");
                }

                return Result<CampgroundDto>.Success(newCampground);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error adding campground. See exception.");
                return Result<CampgroundDto>.Failure("Failed to add new campground.");
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
                _logger.LogInformation("CampgroundService method called: CampgroundIdIsExistsAsync...");
                _logger.LogInformation($"Verifying ID: {newCampgroundId} is valid...");

                var campgroundResult = await _campgroundRepo.GetCampgroundByIdAsync(newCampgroundId);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Campground with ID: {newCampgroundId} does not exist...");
                return Result<bool>.Failure($"Campground with ID: {newCampgroundId} does not exist.");
            }
        }

        public async Task<Result<bool>> IsCampgroundUpdatedAsync(CampgroundDto originalCampground, CampgroundDto updatedCampground)
        {
            try
            {
                _logger.LogInformation("CampgroundService method called: IsCampgroundUpdatedAsync...");
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

        public async Task<Result<bool>> IsCampgroundAddedAsync(int id)
        {
            var result = new Result<bool>();

            try
            {
                _logger.LogInformation("CampgroundService method called: IsCampgroundAddedAsync...");
                result = await CampgroundIdIsExistsAsync(id);

                if(result.Succeeded == true)
                {
                    _logger.LogInformation($"Campground with ID: {id} has been added to the system...");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Campground with ID: {id} has not been added to the system...");
                return result;
            }
        }
    }
}