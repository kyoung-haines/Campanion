using App.API.Data;
using App.API.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace App.API.Repositories.Identity
{
    public class AppUserRepository
    {
        private readonly ILogger _logger;
        private readonly CampanionDbContext _context;

        public AppUserRepository(CampanionDbContext context, ILogger logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task CreateAppUserAsync(AppUser appUser);
        public async Task DeleteAppUser(int id);
        public async Task<List<AppUser>> GetAllAppUserAsync()
        {
            _logger.LogInformation("Retrieving all app users...");

            var appUsers = await _context.AppUsers.ToListAsync();
            if(appUsers == null || appUsers.Count == 0)
            {
                _logger.LogInformation("There are no app users to retrieve...");
                _logger.LogInformation("Returning an empty list...");
            }

            _logger.LogInformation("App users retrieved. Returning a list of app users...");

            return appUsers;
        }
        public async Task<AppUser> GetAppUserByIdAsync();
        public async Task UpdateAppUserAsync(int id);
    }
}