using App.API.Data;
using App.API.Models.Trips;
using Microsoft.EntityFrameworkCore;

namespace App.API.Repositories
{
    public class TripRepository : ITripRepository
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

        public async Task<Result<bool>> DeleteTripAsync(int tripId)
        {
            try
            {
                _logger.LogInformation("TripRepository method called: DeleteTripAsync...");
                _logger.LogInformation($"Attempting to retrieve trip with ID: {tripId}...");

                var tripResult = await _context.FindAsync<Trip>(tripId);

                if (tripResult == null)
                {
                    _logger.LogWarning($"No trip with ID: {tripId} found. Check ID value...");
                }

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete trip with ID: {tripId}...");
                return Result<bool>.Failure("Failed to delete the trip from the database." + ex.Message);
            }
        }

        public async Task<Result<Trip>> CreateTripAsync(Trip trip)
        {
            try
            {
                _logger.LogInformation("TripRepository method called: CreateTripAsync...");
                _logger.LogInformation($"Attempring to create a new trip with ID: {trip.TripId}");

                if (trip == null)
                {
                    _logger.LogError("Failed to create the trip. The trip is empty...");
                    return Result<Trip>.Failure("Failed to create the trip. Please try again.");

                }

                return Result<Trip>.Success(trip);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to create new trip with ID: {trip.TripId}...");
                return Result<Trip>.Failure("Failed to create the trip: " + ex.Message);
            }
        }
    }
}
