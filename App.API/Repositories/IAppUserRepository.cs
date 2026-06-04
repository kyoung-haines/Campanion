using App.API.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.API.Repositories
{
    public interface IAppUserRepository
    {
        public Task<Result<IEnumerable<AppUser>>> GetAllAppUsersAsync();
        public Task<Result<IEnumerable<AppUser>>> GetAllAdminAppUsersAsync();
        public Task<Result<IEnumerable<AppUser>>> GetAllRegularAppUsersAsync();
        public Task<Result<AppUser>> GetAppUserByIdAsync(int appUserId);
        public Task<Result<bool>> DeleteAppUserAsync(int appUserId);
        public Task<Result<AppUser>> UpdateAppUserAsync(int appUserId);
        public Task<IdentityResult> CreateAppUserAsync(AppUser newUser);
    }
}
