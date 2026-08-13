using App.API.Enums;
using App.API.Models.Campgrounds;
using Campanion.Shared.Dtos.CampgroundDtos;
namespace App.API.Models.Campgrounds
{
    public class Campground
    {
        // TODO: DOC COMMENTS AGAIN
        public int CampgroundId { get; set; }
        public required string CampgroundName { get; set; } = "";
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

        // Navigational
        public List<AppUserFavouriteCampground> FavouritedBy= new();

        // Helper methods
        public async Task<CampgroundDto> ToDtoAsync(Campground campground)
        {
            if (campground == null)
                return new CampgroundDto
                {
                    CampgroundIdDto = "",
                    CampgroundImagePathDto = "",
                    CampgroundStreetNameDto = "",
                    CampgroundCityDto = "",
                    CampgroundProvinceDto = "",
                    CampgroundCountryDto = "",
                    CampgroundPostalCodeDto = "",
                    CampgroundPhoneDto = "",
                    CampgroundEmailDto = "",
                    CampgroundIsOpenYearRoundDto = "",
                    CampgroundOpenDateDto = "",
                    CampgroundCloseDateDto = "",
                    CampgroundHasFacilitiesDto = "",
                    CampgroundFacilitiesDto = new List<string>(),
                    CampgroundHasActivitiesDto = "",
                    CampgroundActivitiesDto = new List<string>(),
                    CampgroundUrlDto = ""
                };
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
            foreach (var user in campground.FavouritedBy)
            {
                var userId = user.AppUserId;
                var appUserUsername = user.AppUser.AppUserProfile.ProfileUsername;
                campgroundDto.FavouritedByDto.Add(appUserUsername);
            }

            return campgroundDto;
        }

        public CampgroundDto ToDto(Campground campground)
        {
            if (campground == null)
                return new CampgroundDto
                {
                    CampgroundIdDto = "",
                    CampgroundImagePathDto = "",
                    CampgroundStreetNameDto = "",
                    CampgroundCityDto = "",
                    CampgroundProvinceDto = "",
                    CampgroundCountryDto = "",
                    CampgroundPostalCodeDto = "",
                    CampgroundPhoneDto = "",
                    CampgroundEmailDto = "",
                    CampgroundIsOpenYearRoundDto = "",
                    CampgroundOpenDateDto = "",
                    CampgroundCloseDateDto = "",
                    CampgroundHasFacilitiesDto = "",
                    CampgroundFacilitiesDto = new List<string>(),
                    CampgroundHasActivitiesDto = "",
                    CampgroundActivitiesDto = new List<string>(),
                    CampgroundUrlDto = ""
                };
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
            foreach (var user in campground.FavouritedBy)
            {
                var userId = user.AppUserId;
                var appUserUsername = user.AppUser.AppUserProfile.ProfileUsername;
                campgroundDto.FavouritedByDto.Add(appUserUsername);
            }

            return campgroundDto;
        }
    }
}
