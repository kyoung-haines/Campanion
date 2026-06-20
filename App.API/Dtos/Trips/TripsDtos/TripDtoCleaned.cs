using App.API.Models.Trips;

namespace App.API.Dtos.Trips.TripsDtos
{
    public class TripDtoCleaned
    {
        private readonly int _tripId;
        private readonly string? _tripName;
        private readonly string _tripStatus;
        private readonly string _tripStartDate;
        private readonly string _tripEndDate;
        private readonly string _tripCreationDate;
        private readonly List<string> _tripAttendees = new List<string>();

        public TripDtoCleaned(Trip trip)
        {
            _tripName = trip.TripName;
            _tripStatus = Convert.ToString(trip.TripStatus);
            _tripStartDate = Convert.ToString(trip.TripStartDate);
            _tripEndDate = Convert.ToString(trip.TripEndDate);
            _tripCreationDate = Convert.ToString(trip.TripCreationDate);

            foreach (var user in trip.TripAttendees)
            {
                var attendeeName = user.AppUserFirstName + " " + user.AppUserLastName;
                _tripAttendees.Add(attendeeName);
            }
        }
    }
}
