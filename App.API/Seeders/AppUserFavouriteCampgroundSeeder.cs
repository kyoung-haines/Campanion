using App.API.Data;
using App.API.Models.Campgrounds;
using App.API.Models.Identity;
using App.API.Services;

namespace App.API.Seeders
{
    public static class AppUserFavouriteCampgroundSeeder
    {
        public static async Task SeedAppUserFavouriteCampgroundsAsync(CampanionDbContext context, ILogger logger, IAppUserService userService, ICampgroundService campgroundService)
        {
            logger.LogInformation("Attempting to seed AppUserFavouriteCampgrounds...");

            var testAppUsersResult = await userService.GetAllAppUsersAsync();
            var testCampgroundsResult = await campgroundService.GetAllCampgroundsAsync();

            var testAppUsers = testAppUsersResult.Data;
            var testCampgrounds = testCampgroundsResult.Data;

            var testAppUser1 = testAppUsers[0];
            var testAppUser2 = testAppUsers[1];

            var testCampground1 = testCampgrounds[0];
            var testCampground2 = testCampgrounds[1];

            var testAppUserFavouriteCampground1 = new AppUserFavouriteCampground
            {
                AppUserId = testAppUsers[0].AppUserId,
                CampgroundId = Convert.ToInt32(testCampgrounds[0].CampgroundIdDto),
                FavouritedAt = DateTime.Now
            };
            var testAppUserFavouriteCampground2 = new AppUserFavouriteCampground
            {
                AppUserId = testAppUsers[1].AppUserId,
                CampgroundId = Convert.ToInt32(testCampgrounds[1].CampgroundIdDto),
                FavouritedAt = DateTime.Now
            };

            int numCampgrounds = testCampgrounds.Count();
            int counter = 0;

            //while (counter < numCampgrounds)
            //{
            //    var campId = testCampgrounds[counter].CampgroundIdDto;
            //    var appUserId = testAppUsers[counter].AppUserId;
            //    var favouritedAt = DateTime.Now;

            //    var appUserFavouriteCampground = new AppUserFavouriteCampground
            //    {
            //        AppUserId = appUserId.ToString(),
            //        CampgroundId = Convert.ToInt32(campId),
            //        FavouritedAt = favouritedAt
            //    };    
            //}

            if (!context.AppUserFavouriteCampgrounds.Contains(testAppUserFavouriteCampground1))
            {
                logger.LogInformation($"Seeding AppUserFavouriteCampground - AppUserId: {testAppUserFavouriteCampground1.AppUserId} | Campground Id: {testAppUserFavouriteCampground1.CampgroundId}");

                context.AppUserFavouriteCampgrounds.Add(testAppUserFavouriteCampground1);
                await context.SaveChangesAsync();

                counter++;
            }

            if (!context.AppUserFavouriteCampgrounds.Contains(testAppUserFavouriteCampground2))
            {
                logger.LogInformation($"Seeding AppUserFavouriteCampground - AppUserId: {testAppUserFavouriteCampground2.AppUserId} | Campground Id: {testAppUserFavouriteCampground2.CampgroundId}");

                context.AppUserFavouriteCampgrounds.Add(testAppUserFavouriteCampground2);
                await context.SaveChangesAsync();

                counter++;
            }
            else
            {
                logger.LogInformation("AppUserFavourite record already exists. Skipping...");
                return;
            }
        }
    }
}
