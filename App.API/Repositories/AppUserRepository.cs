using App.API.Data;
using App.API.Models;
using App.API.Models.Identity;
using App.API.Exceptions.RepositoryExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace App.API.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ILogger<AppUserRepository> _logger;
        private readonly UserManager<AppUser> _userManager;

        public AppUserRepository(ILogger<AppUserRepository> logger, UserManager<AppUser> context)
        {
            _logger = logger;
            _userManager = context;
        }

        public async Task<Result<List<AppUser>>> GetAllAppUsersAsync()
        {
            try
            {
                _logger.LogInformation("AppUserRepository method called: GetAllAppUsers()...");
                _logger.LogInformation("Attempting to retrieve all app users...");

                var appUsers = await _userManager.Users.ToListAsync();

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

                var admins = await _userManager.Users.Where(user => user.AppUserType == Enums.AppUserType.ADMINISTRATOR).ToListAsync();
                
                if(admins.Count() !> 0)
                {
                    _logger.LogWarning("Critical Warning: No Administrator users found. This should only be true in deliberate situations. " +
                        "If you see this warning and you're unsure why, it's very likely a problem.");
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

        public async Task<Result<List<AppUser>>> GetAllRegularAppUsersAsync()
        {
            try
            {
                _logger.LogInformation("AppUserRepository method called: GetAllRegularAppUsersAsync...");
                _logger.LogInformation("Attempting to retrieve all regular app users...");

                var regularUsers = await _userManager.Users.Where(user => user.AppUserType == Enums.AppUserType.REGULAR_USER).ToListAsync();
            
                if(regularUsers.Count() == 0)
                {
                    _logger.LogWarning("There are no regular users to retrieve. If you are seeing this warning, and you unsure why," + 
                        "it is probably not a good thing. If you know there are registered regular users, and you are seeing this warning, " +
                        "that is also probably not a good thing - both of these will likely indicate a backend issue..."); 
                }
                else if(regularUsers == null)
                {
                    _logger.LogError("The returned List<AppUser>() object is null. This should never be null. It can be empty, but it shouldn't be null. " +
                        "This indicates that the operation to retrieve the regular users from the database completely failed.");
                    throw new NullReferenceException("The list of regular users cannot be null. This indicates a backend issue. Please contact support.");
                }
                
                return Result<List<AppUser>>.Success(regularUsers);
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve regular users from the database...");
                return Result<List<AppUser>>.Failure("Failed to retrieve regulare users from the datbase. Please try again.");
            }
        }

        public async Task<Result<AppUser>> GetAppUserByIdAsync(int appUserId)
        {
            try
            {
                _logger.LogInformation("AppUserRepository method called: GetAppUserById...");
                _logger.LogInformation($"Attempting to retrieve AppUser: {appUserId}");

                var appUser = await _userManager.FindByIdAsync(Convert.ToString(appUserId));

                if(appUser == null)
                {
                    _logger.LogError($"AppUserId: {appUserId} is not in the system.");
                    return Result<AppUser>.Failure("User not found.");
                }

                return Result<AppUser>.Success(appUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to retrieve user. AppUserID: {appUserId}...");
                return Result<AppUser>.Failure("Failed to retrieve user.");
            }
        }

        public async Task<Result<bool>> DeleteAppUserAsync(int appUserId)
        {
            try
            {
                _logger.LogInformation("AppUserRepository method called: DeleteAppUserAsync...");
                _logger.LogInformation($"Attempting to delete User: {appUserId}...");

                var user = await _userManager.FindByIdAsync(Convert.ToString(appUserId));

                if(user == null)
                {
                    _logger.LogWarning($"User not found. Check user with ID: {appUserId} exists in the system");
                    return Result<bool>.Failure("Failed to retrieve the User. User not deleted from the database.");
                }

                var deleteResult = await _userManager.DeleteAsync(user);

                if(deleteResult.Succeeded != true)
                {
                    throw new RepositoryException("Failed to delete the user from the database. No changes made.");
                }

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete user ID: {appUserId}...");
                return Result<bool>.Failure("Failed to delete user.");
            }
        }
    }
}
