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
    }
}
