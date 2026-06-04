using App.API.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.API.Services
{
    public interface IAppUserService
    {
        public Task<Result<IEnumerable<AppUser>>> GetAllAppUsersAsync();
        public Task<Result<IEnumerable<AppUser>>> GetAllAppAdminsAsync();
        public Task<Result<IEnumerable<AppUser>>> GetAllRegularAppUsersAsync();
        public Task CreateAppUserAsync(AppUser user, IdentityResult result);
        public Task<AppUser> GetAppUserByIdAsync(int id);
        public Task UpdateAppUserByIdAsync(int id);
        public Task DeleteAppUserAsync(int id);
    }
}
