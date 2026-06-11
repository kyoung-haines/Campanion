using App.API.Models;
using App.API.Models.Identity;
using App.API.Repositories;
using Microsoft.AspNetCore.Identity;

namespace App.API.Services
{
    public class AppUserService : IAppUserService
    {
        private ILogger _logger;
        private readonly IAppUserRepository _repository;

        public AppUserService(ILogger logger, IAppUserRepository repo)
        {
            _logger = logger;
            _repository = repo;
        }

        public async Task<Result<List<AppUser>>> GetAllAppUsersAsync()
        {
            try
            {
                var result = await _repository.GetAllAppUsersAsync();

                return result;
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

                var result = await _repository.GetAllAdminAppUsersAsync();
                return result;
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
                var regularUsersResult = await _repository.GetAllRegularAppUsersAsync();
                return regularUsersResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result<List<AppUser>>.Failure($"Failed to retrieve non-admin users list...\n{ex.Message}");
            }
        }

        public async Task<IdentityResult> CreateAppUserAsync(AppUser newUser, IdentityResult result)
        {
            var _result = result;
            try
            {
                _logger.LogInformation("AppUserService method called: CreateAppUserAsync...");
                
                _result = await _repository.CreateAppUserAsync(newUser, result);
                return _result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Failed to create user...");
                return _result;
            }
        }

        public async Task<Result<AppUser>> GetAppUserByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("AppUserService method called: GetAppUserByIdAsync...");
                var result = await _repository.GetAppUserByIdAsync(id);
                return result;
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
                var result = await _repository.UpdateAppUserAsync(id);
                return result;
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
                return result;
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
