using App.API.Models.Identity;
using App.API.Models.Trips;
using App.API.Repositories;

namespace App.API.Services
{
    public class AppUserTripService : IAppUserTripService
    {
        private readonly IAppUserTripRepository _tripRepository;
        private readonly ILogger<AppUserTripService> _logger;

        public async Task<Result<AppUserTrip>> AddNewAppUserTripAsync(AppUser appUser, Trip trip)
        {
            try
            {
                _logger.LogInformation("AppUserTripService method called: AddNewTripAsync...");
                _logger.LogInformation($"Attempting to add a new app user trip to the database for user: {appUser.Id} and trip: {trip.TripId}...");

                var appUserTrip = await _tripRepository.AddNewAppUserTripAsync(appUser, trip);

                return Result<AppUserTrip>.Success(appUserTrip);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add new app user trip to the database...");
                return Result<AppUserTrip>.Failure("Failed to add user trip to the database. Please try again.");
            }
           
        }

        public async Task<Result<IEnumerable<AppUserTrip>>> RetrieveAllAppUserTripsByUserIdAsync(AppUser appUser)
        {
            _logger.LogInformation("AppUserTripService method called: RetrieveAllAppUserTripsByUserIdAsync...");
            _logger.LogInformation($"Attempting to retrieve all trips for user: {appUser.Id}...");

            try
            {
                var appUserTrips = await _tripRepository.RetrieveAllAppUserTripsByUserIdAsync(appUser);

                return Result<IEnumerable<AppUserTrip>>.Success(appUserTrips);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to retrieve trips for user: {appUser.Id}...");
                return Result<IEnumerable<AppUserTrip>>.Failure("Failed to retrieve trips. Please try again.");
            }
        }
    }
}
