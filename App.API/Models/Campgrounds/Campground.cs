using App.API.Enums;
using App.API.Models.Identity;
namespace App.API.Models.Campgrounds
{
    public class Campground
    {
        // TODO: DOC COMMENTS AGAIN
        public int CampgroundId { get; set; }
        public required string CampgroundName { get; set; } = string.Empty;
        public string CampgroundImagePath { get; set; } = "images/campgrounds/placeholder-campground.png";
        public required string CampgroundStreetName { get; set; } = string.Empty;
        public required string CampgroundCity { get; set; } = string.Empty;
        public required string CampgroundProvince { get; set; } = string.Empty;
        public required string CampgroundCountry { get; set; } = string.Empty;
        public required string CampgroundPostalCode { get; set; } = string.Empty;
        public required string CampgroundPhone { get; set; } = string.Empty;
        public required string CampgroundEmail { get; set; } = "N/A";
        public required CampgroundType CampgroundType { get; set; } = new();
        public required bool CampgroundIsOpenYearRound { get; set; } = new();
        public DateOnly? CampgroundOpenDate { get; set; } = new();
        public DateOnly? CampgroundCloseDate { get; set; } = new();
        public required bool CampgroundHasFacilities { get; set; }
        public List<string>? CampgroundFacilities { get; set; } = new();
        public required bool CampgroundHasActivities { get; set; } = new();
        public List<string>? CampgroundActivities { get; set; } = new();
        public string? CampgroundUrl { get; set; } = string.Empty;
        public List<AppUserFavouriteCampground> FavouritedBy { get; set; } = new();
    }
}
