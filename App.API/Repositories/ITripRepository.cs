using App.API.Models.Trips;

namespace App.API.Repositories
{
    public interface ITripRepository
    {
        Task<Result<List<Trip>>> GetAllTripsAsync();
        //REMOVE - MOVE TO AppUserTrip repository layer when created
        //Task<Result<List<Trip>>> GetTripsByUserIdAsync(int userId);
        Task<Result<Trip>> UpdateTripAsync(int tripId);
        Task<Result<bool>> DeleteTripAsync(int tripId);
        //REMOVE - MOVE TO AppUserTrip repository layer when created
        //Task<Result<bool>> DeleteAllTripsByUserId(int userId);
        Task<Result<Trip>> CreateTripAsync(Trip trip);
    }
}
