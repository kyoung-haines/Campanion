using App.API.Data;
using App.API.Models.Identity;
using App.API.Services;
using Microsoft.EntityFrameworkCore;
namespace App.API.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ILogger<AppUserRepository> _logger;
        private readonly CampanionDbContext _context;

        public AppUserRepository(ILogger<AppUserRepository> logger, CampanionDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<List<AppUser>>> GetAllAppUsersAsync()
        {
            try
            {
                _logger.LogInformation("AppUserRepository method called: GetAllAppUsers()...");
                _logger.LogInformation("Attempting to retrieve all app users...");

                var appUsers = await _context.AppUsers.ToListAsync();

                if(appUsers.Count() <= 0)
                {
                    _logger.LogWarning("AppUsers list is empty. If there are registered users, an error has occurred...");
                }

                return Result<List<AppUser>>.Success(appUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users from the database...");
                return Result<List<AppUser>>.Failure($"Failed to retrieve users from the database.");
            }
        }

        public async Task<Result<List<AppUser>>> GetAllAdminAppUsersAsync()
        {
            var admins = await _context.AppUsers.Where(user => user.AppUserType == Enums.AppUserType.ADMINISTRATOR).ToListAsync();

            return Result<List<AppUser>>.Success(admins);
        }
    }
}
