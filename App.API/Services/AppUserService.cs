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

        public async Task<Result<IEnumerable<AppUser>>> GetAllAppUsersAsync()
        {
            try
            {
                var result = await _repository.GetAllAppUsersAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to retrieve users...", ex.Message);
                return Result<IEnumerable<AppUser>>.Failure($"Failed to retrieve all users...\n{ex.Message}");
            }            
        }

        public async Task<Result<IEnumerable<AppUser>>> GetAllAppAdminsAsync()
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
                return Result<IEnumerable<AppUser>>.Failure($"Failed to retrieve admins list...\n{ex.Message}...");
            }
        }

        public async Task CreateAppUserAsync(AppUser user, string password)
        {
            _logger.LogInformation("Creating new user...");

            string userPasswordPlain = password;

            if(user != null)
            {
                var result = await _userManager.CreateAsync(user, userPasswordPlain);
                if(result.Succeeded)
                {
                    _logger.LogInformation("User successfully created...");
                }
            }
            else
            {
                _logger.LogError("User not created successfully. The AppUser object is null");
                throw new Exception("Error creating user. Please try again.");
            }
        }

        public async Task<AppUser> GetAppUserByIdAsync(int id)
        {
            _logger.LogInformation($"Looking for User with ID: {id}...");

            AppUser? user = await _userManager.FindByIdAsync(Convert.ToString(id));

            if(user == null)
            {
                _logger.LogError("User is null. Check the ID value exists.");
                throw new Exception($"User with ID: {id} does not exist. Please check the ID value.");
            }

            _logger.LogInformation($"User with ID: {id} found!");

            return user;
        }

        public async Task UpdateAppUserByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(id));

            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteAppUserAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(id));
            await _userManager.DeleteAsync(user);
        }
    }
}
