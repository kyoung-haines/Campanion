using App.API.Models;
using App.API.Models.Identity;
using App.API.Repositories;
using Microsoft.AspNetCore.Identity;

namespace App.API.Services
{
    public class AppUserService : IAppUserService
    {
        private ILogger<AppUserService> _logger;
        private readonly IAppUserRepository _repository;

        public AppUserService(ILogger<AppUserService> logger, IAppUserRepository repo)
        {
            _logger = logger;
            _repository = repo;
        }

        public async Task<Result<List<AppUser>>> GetAllAppUsersAsync()
        {
            try
            {
                _logger.LogInformation("AppUserService method called: GetAllAppUsersAsync...");
                var allUsers = await _repository.GetAllAppUsersAsync();

                var allUsersResult = Result<List<AppUser>>.Success(allUsers);

                _logger.LogInformation("AppUserService successfully retrieved all users from the repository layer...");
                _logger.LogInformation("AppUserService sending users list...");

                return allUsersResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to retrieve users...", ex.Message);
                return Result<List<AppUser>>.Failure($"Failed to retrieve all users...\n{ex.Message}");
            }            
        }

        public async Task<Result<List<AppUser>>> GetAllAppAdminsAsync()
        {
            try
            {
                _logger.LogInformation("AppUserServiceMethod called: GetAllAppAdminsAsync()...");

                var allAdmins = await _repository.GetAllAdminAppUsersAsync();

                var allAdminsResult = Result<List<AppUser>>.Success(allAdmins);

                return allAdminsResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result<List<AppUser>>.Failure($"Failed to retrieve admins list...\n{ex.Message}...");
            }
        }

        public async Task<Result<List<AppUser>>> GetAllRegularAppUsersAsync()
        {
            try
            {
                _logger.LogInformation("AppUserService method called: GetAllRegularAppUsersAsync...");
                var allRegularUsers = await _repository.GetAllRegularAppUsersAsync();

                var allRegularUsersResult = Result<List<AppUser>>.Success(allRegularUsers);

                return allRegularUsersResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result<List<AppUser>>.Failure($"Failed to retrieve non-admin users list...\n{ex.Message}");
            }
        }

        public async Task<IdentityResult> CreateAppUserAsync(AppUser newUser, string password, string role = Roles.Member)
        {
            _logger.LogInformation("AppUserService method called: CreateAppUserAsync...");
                
            var createResult = await _repository.CreateAppUserAsync(newUser, password);
            
            if (!createResult.Succeeded)
            {
                return createResult;
            }

            var roleResult = await _repository.AddUserToRoleAsync(newUser, role);

            return roleResult;

        }

        public async Task<Result<AppUser>> GetAppUserByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("AppUserService method called: GetAppUserByIdAsync...");
                var appUser = await _repository.GetAppUserByIdAsync(id);

                var appUserResult = Result<AppUser>.Success(appUser);

                return appUserResult;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get user with ID: {id}...\n{ex.Message}");
                return Result<AppUser>.Failure(ex.Message);
            }
        }

        public async Task<Result<AppUser>> UpdateAppUserByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("AppUserService method called: UpdateAppUserByIdAsync...");
                var updateUser = await _repository.UpdateAppUserAsync(id);
                return Result<AppUser>.Success(updateUser);
            }
            catch (Exception ex)
            {
                return Result<AppUser>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteAppUserAsync(int id)
        {
            try
            {
                _logger.LogInformation("AppUserService method called: DeleteAppUserAsync...");
                var result = await _repository.DeleteAppUserAsync(id);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<AppUser>> GetAppUserByEmailAsync(string userEmail)
        {
            try
            {
                _logger.LogInformation("AppUserService method called: GetAppUserByEmailAsync...");

                var user = await _repository.GetAppUserByEmailAsync(userEmail);

                var userResult = Result<AppUser>.Success(user);

                _logger.LogInformation($"Successfully retrieved user with email: {userEmail}...");

                return userResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("AppUserService Exception thrown: ", ex.Message);
                throw;
            }
        }
    }
}
