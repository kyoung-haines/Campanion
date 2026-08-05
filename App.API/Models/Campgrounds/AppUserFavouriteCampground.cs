using Campanion.Shared.Dtos.AppUserDtos;
using Microsoft.EntityFrameworkCore;

namespace App.API.Models.Campgrounds
{
    [PrimaryKey(nameof(AppUserId), nameof(CampgroundId))]
    public class AppUserFavouriteCampground
    {
        public string AppUserId { get; set; }
        public int CampgroundId { get; set; }
        public DateTime FavouritedAt { get; set; }

        //public AppUserFavouriteCampgroundDto ToDto(AppUserFavouriteCampground favCampground)
        //{
        //    var campground = 
        //    var appUserFavouriteCampgroundDto = new AppUserFavouriteCampgroundDto
        //    {
        //        AppUserId = favCampground.AppUserId,
        //        CampgroundId = favCampground.CampgroundId.ToString(),
        //        CampgroundName = favCampground.
        //    };
        //}
    }
}
