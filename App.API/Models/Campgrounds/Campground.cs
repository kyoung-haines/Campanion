using App.API.Enums;
using App.API.Models.Identity;
namespace App.API.Models.Campgrounds
{
    public class Campground
    {
        // TODO: DOC COMMENTS AGAIN
        public int CampgroundId { get; set; }
        public required string CampgroundName { get; set; }
        public required string CamproundImagePath { get; set; } = "images/campgrounds/placeholder-campground.png";
        public required string CampgroundStreetName { get; set; }
        public required string CampgroundCity { get; set; }
        public required string CampgroundProvince { get; set; }
        public required string CampgroundCountry { get; set; }
        public required string CampgroundPostalCode { get; set; }
        public required string CampgroundPhone { get; set; }
        public required string CampgroundEmail { get; set; } = "N/A";
        public required CampgroundType CampgroundType { get; set; }
        public required bool CampgroundIsOpenYearRound { get; set; }
        public DateOnly? CampgroundOpenDate { get; set; }
        public DateOnly? CampgroundCloseDate { get; set; }
        public required bool CampgroundHasFacilities { get; set; }
        public List<string>? CampgroundFacilities { get; set; }
        public required bool CampgroundHasActivities { get; set; }
        public List<string>? CampgroundActivities { get; set; }
        public string? CampgroundUrl { get; set; }
        public List<AppUser> FavouritedBy { get; set; }
    }
}
