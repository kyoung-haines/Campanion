using Campanion.Shared.Dtos.TripDtos;
using App.API.Enums;
using App.API.Models.Identity;
namespace App.API.Models.Trips
{
    public class Trip
    {
        public int TripId { get; set; }
        public string? TripName { get; set; } = $"Created: {DateTime.Now}";
        public TripStatus TripStatus { get; set; } = Enums.TripStatus.DRAFT;
        public DateTime TripStartDate { get; set; }
        public DateTime TripEndDate { get; set; }
        public DateTime TripCreationDate { get; set; }
        public List<AppUser> TripAttendees { get; set; } = new List<AppUser>();

        // HELPERS
        public async Task<TripDto> ConvertTripObjecToTripDtoAsync(Trip trip)
        {
            var tripDto = new TripDto
            {
                TripId = Convert.ToString(trip.TripId),
                TripName = trip.TripName,
                TripStatus = Convert.ToString(trip.TripStatus),
                TripStartDate = trip.TripStartDate.ToString(),
                TripEndDate = trip.TripEndDate.ToString(),
                TripCreationDate = trip.TripCreationDate.ToString(),
                TripAttendees = null
            };

            foreach (var attendee in trip.TripAttendees)
            {
                var tripAttendee = attendee.AppUserFirstName + attendee.AppUserLastName;
                tripDto.TripAttendees.Add(tripAttendee);
            }

            return tripDto;
        }
    }    
}