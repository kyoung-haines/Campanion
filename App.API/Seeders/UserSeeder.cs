using App.API.Models;
using App.API.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.API.Seeders
{
    public static class UserSeeder
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(userManager.FindByEmailAsync("user@campanion.com") == null)
            {
                var user = new AppUser
                {
                    AppUserFirstName = "Tester",
                    AppUserLastName = "Testerton01",
                    AppUserCountry = "Canada",
                    AppUserProvince = "ON"
                };

                await userManager.CreateAsync(user, "User@123!");
                await userManager.AddToRoleAsync(user, Roles.Member);
            }
        }
    }
}
