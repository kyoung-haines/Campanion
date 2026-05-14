using App.API.Data;
using App.API.Models.Identity;

namespace App.API.Repositories.Identity
{
    public class AppUserRepository
    {
        private readonly CampanionDbContext _context;

        public AppUserRepository(CampanionDbContext context)
        {
            _context = context;
        }

        public Task CreateAppUserAsync(AppUser appUser);
        public Task DeleteAppUser(int id);
        public Task<List<AppUser>> GetAllAppUserAsync()
        {

        }
        public Task<AppUser> GetAppUserByIdAsync();
        public Task UpdateAppUserAsync(int id);
    }
}