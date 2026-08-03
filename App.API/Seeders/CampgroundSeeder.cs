using App.API.Data;
using App.API.Enums;
using App.API.Models.Campgrounds;
using App.API.Services;

namespace App.API.Seeders
{
    public static class CampgroundSeeder
    {
        public static async Task SeedCampgroundsAsync(CampanionDbContext context, ICampgroundService campgroundService, ILogger logger)
        {
            logger.LogInformation("Attempting to seed test campgrounds...");

            var _testCampground1 = new Campground
            {
                CampgroundName = "Test Campground 1",
                CampgroundCity = "Guelph",
                CampgroundCountry = "Canada",
                CampgroundEmail = "testcampground1@test.ca",
                CampgroundProvince = "ON",
                CampgroundStreetName = "Test Street",
                CampgroundPostalCode = "N1G4V6",
                CampgroundPhone = "1111111111",
                CampgroundType = CampgroundType.PROVINCIAL,
                CampgroundIsOpenYearRound = true,
                CampgroundHasActivities = false,
                CampgroundHasFacilities = false
            };

            var _testCampground2 = new Campground
            {
                CampgroundName = "Test Campground 2",
                CampgroundCity = "Guelph",
                CampgroundCountry = "Canada",
                CampgroundEmail = "testcampground2@test.ca",
                CampgroundProvince = "ON",
                CampgroundStreetName = "Test Road",
                CampgroundPostalCode = "N1M1M1",
                CampgroundPhone = "2222222222",
                CampgroundType = CampgroundType.NATIONAL,
                CampgroundIsOpenYearRound = true,
                CampgroundHasActivities = false,
                CampgroundHasFacilities = false
            };

            if (!context.Campgrounds.Contains<Campground>(_testCampground1))
            {
                await context.Campgrounds.AddAsync(_testCampground1);

                logger.LogInformation("Campground successfully seeded...");
            }

            if (!context.Campgrounds.Contains<Campground>(_testCampground2))
            {
                await context.Campgrounds.AddAsync(_testCampground2);

                logger.LogInformation("Campground successfully seeded...");
            }

            else
            {
                logger.LogInformation("Campground already exists - skipping...");
                return;
            }

            await context.SaveChangesAsync();

            
        }
    }
}
