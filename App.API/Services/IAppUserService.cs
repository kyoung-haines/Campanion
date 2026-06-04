using App.API.Models.Identity;

namespace App.API.Services
{
    public interface IAppUserService
    {
        Task<IEnumerable<AppUser>> GetAllAppUsersAsync();
        Task<IEnumerable<AppUser>> GetAllAppAdminsAsync();
        public Task<Result<IEnumerable<AppUser>>> GetAllRegularAppUsersAsync();
        Task CreateAppUserAsync(AppUser user, string password);
        Task<AppUser> GetAppUserByIdAsync(int id);
        Task UpdateAppUserByIdAsync(int id);
        Task DeleteAppUserAsync(int id);
    }
}
