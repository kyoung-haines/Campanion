using App.API.Models.Trips;

namespace App.API.Repositories
{
    public interface ITripRepository
    {
        Task<List<Trip>> GetAllTripsAsync();
        //REMOVE - MOVE TO AppUserTrip repository layer when created
        //Task<Result<List<Trip>>> GetTripsByUserIdAsync(int userId);
        Task<Trip> UpdateTripAsync(int tripId);
        Task<bool> DeleteTripAsync(int tripId);
        //REMOVE - MOVE TO AppUserTrip repository layer when created
        //Task<Result<bool>> DeleteAllTripsByUserId(int userId);
        Task<Trip> CreateTripAsync(Trip trip);
        Task<Trip> GetTripByTripIdAsync(int tripId);
    }
}
