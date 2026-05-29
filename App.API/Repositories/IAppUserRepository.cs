using App.API.Models.Identity;

namespace App.API.Repositories
{
    public interface IAppUserRepository
    {
        Task<Result<List<AppUser>>> GetAllAppUsersAsync();
        Task<Result<List<AppUser>>> GetAllAdminAppUsersAsync();
        Task<Result<List<AppUser>>> GetAllRegularAppUsersAsync();
        Task<Result<AppUser>> GetAppUserByIdAsync();
    }
}
