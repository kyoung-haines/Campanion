using App.API.Models.Identity;
using Campanion.Shared.Dtos.AppUserDtos;
using Microsoft.EntityFrameworkCore;

namespace App.API.Models.Campgrounds
{
    [PrimaryKey(nameof(AppUserId), nameof(CampgroundId))]
    public class AppUserFavouriteCampground
    {
        public string AppUserId { get; set; }
        public int CampgroundId { get; set; }
        public DateTime FavouritedAt { get; set; } = DateTime.Now;

        // Navigational Properties
        public AppUser AppUser { get; set; }
        public Campground Campground { get; set; }

        public AppUserFavouriteCampgroundDto ToDto(AppUserFavouriteCampground favCampground)
        {
             
            var appUserFavouriteCampgroundDto = new AppUserFavouriteCampgroundDto
            {
                AppUserId = this.AppUserId,
                CampgroundId = this.CampgroundId.ToString(),
                CampgroundName = this.Campground.CampgroundName
            };

            return appUserFavouriteCampgroundDto;
        }
    }
}
