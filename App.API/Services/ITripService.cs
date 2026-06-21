using App.API.Dtos.Trips.TripsDtos;
using App.API.Models.Trips;

namespace App.API.Services
{
    public interface ITripService
    {
        Task<Result<List<TripDto>>> GetAllTripsAsync();
        Task<Result<bool>> DeleteTripAsync(int tripId);
        Task<Result<TripDto>> CreateTripAsync(Trip trip);
        Task<Result<TripDto>> UpdateTripAsync(int tripId);
    }
}
