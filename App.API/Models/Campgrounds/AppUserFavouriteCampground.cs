using Microsoft.EntityFrameworkCore;

namespace App.API.Models.Campgrounds
{
    [PrimaryKey(nameof(AppUserId), nameof(CampgroundId))]
    public class AppUserFavouriteCampground
    {
        public int AppUserId { get; set; }
        public int CampgroundId { get; set; }
        public DateTime FavouritedAt { get; set; }
    }
}
