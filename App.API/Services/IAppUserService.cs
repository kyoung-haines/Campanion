using App.API.Models.Identity;

namespace App.API.Services
{
    public interface IAppUserService
    {
        Task<IList<AppUser>> GetAllAppUsersAsync();
        Task<IList<AppUser>> GetAllAppAdminsAsync();
        Task CreateAppUserAsync(AppUser user, string password);
        Task<AppUser> GetAppUserByIdAsync(int id);
        Task UpdateAppUserByIdAsync(int id);
        Task DeleteAppUserAsync(int id);
    }
}
