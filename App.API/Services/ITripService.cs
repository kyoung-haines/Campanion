using Campanion.Shared.Dtos.TripDtos;

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
