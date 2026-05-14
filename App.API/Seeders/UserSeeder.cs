using App.API.Models;
using App.API.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.API.Seeders
{
    public static class UserSeeder
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, ILogger logger)
        {
            if(await userManager.FindByEmailAsync("user@campanion.com") == null)
            {
                logger.LogInformation("Seeding test user(s)...");

                var user = new AppUser
                {
                    UserName = "tester01",
                    Email = "user@campanion.com",
                    AppUserFirstName = "Tester",
                    AppUserLastName = "Testerton01",
                    AppUserCountry = "Canada",
                    AppUserProvince = "ON"
                };

                var result = await userManager.CreateAsync(user, "User@123!");
                if(result.Succeeded)
                {
                    logger.LogInformation("Test user created successfully!");
                    await userManager.AddToRoleAsync(user, Roles.Member);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        logger.LogError("User seeding failed: {Code} - {Description}", error.Code, error.Description);
                    }
                }
            }
            else
            {
                logger.LogInformation("Test user already exists, skipping...");
            }
        }
    }
}
