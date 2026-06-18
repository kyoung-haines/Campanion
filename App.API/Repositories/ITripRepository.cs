using App.API.Models.Trips;

namespace App.API.Repositories
{
    public interface ITripRepository
    {
        Task<Result<List<Trip>>> GetAllTripsAsync();
        Task<Result<List<Trip>>> GetTripsByUserIdAsync(int userId);
        Task<Result<Trip>> UpdateTripAsync(int tripId);
        Task<Result<bool>> DeleteTripAsync(int tripId);
        Task<Result<bool>> DeleteAllTripsByUserId();
        Task<Result<Trip>> CreateTripAsync(Trip trip);
    }
}
