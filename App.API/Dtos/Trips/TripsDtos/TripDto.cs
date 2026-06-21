using App.API.Enums;
using App.API.Models.Identity;
using App.API.Models.Trips;

namespace App.API.Dtos.Trips.TripsDtos
{
    public class TripDto
    {

        public int TripId { get; set; }
        public string? TripName { get; set; }
        public string TripStatus { get; set; }
        public string TripStartDate { get; set; }
        public string TripEndDate { get; set; }
        public string TripCreationDate { get; set; } = Convert.ToString(DateTime.Now);
        public List<string> TripAttendees { get; set; } = new List<string>();
    }
}
