using App.API.Models.Identity;

namespace App.API.Repositories.Identity
{
    public interface IAppUserRepository
    {
        public Task<List<AppUser>> GetAllAppUserAsync();
        public Task<AppUser> GetAppUserByIdAsync();
        public Task CreateAppUserAsync(AppUser appUser);
        public Task UpdateAppUserAsync(int id);
        public Task DeleteAppUser(int id);

    }
}
