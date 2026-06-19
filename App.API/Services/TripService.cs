using App.API.Models.Trips;
using App.API.Repositories;

namespace App.API.Services
{
    public class TripService : ITripService
    {
        private ILogger _logger;
        private ITripRepository _tripRepository;

        public TripService(ILogger logger, ITripRepository tripRepoository)
        {
            _logger = logger;
            _tripRepository = tripRepoository;
        }

        public async Task<Result<List<Trip>>> GetAllTripsAsync()
        {
            try
            {
                _logger.LogInformation("TripService method called: GetAllTripsAsync...");
                _logger.LogInformation("Attempting to retrieve all trips for all users...");

                var tripsResult = await _tripRepository.GetAllTripsAsync();
                
                if (tripsResult.Data == null || tripsResult.Data.Count() == 0)
                {
                    _logger.LogWarning("There are no trips to return. The list is empty. If there are known trips saved, this is an error...");
                }

                return Result<List<Trip>>.Success(tripsResult.Data);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve all trips: {DateTime.Now}");
                return Result<List<Trip>>.Failure($"Failed to retrieve all trips. " + ex.Message);
            }
        }
    }
}
