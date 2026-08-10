using App.API.Enums;
using App.API.Models.Identity;
using Campanion.Shared.Dtos.CampgroundDtos;
namespace App.API.Models.Campgrounds
{
    public class Campground
    {
        // TODO: DOC COMMENTS AGAIN
        public int CampgroundId { get; set; }
        public required string CampgroundName= string.Empty;
        public string CampgroundImagePath= "images/campgrounds/placeholder-campground.png";
        public required string CampgroundStreetName= string.Empty;
        public required string CampgroundCity= string.Empty;
        public required string CampgroundProvince= string.Empty;
        public required string CampgroundCountry= string.Empty;
        public required string CampgroundPostalCode= string.Empty;
        public required string CampgroundPhone= string.Empty;
        public required string CampgroundEmail= "N/A";
        public required CampgroundType CampgroundType= new();
        public required bool CampgroundIsOpenYearRound= new();
        public DateOnly? CampgroundOpenDate= new();
        public DateOnly? CampgroundCloseDate= new();
        public required bool CampgroundHasFacilities { get; set; }
        public List<string>? CampgroundFacilities= new();
        public required bool CampgroundHasActivities= new();
        public List<string>? CampgroundActivities= new();
        public string? CampgroundUrl= string.Empty;

        // Navigational
        public List<AppUserFavouriteCampground> FavouritedBy= new();

        // Helper methods
        public async Task<CampgroundDto> ToDtoAsync(Campground campground)
        {
            var campgroundDto = new CampgroundDto
            {
                CampgroundIdDto = campground.CampgroundId.ToString(),
                CampgroundImagePathDto = campground.CampgroundImagePath,
                CampgroundStreetNameDto = campground.CampgroundStreetName,
                CampgroundCityDto = campground.CampgroundCity,
                CampgroundProvinceDto = campground.CampgroundProvince,
                CampgroundCountryDto = campground.CampgroundCountry,
                CampgroundPostalCodeDto = campground.CampgroundPostalCode,
                CampgroundPhoneDto = campground.CampgroundPhone,
                CampgroundEmailDto = campground.CampgroundEmail,
                CampgroundIsOpenYearRoundDto = campground.CampgroundIsOpenYearRound.ToString(),
                CampgroundOpenDateDto = campground.CampgroundOpenDate.ToString(),
                CampgroundCloseDateDto = campground.CampgroundCloseDate.ToString(),
                CampgroundHasFacilitiesDto = campground.CampgroundHasFacilities.ToString(),
                CampgroundFacilitiesDto = campground.CampgroundFacilities,
                CampgroundHasActivitiesDto = campground.CampgroundHasActivities.ToString(),
                CampgroundActivitiesDto = campground.CampgroundActivities,
                CampgroundUrlDto = campground.CampgroundUrl
            };

            // CampgroundType
            switch (campground.CampgroundType)
            {
                case Enums.CampgroundType.PROVINCIAL:
                    campgroundDto.CampgroundTypeDto = "PROVINCIAL";
                    break;
                case Enums.CampgroundType.MUNICIPAL:
                    campgroundDto.CampgroundTypeDto = "MUNICIPAL";
                    break;
                case Enums.CampgroundType.NATIONAL:
                    campgroundDto.CampgroundTypeDto = "NATIONAL";
                    break;
                case Enums.CampgroundType.PRIVATE:
                    campgroundDto.CampgroundTypeDto = "PRIVATE";
                    break;
            }

            // FavouritedBy


            return campgroundDto;
        }
    }
}
