using App.API.Data;
using App.API.Models.Trips;
using Microsoft.EntityFrameworkCore;

namespace App.API.Repositories
{
    public class TripRepository
    {
        private ILogger<TripRepository> _logger;
        private CampanionDbContext _context;

        public TripRepository(ILogger<TripRepository> logger, CampanionDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<List<Trip>>> GetAllTripsAsync()
        {
            try
            {
                _logger.LogInformation("TripRepository method called: GetAllTripsAsync...");
                _logger.LogInformation("Attempting to retrieve all trips from the database...");

                var allTrips = await _context.Trips.ToListAsync<Trip>();

                if (allTrips == null || allTrips.Count() == 0)
                {
                    _logger.LogWarning("No trips found in the database...");
                    _logger.LogWarning("Returning an empty list...");
                }

                _logger.LogInformation("Successfully retrieved all trips from the database...");

                return Result<List<Trip>>.Success(allTrips);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all trips from the database...");
                return Result<List<Trip>>.Failure("Failed to retrieve trips from the database. Please try again.");
            }
        }

        public async Task<Result<Trip>> UpdateTripAsync(int tripId)
        {
            try
            {
                _logger.LogInformation("TripRepository method called: UpdateTripAsync...");
                _logger.LogInformation($"Attempting to update trip with ID: {tripId}...");

                var trip = await _context.Trips.FindAsync(tripId);

                if (trip == null)
                {
                    _logger.LogWarning($"No trip with ID: {tripId} found. Check ID value...");
                }

                return Result<Trip>.Success(trip);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update trip with ID: {tripId}...");

                return Result<Trip>.Failure("Failed to update the trip. Please try again.");
            }
        }
    }
}
