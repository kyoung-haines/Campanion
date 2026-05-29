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
            try
            {
                _logger.LogInformation("AppUserRepository method called: GetAllAdminAppUsersAsync...");
                _logger.LogInformation("Attempting to retrieve all admininstrator users...");
                var admins = await _context.AppUsers.Where(user => user.AppUserType == Enums.AppUserType.ADMINISTRATOR).ToListAsync();
                
                if(admins.Count() !> 0)
                {
                    _logger.LogWarning("Critical Warning: No Administrator users found. This should only be true in deliberate situations. " +
                        "If you see this warning and you're unsure why, it's a problem.");
                }
                else
                {
                    _logger.LogInformation("Successfully retrieved administrator users...");
                }

                return Result<List<AppUser>>.Success(admins);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve administrator users from the database...");
                return Result<List<AppUser>>.Failure("Failed to retrieve administrator users from the database.");
            }
        }
    }
}
