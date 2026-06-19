using App.API.Models.Trips;

namespace App.API.Services
{
    public interface ITripService
    {
        Task<Result<List<Trip>>> GetAllTripsAsync();
        Task<Result<bool>> DeleteTripAsync(int tripId);
        Task<Result<Trip>> CreateTripAsync(Trip trip);
        Task<Result<Trip>> UpdateTripAsync(int tripId);
    }
}
