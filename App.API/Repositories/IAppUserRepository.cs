using App.API.Models.Identity;

namespace App.API.Repositories
{
    public interface IAppUserRepository
    {
        Task<List<AppUser>> GetAllAppUsersAsync();
    }
}
