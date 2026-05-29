using App.API.Models.Identity;

namespace App.API.Repositories
{
    public interface IAppUserRepository
    {
        public Task<Result<List<AppUser>>> GetAllAppUsersAsync();
        public Task<Result<List<AppUser>>> GetAllAdminAppUsersAsync();
        public Task<Result<List<AppUser>>> GetAllRegularAppUsersAsync();
        public Task<Result<AppUser>> GetAppUserByIdAsync(int appUserId);
    }
}
