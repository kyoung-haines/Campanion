using App.API.Data;
using App.API.Models.Identity;
using App.API.Services;
using Microsoft.AspNetCore.Identity;

namespace App.API.Seeders
{
    public static class ProfileSeeder
    {
        public static async Task SeedUserProfilesAsync(AppUser newUser, CampanionDbContext context, ProfileService profileService, ILogger logger)
        {
            logger.LogInformation("Attempting to seed user profiles...");

            if (await profileService.GetProfileByAppUserIdAsync(newUser) == null )
            {
                logger.LogInformation("Seeding profile...");

                var rand = new Random();

                var newUserProfile = new Profile
                {
                    AppUserId = newUser.Id,
                    ProfileCreatedAt = DateTime.UtcNow,
                    ProfileImagePath = "https://picsum.photos/seed/picsum/200",
                    ProfileOwner = newUser,
                    ProfileUsername = newUser.AppUserFirstName + newUser.AppUserLastName + rand.NextInt64()
                };

                context.Profiles.Add(newUserProfile);
            }
            else
            {
                logger.LogInformation("Profile exists...skipping...");
            }
        }
    }
}
