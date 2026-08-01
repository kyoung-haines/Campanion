using App.API.Data;
using App.API.Models.Identity;
using App.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.API.Seeders
{
    public static class ProfileSeeder
    {
        public static async Task SeedUserProfilesAsync(UserManager<AppUser> userManager, CampanionDbContext context, ILogger logger)
        {
            logger.LogInformation("Attempting to seed user profiles...");

            var users = await userManager.Users.ToListAsync<AppUser>();
            var rand = new Random();

            foreach (var user in users)
            {
                var profileExists = await context.Profiles
                    .AnyAsync(p => p.AppUserId == user.Id);

                if (profileExists)
                {
                    logger.LogInformation($"Profile for user {user.Id} already exists, skipping...");
                    continue;
                }

                logger.LogInformation($"Seeding profile for user {user.Id}...");

                var newProfile = new Profile
                {
                    AppUserId = user.Id,
                    ProfileCreatedAt = DateTime.UtcNow,
                    ProfileImagePath = "https://picsum.photos/seed/picsum/200",
                    ProfileUserProvince = user.AppUserProvince,
                    ProfileUserCountry = user.AppUserCountry,
                    ProfileOwner = user,
                    ProfileUsername = user.AppUserFirstName + user.AppUserLastName + rand.NextInt64()
                };

                await context.Profiles.AddAsync(newProfile);
            }

            await context.SaveChangesAsync();
        }
    }
}
