using App.API.Dtos.Trips.TripsDtos;
using App.API.Models.Trips;
using Campanion.Shared.DTOs.Trip;

namespace App.API.Services
{
    public interface ITripService
    {
        Task<Result<List<TripDto>>> GetAllTripsAsync();
        Task<Result<bool>> DeleteTripAsync(int tripId);
        Task<Result<TripDto>> CreateTripAsync(CreateTripDto createTripDto);
        Task<Result<TripDto>> UpdateTripAsync(int tripId);
        Task<Result<TripDto>> GetTripByIdAsync(int tripId);
    }
}
