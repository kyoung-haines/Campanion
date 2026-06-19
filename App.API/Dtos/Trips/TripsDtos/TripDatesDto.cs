using App.API.Models.Trips;

namespace App.API.Dtos.Trips.TripsDtos
{
    public class TripDatesDto
    {
        private int _tripId;
        private DateTime _tripStartDate;
        private DateTime _tripEndDate;

        public TripDatesDto(Trip trip)
        {
            _tripId = trip.TripId;
            _tripStartDate = trip.TripStartDate;
            _tripEndDate = trip.TripEndDate;
        }
    }
}
