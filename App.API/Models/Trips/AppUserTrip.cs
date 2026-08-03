using App.API.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.API.Models.Trips
{
    [PrimaryKey(nameof(TripId), nameof(AppUserId))]
    public class AppUserTrip
    {
        public int TripId { get; set; }
        public string AppUserId { get; set; }
        public DateTime AppUserTripeAddedAt { get; set; } = DateTime.Now;

        // NAVIGATIONAL PROPERTIES
        public AppUser AppUser { get; set; }
        public Trip Trip { get; set; }

        public AppUserTrip()
        {

        }

        public AppUserTrip(AppUser appUser, Trip trip)
        {
            TripId = trip.TripId;
            AppUserId = appUser.Id;
        }
    }
}
