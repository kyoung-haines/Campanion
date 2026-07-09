using App.API.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.API.Repositories
{
    public interface IAppUserRepository
    {
        public Task<List<AppUser>> GetAllAppUsersAsync();
        public Task<List<AppUser>> GetAllAdminAppUsersAsync();
        public Task<List<AppUser>> GetAllRegularAppUsersAsync();
        public Task<AppUser> GetAppUserByIdAsync(string appUserId);
        public Task<bool> DeleteAppUserAsync(string appUserId);
        public Task<AppUser> UpdateAppUserAsync(string appUserId);
        public Task<IdentityResult> CreateAppUserAsync(AppUser newUser);
        public Task<AppUser> GetAppUserByEmailAsync(string userEmail);
    }
}
