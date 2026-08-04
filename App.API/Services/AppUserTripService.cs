using Campanion.Shared.Dtos.TripDtos;
using App.API.Models.Identity;
using App.API.Models.Trips;
using App.API.Repositories;

namespace App.API.Services
{
    public class AppUserTripService : IAppUserTripService
    {
        private readonly IAppUserTripRepository _tripRepository;
        private readonly ILogger<IAppUserTripService> _logger;
        private readonly IAppUserService _appUserService;
        private readonly ITripService _tripService;
        public AppUserTripService(IAppUserTripRepository tripRepo, ILogger<IAppUserTripService> logger, IAppUserService appUserService, ITripService tripService)
        {
            _tripRepository = tripRepo;
            _logger = logger;
            _appUserService = appUserService;
            _tripService = tripService;
        }

        public async Task<Result<AppUserTrip>> AddNewAppUserTripAsync(string appUserId, int tripId)
        {
            TripDto trip = null;
            AppUser appUser = null;

            try
            {
                _logger.LogInformation("AppUserTripService method called: AddNewTripAsync...");
                _logger.LogInformation($"Attempting to add a new app user trip to the database for user: {appUser.Id} and trip: {trip.TripId}...");

                var tripResult = await _tripService.GetTripByIdAsync(tripId);
                trip = tripResult.Data;

                var appUserTrip = await _tripRepository.AddNewAppUserTripAsync(appUser, trip);

                return Result<AppUserTrip>.Success(appUserTrip);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add new app user trip to the database...");
                return Result<AppUserTrip>.Failure("Failed to add user trip to the database. Please try again.");
            }
           
        }

        public async Task<Result<IEnumerable<AppUserTrip>>> RetrieveAllAppUserTripsByUserIdAsync(string appUserId)
        {
            AppUser appUser = null;
            
            try
            {
                _logger.LogInformation("AppUserTripService method called: RetrieveAllAppUserTripsByUserIdAsync...");
                
                var appUserResult = await _appUserService.GetAppUserByIdAsync(appUserId);
                appUser = appUserResult.Data;

                _logger.LogInformation($"Attempting to retrieve all trips for user: {appUser.Id}...");

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
