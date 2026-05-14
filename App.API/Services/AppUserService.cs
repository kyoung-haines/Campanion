using App.API.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.API.Services
{
    public class AppUserService : IAppUserService
    {
        private UserManager<AppUser> _userManager;

        public AppUserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task CreateAppUserAsync(AppUser user, string password)
        {
            string userPasswordPlain = password;

            if(user != null)
            {
                await _userManager.CreateAsync(user, userPasswordPlain);
            }
            else
            {
                throw new Exception("Error creating user. Please try again.");
            }
        }
    }
}
