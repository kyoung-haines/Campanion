using App.API.Models.Trips;

namespace App.API.Repositories
{
    public interface ITripRepository
    {
        Task<List<Trip>> GetAllTripsAsync();
        Task<Trip> UpdateTripAsync(Trip trip);
        Task<bool> DeleteTripAsync(int tripId);
        Task<Trip> CreateTripAsync(Trip trip);
        Task<Trip> GetTripByTripIdAsync(int tripId);
    }
}
