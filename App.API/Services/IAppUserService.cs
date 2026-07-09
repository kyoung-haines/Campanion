using App.API.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.API.Services
{
    public interface IAppUserService
    {
        public Task<Result<List<AppUser>>> GetAllAppUsersAsync();
        public Task<Result<List<AppUser>>> GetAllAppAdminsAsync();
        public Task<Result<List<AppUser>>> GetAllRegularAppUsersAsync();
        public Task<IdentityResult> CreateAppUserAsync(AppUser user, string password, string role);
        public Task<Result<AppUser>> GetAppUserByIdAsync(int id);
        public Task<Result<AppUser>> UpdateAppUserByIdAsync(int id);
        public Task<Result<bool>> DeleteAppUserAsync(int id);
        public Task<Result<AppUser>> GetAppUserByEmailAsync(string email);
    }
}
