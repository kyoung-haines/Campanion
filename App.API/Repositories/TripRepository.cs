using App.API.Data;
using App.API.Models.Trips;
using Microsoft.EntityFrameworkCore;
using Campanion.Shared.Dtos.TripDtos;
using App.API.Exceptions.TripExceptions;

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

        public async Task<IEnumerable<Trip>> GetAllTripsAsync()
        {
            try
            {
                _logger.LogInformation("TripRepository method called: GetAllTripsAsync...");
                _logger.LogInformation("Attempting to retrieve all trips from the database...");

                List<Trip> allTrips = await _context.Trips.ToListAsync<Trip>();

                if (allTrips == null)
                {
                    _logger.LogError("Trips list is null...");
                    throw new NullReferenceException("An error occurred. The list of trips returned was null. Please try again.");
                }

                if (allTrips.Count() == 0)
                {
                    _logger.LogWarning("No trips found in the database...");
                    _logger.LogWarning("Returning an empty list...");
                }

                else
                {
                    var totalRecords = allTrips.Count();
                    _logger.LogInformation($"Total of: {totalRecords} retrieved...");
                    _logger.LogInformation("Successfully retrieved all trips. Returning list...");
                }

                return allTrips;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all trips from the database...");
                throw;
            }
        }

        public async Task<Trip> UpdateTripAsync(Trip trip)
        {
            try
            {
                _logger.LogInformation("TripRepository method called: UpdateTripAsync...");
                _logger.LogInformation($"Attempting to update trip with ID: {trip.TripId}...");

                if (trip == null)
                {
                    _logger.LogWarning($"No trip with ID: {trip.TripId} found. Check ID value...");
                }

                _context.Update<Trip>(trip);

                await _context.SaveChangesAsync();

                return trip;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update trip with ID: {trip.TripId}...");
                throw;
            }
        }

        public async Task<bool> DeleteTripAsync(int tripId)
        {
            try
            {
                _logger.LogInformation("TripRepository method called: DeleteTripAsync...");
                _logger.LogInformation($"Attempting to retrieve trip with ID: {tripId}...");

                var tripResult = await _context.FindAsync<Trip>(tripId);

                if (tripResult == null)
                {
                    _logger.LogWarning($"No trip with ID: {tripId} found. Check ID value...");
                    return false;
                }

                _context.Remove<Trip>(tripResult);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete trip with ID: {tripId}...");
                throw;
            }
        }

        public async Task<Trip> CreateTripAsync(Trip trip)
        {
            try
            {
                _logger.LogInformation("TripRepository method called: CreateTripAsync...");
                _logger.LogInformation($"Attempring to create a new trip with ID: {trip.TripId}");
                
                if (trip == null)
                {
                    _logger.LogError("Failed to create the trip. The trip is null...");
                    return trip;

                }

                var addResult = await _context.Trips.AddAsync(trip);

                await _context.SaveChangesAsync();

                return trip;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to create new trip with ID: {trip.TripId}...");
                throw;
            }
        }

        public async Task<Trip> GetTripByTripIdAsync(int tripId)
        {
            try
            {
                _logger.LogInformation("TripRepository method called: GetTripByIdAsync...");
                _logger.LogInformation($"Attempting to retrieve trip with ID: {tripId}...");

                var trip = await _context.FindAsync<Trip>(tripId);

                if (trip == null)
                {
                    // change this to a custom InvalidTripId Exception
                    throw new TripNotFoundException("Trip not found. If possible confirm, the TripId value and try again.");
                }

                return trip;
            }
            catch (Exception ex) when (ex is not TripNotFoundException)
            {
                _logger.LogError(ex, $"Failed to retrieve trip with ID: {tripId}...");
                throw;
            }
        }
    }
}
