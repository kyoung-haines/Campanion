using Campanion.Shared.Dtos.AppUserDtos;
using App.API.Models.Identity;
using App.API.Models.Trips;
using App.API.Repositories;

namespace App.API.Services
{
    public class AppUserTripService : IAppUserTripService
    {
        private readonly IAppUserTripRepository _appUserTripRepository;
        private readonly ILogger<IAppUserTripService> _logger;
        private readonly IAppUserService _appUserService;
        private readonly ITripService _tripService;
        public AppUserTripService(IAppUserTripRepository tripRepo, ILogger<IAppUserTripService> logger, IAppUserService appUserService, ITripService tripService)
        {
            _appUserTripRepository = tripRepo;
            _logger = logger;
            _appUserService = appUserService;
            _tripService = tripService;
        }

        public async Task<Result<AppUserTripDto>> AddNewAppUserTripAsync(string appUserId, int tripId)
        {
            try
            {
                _logger.LogInformation("AppUserTripService method called: AddNewTripAsync...");
                _logger.LogInformation($"Attempting to add a new app user trip to the database for user: {appUserId} and trip: {tripId}...");

                var appUserTrip = new AppUserTrip
                {
                    AppUserId = appUserId,
                    TripId = tripId
                };

                var appUserTripDto = await appUserTrip.AppUserTripToDTOAsync(appUserTrip);

                return Result<AppUserTripDto>.Success(appUserTripDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add new app user trip to the database...");
                return Result<AppUserTripDto>.Failure("Failed to add user trip to the database. Please try again.");
            }
           
        }

        public async Task<Result<IEnumerable<AppUserTripDto>>> RetrieveAllAppUserTripsByUserIdAsync(string appUserId)
        {
            try
            {
                _logger.LogInformation("AppUserTripService method called: RetrieveAllAppUserTripsByUserIdAsync...");
                
                var appUserResult = await _appUserService.GetAppUserByIdAsync(appUserId);
                var appUser = appUserResult.Data;

                _logger.LogInformation($"Attempting to retrieve all trips for user: {appUser.Id}...");

                var appUserTrips = await _appUserTripRepository.RetrieveAllAppUserTripsByUserIdAsync(appUserId);
                List<AppUserTripDto> appUserTripsDto = new();

                foreach(var trip in appUserTrips)
                {
                    AppUserTrip appUserTrip = new();
                    var appUserTripDto = await appUserTrip.AppUserTripToDTOAsync(appUserTrip);
                    appUserTripsDto.Add(appUserTripDto);
                }
                return Result<IEnumerable<AppUserTripDto>>.Success(appUserTripsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to retrieve trips for user: {appUserId}...");
                return Result<IEnumerable<AppUserTripDto>>.Failure("Failed to retrieve trips. Please try again.");
            }
        }
    }
}
