using App.API.Data;
using App.API.Models.Identity;
using App.API.Models.Trips;
using Microsoft.EntityFrameworkCore;

namespace App.API.Repositories
{
    public class AppUserTripRepository : IAppUserTripRepository
    {
        private readonly CampanionDbContext _context;
        private readonly ILogger<AppUserTripRepository> _logger;

        public AppUserTripRepository(CampanionDbContext context, ILogger<AppUserTripRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AppUserTrip> AddNewAppUserTripAsync(AppUser appUser, Trip trip)
        {
            _logger.LogInformation("AppUserTripRepository method called: AddNewTripAsync...");
            _logger.LogInformation("Attempting to add a new trip to the database...");

            try
            {
                var appUserTrip = new AppUserTrip(appUser, trip); 

                await _context.AppUserTrips.AddAsync(appUserTrip);

                await _context.SaveChangesAsync();

                return appUserTrip;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to add new trip for user {appUser.Id}...");
                throw;
            }
        }

        public async Task<IEnumerable<AppUserTrip>> RetrieveAllAppUserTripsByUserIdAsync(string appUserId)
        {
            _logger.LogInformation("AppUserTripRepository method called: RetrieveAllTripsByUserIdAsync...");
            _logger.LogInformation($"Attempting to retrieve all trips for user {appUserId}...");

            try
            {
                var allTrips = await _context.AppUserTrips.ToListAsync();

                if (allTrips.Count == 0)
                {
                    _logger.LogInformation("Trips list is empty. Either no user has saved any trips, or this is an error...");
                    return allTrips;
                }

                var allTripsByUserId = allTrips.Where<AppUserTrip>(trip => trip.AppUserId == appUserId).ToList<AppUserTrip>();

                if (allTripsByUserId.Count == 0)
                {
                    _logger.LogInformation($"User trips list is empty, or there was an error...");
                }

                return allTripsByUserId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve trips for user: {appUserId}...");
                throw;
            }
        }

        public async Task<AppUserTrip> UpdateAppUserTripAsync(AppUserTrip appUserTrip)
        {
            try
            {
                _logger.LogInformation("AppUserTripRepository method called: UpdateAppUserTripAsync...");
                _logger.LogInformation($"Attempting to update trip {appUserTrip.TripId}...");

                _context.Update<AppUserTrip>(appUserTrip);

                await _context.SaveChangesAsync();

                return appUserTrip;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update user's trip...");
                throw;
            }
        }

        public async Task DeleteAppUserTripAsync(int tripId)
        {
            try
            {
                _logger.LogInformation("AppUserTripRepository method called: DeleteAppUserTripAsync...");
                _logger.LogInformation($"Attempting to delete trip: {tripId}...");

                var appUserTrip = await _context.FindAsync<AppUserTrip>(tripId);

                if (appUserTrip == null)
                {
                    _logger.LogInformation("AppUserTrip is null. This trip likely doesn't exist. Check the TripId value exists...");
                    return;
                }

                var removeAction = _context.Remove<AppUserTrip>(appUserTrip);

                var deletionConfirmation = await _context.FindAsync<AppUserTrip>(tripId);

                if (deletionConfirmation == null)
                {
                    _logger.LogInformation($"Trip {tripId} has been successfully deleted...");
                }

                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete trip: {tripId}...");
                throw;
            }
        }
    }
}
