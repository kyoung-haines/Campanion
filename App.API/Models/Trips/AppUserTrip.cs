using Microsoft.EntityFrameworkCore;

namespace App.API.Models.Trips
{
    [PrimaryKey(nameof(TripId), nameof(AppUserId))]
    public class AppUserTrip
    {
        public int TripId { get; set; }
        public int AppUserId { get; set; }
        public DateOnly AppUserTripeAddedAt { get; set; }
    }
}
