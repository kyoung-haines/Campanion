using App.API.Enums;
using App.API.Models.Identity;
using App.API.Models.Trips;
using Campanion.Shared.Dtos.TripDtos;

namespace App.API.Dtos.Trips.TripsDtos
{
    public class TripDto
    {

        public int? TripId { get; set; }
        public string? TripName { get; set; }
        public string TripStatus { get; set; }
        public string TripStartDate { get; set; }
        public string TripEndDate { get; set; }
        public string TripCreationDate { get; set; } = Convert.ToString(DateTime.Now);
        public List<string>? TripAttendees { get; set; } = new List<string>();

        public TripDto() { }

        public TripDto(Trip trip)
        {
            TripId = trip.TripId;
            TripName = trip.TripName;
            TripStatus = Convert.ToString(trip.TripStatus);
            TripStartDate = Convert.ToString(trip.TripStartDate);
            TripEndDate = Convert.ToString(trip.TripEndDate);
            TripCreationDate = Convert.ToString(trip.TripCreationDate);

            foreach (var item in trip.TripAttendees)
            {
                var attendeeName = item.AppUserFirstName + item.AppUserLastName;

                TripAttendees.Add(attendeeName);
            }
        }

        public static async Task<TripDto> ConvertCreateTripDtoToTripDto(CreateTripDto createDto)
        {
            var tripDto = new TripDto();

            tripDto.TripName = createDto.TripName;
            tripDto.TripStartDate = Convert.ToString(createDto.TripStartDate);
            tripDto.TripEndDate = Convert.ToString(createDto.TripEndDate);
            tripDto.TripCreationDate = Convert.ToString(DateOnly.FromDateTime(createDto.TripCreatedAt));

            return tripDto;
        }

        // this method needs to be refactored
        // eventually, the ability to add attendees or invite attendees
        // right from the creation screen will be a feature
        // for now TripAttendees at the Trip level are an empty List<AppUser>
        public static async Task<Trip> ConvertTripDtoToTrip(TripDto tripDto)
        {
            Trip trip = new Trip
            {
                TripName = tripDto.TripName,
                TripStartDate = Convert.ToDateTime(tripDto.TripStartDate),
                TripEndDate = Convert.ToDateTime(tripDto.TripEndDate),
                TripCreationDate = Convert.ToDateTime(tripDto.TripCreationDate)
            };

            return trip;
        }
    }
}
