using App.API.Enums;
namespace App.API.Models.Trips
{
    public class Trip
    {
        public int TripId { get; set; }
        public TripStatus TripStatus { get; set; }
        public DateTime TripStartDate { get; set; }
        public DateTime TripEndDate { get; set; }
        public DateTime TripCreationDate { get; set; }
    }
}
