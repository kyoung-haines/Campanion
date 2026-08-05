using App.API.Data;
using App.API.Models;
using App.API.Models.Identity;
using App.API.Exceptions.RepositoryExceptions;
using App.API.Exceptions.AppUserExceptions;
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

        public async Task<List<AppUser>> GetAllAppUsersAsync()
        {
            try
            {
                _logger.LogInformation("AppUserRepository method called: GetAllAppUsers()...");
                _logger.LogInformation("Attempting to retrieve all app users...");

                var appUsers = await _userManager.Users.ToListAsync();

                if(appUsers.Count() <= 0)
                {
                    _logger.LogWarning("Users List is empty. If there are registered users, an error has occurred...");
                }

                _logger.LogInformation("AppUserRepository successfully retrieved users...passing users to the service layer...");

                return appUsers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users from the database...");
                throw;
            }
        }

        public async Task<List<AppUser>> GetAllAdminAppUsersAsync()
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

                return admins;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve administrator users from the database...");
                throw;
            }
        }

        public async Task<List<AppUser>> GetAllRegularAppUsersAsync()
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
                    throw new NullReferenceException("This indicates a critical issue. Contact support.");
                }

                return regularUsers;
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve regular users from the database...");
                throw;
            }
        }

        public async Task<AppUser> GetAppUserByIdAsync(string appUserId)
        {
            try
            {
                _logger.LogInformation("AppUserRepository method called: GetAppUserById...");
                _logger.LogInformation($"Attempting to retrieve AppUser: {appUserId}");

                var appUser = await _userManager.FindByIdAsync(Convert.ToString(appUserId));

                if(appUser == null)
                {
                    _logger.LogError($"AppUserId: {appUserId} is not in the system.");
                    throw new InvalidUserIdException($"Invalid User ID: {appUserId}.");
                }

                return appUser;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to retrieve user. AppUserID: {appUserId}...");
                throw;
            }
        }

        public async Task<bool> DeleteAppUserAsync(string appUserId)
        {
            try
            {
                _logger.LogInformation("AppUserRepository method called: DeleteAppUserAsync...");
                _logger.LogInformation($"Attempting to delete User: {appUserId}...");

                var user = await _userManager.FindByIdAsync(Convert.ToString(appUserId));

                if(user == null)
                {
                    _logger.LogWarning($"User not found. Check user with ID: {appUserId} exists in the system");
                    return false;
                }

                var deleteResult = await _userManager.DeleteAsync(user);
                
                if(deleteResult.Succeeded != true)
                {
                    throw new RepositoryException("Failed to delete the user from the database. No changes made.");
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete user ID: {appUserId}...");
                return false;
            }
        }

        public async Task<AppUser> UpdateAppUserAsync(AppUser appUser)
        {
            try
            {
                _logger.LogInformation("AppUserRepository method called: UpdateAppUserAsync...");
                _logger.LogInformation($"Attempting to retrieve user: {appUser.Id}");

                if(appUser == null)
                {
                    _logger.LogError($"AppUser is null. AppUser: {appUser.Id} doesn't exist...");
                    throw new InvalidUserIdException($"Failed to retrieve user. ID: {appUser.Id} is invalid.");
                }

                var updatedResult = await _userManager.UpdateAsync(appUser);

                if (updatedResult.Succeeded == true)
                {
                    _logger.LogInformation($"Successfully updated AppUser: {appUser.Id}...");
                }
                else
                {
                    _logger.LogInformation($"Failed to update AppUser: {appUser.Id}");
                    throw new DbUpdateException("Failed to delete the user from the database. Please try again.");
                }

                return appUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update AppUser: {appUser.Id}...");
                throw;
            }
        }
    
        public async Task<IdentityResult> CreateAppUserAsync(AppUser newUser, string password)
        {
            try
            {
                _logger.LogInformation($"AppUserRepository method called: CreateAppUserAsync()...");
                _logger.LogInformation($"Attempting to create user with ID: {newUser.Id}...");
                var result = await _userManager.CreateAsync(newUser, password);

                if (result.Succeeded == false)
                {
                    _logger.LogWarning("Failed to save the user to the database... Errors: { Errors}", 
                        string.Join("; ", result.Errors.Select(e => e.Description)));
                }
                else
                {
                    _logger.LogInformation($"Successfully saved user: {newUser.Id} to the database...");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to save the user to the database...");
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UnexpectedError",
                    Description = "An unexpected error occurred while creating the user."
                });
            }
        }

        public async Task<IdentityResult> AddUserToRoleAsync(AppUser user, string role)
        {
            try
            {
                _logger.LogInformation($"Attempting to add user {user.Id} to role {role}...");
                var result = await _userManager.AddToRoleAsync(user, role);

                if (!result.Succeeded)
                {
                    _logger.LogWarning($"Failed to add user {user.Id} to role {role}...");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to add user {user.Id} to role {role}...");
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UnexpectedError",
                    Description = "An unexpected error occurred while assigning the role."
                });
            }
        }

        public async Task<AppUser> GetAppUserByEmailAsync(string userEmail)
        {
            try
            {
                _logger.LogInformation("AppUserRepository method called: GetAppUserByEmailAsync...");
                _logger.LogInformation($"Attempting to retrieve user with email:  {userEmail}...");

                if (userEmail == null || userEmail == string.Empty)
                {
                    _logger.LogError("The email address provided is blank...");
                    throw new InvalidUserEmailException("The user email provided is blank.");
                }

                AppUser user = await _userManager.FindByEmailAsync(userEmail.ToLower());

                if (user == null)
                {
                    _logger.LogWarning($"No user found with email: {userEmail}...");
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to retrieve user with email: {userEmail}...");
                throw;
            }
        }
    }
}
