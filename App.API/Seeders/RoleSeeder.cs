using Microsoft.AspNetCore.Identity;
using App.API.Services;
namespace App.API.Seeders
{
    public class RoleSeeder
    {
        private readonly RolesService _roleService;

        public RoleSeeder(RolesService roleService)
        {
            _roleService = roleService;
        }

        public async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            List<string> roles = await _roleService.GetAllRolesAsync();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
