using App.API.Data;
using App.API.Models.Campgrounds;
using App.API.Models.Identity;
using App.API.Services;

namespace App.API.Seeders
{
    public static class AppUserFavouriteCampgroundSeeder
    {
        public static async Task SeedAppUserFavouriteCampgroundsAsync(CampanionDbContext context, IAppUserFavouriteCampgroundService appUserFavouriteCampgroundService, ILogger logger, IAppUserService userService, ICampgroundService campgroundService)
        {
            logger.LogInformation("Attempting to seed AppUserFavouriteCampgrounds...");

            var testAppUsersResult = await userService.GetAllAppUsersAsync();
            var testCampgroundsResult = await campgroundService.GetAllCampgroundsAsync();

            var testAppUsers = testAppUsersResult.Data;
            var testCampgrounds = testCampgroundsResult.Data;

            var testAppUserFavouriteCampground1 = new AppUserFavouriteCampground();

            var testAppUserFavouriteCampground2 = new AppUserFavouriteCampground();

            int numCampgrounds = testCampgrounds.Count();
            int counter = 0;

            while (counter < numCampgrounds)
            {
                var campId = testCampgrounds[counter].CampgroundIdDto;
                var appUserId = testAppUsers[counter].AppUserId;
                var favouritedAt = DateTime.Now;

                var appUserFavouriteCampground = new AppUserFavouriteCampground
                {
                    AppUserId = appUserId.ToString(),
                    CampgroundId = Convert.ToInt32(campId),
                    FavouritedAt = favouritedAt
                };

                if (!context.AppUserFavouriteCampgrounds.Contains(appUserFavouriteCampground))
                {
                    logger.LogInformation($"Seeding AppUserFavouriteCampground - AppUserId: {appUserFavouriteCampground.AppUserId} | Campground Id: {appUserFavouriteCampground.CampgroundId}");
                    
                    context.AppUserFavouriteCampgrounds.Add(appUserFavouriteCampground);
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
}
