using Microsoft.EntityFrameworkCore;

namespace App.API.Models.Trips
{
    [PrimaryKey(nameof(TripId), nameof(CampgroundId))]
    public class TripCampground
    {
        public int TripId { get; set; }
        public int CampgroundId { get; set; }
    }
}
