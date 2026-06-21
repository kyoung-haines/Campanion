using App.API.Dtos.Trips.TripsDtos;
using App.API.Models.Trips;
using App.API.Repositories;

namespace App.API.Services
{
    public class TripService : ITripService
    {
        private ILogger _logger;
        private ITripRepository _tripRepository;

        public TripService(ILogger logger, ITripRepository tripRepoository)
        {
            _logger = logger;
            _tripRepository = tripRepoository;
        }

        public async Task<Result<List<TripDto>>> GetAllTripsAsync()
        {
            TripDto tripDto = new TripDto();
            List<TripDto> tripDtoList = new ();

            try
            {
                _logger.LogInformation("TripService method called: GetAllTripsAsync...");

                var tripsResult = await _tripRepository.GetAllTripsAsync();

                if (tripsResult.Data == null || tripsResult.Data.Count() == 0)
                {
                    _logger.LogWarning("There are no trips to return. The list is empty. If there are known trips saved, this is an error...");
                }

                foreach (var trip in tripsResult.Data)
                {
                    tripDto.TripId = trip.TripId;
                    tripDto.TripName = trip.TripName;
                    tripDto.TripStatus = Convert.ToString(trip.TripStatus);
                    tripDto.TripStartDate = Convert.ToString(trip.TripStartDate);
                    tripDto.TripEndDate = Convert.ToString(trip.TripEndDate);
                    tripDto.TripCreationDate = Convert.ToString(trip.TripCreationDate);

                    foreach (var attendee in trip.TripAttendees)
                    {
                        var attendeeName = attendee.AppUserFirstName + attendee.AppUserLastName;
                        tripDto.TripAttendees.Add(attendeeName);
                    }
                }
                
                return Result<List<TripDto>>.Success(tripDtoList);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve all trips: {DateTime.Now}");
                return Result<List<TripDto>>.Failure($"Failed to retrieve all trips. " + ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteTripAsync(int tripId)
        {
            try
            {
                _logger.LogInformation("TripService method called: DeleteTripAsync...");

                var deleteResult = await _tripRepository.DeleteTripAsync(tripId);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete trip with ID: {tripId}...");

                return Result<bool>.Failure("Failed to delete the trip. Please try again.");
            }
        }
        public async Task<Result<TripDto>> CreateTripAsync(Trip trip)
        {
            TripDto tripDto = new TripDto(trip);

            try
            {
                _logger.LogInformation("TripService method called: CreateTripAsync...");

                var createResult = await _tripRepository.CreateTripAsync(trip);

                return Result<TripDto>.Success(tripDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to create new trip with ID: {trip.TripId}..."); ;
                return Result<TripDto>.Failure("Failed to create the trip. Please try again.");
            }
        }
        public async Task<Result<Trip>> UpdateTripAsync(int tripId)
        {
            var trip = new Trip();
            return Result<Trip>.Success(trip);
        }
    }
}
