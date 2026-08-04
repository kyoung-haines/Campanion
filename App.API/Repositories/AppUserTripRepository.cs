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

        public async Task<IEnumerable<AppUserTrip>> RetrieveAllAppUserTripsByUserIdAsync(AppUser appUser)
        {
            _logger.LogInformation("AppUserTripRepository method called: RetrieveAllTripsByUserIdAsync...");
            _logger.LogInformation($"Attempting to retrieve all trips for user {appUser.Id}...");

            try
            {
                var allTrips = await _context.AppUserTrips.ToListAsync();

                var allTripsByUserId = allTrips.Where<AppUserTrip>(trip => trip.AppUserId == appUser.Id);

                return allTripsByUserId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve trips for user: {appUser.Id}...");
                throw;
            }
        }
    }
}
