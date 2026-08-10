using App.API.Models;
using App.API.Models.Identity;
using App.API.Repositories;
using Microsoft.AspNetCore.Identity;
using Campanion.Shared.Dtos.AppUserDtos;

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

        public async Task<Result<List<AppUserDto>>> GetAllAppUsersAsync()
        {
            try
            {
                _logger.LogInformation("AppUserService method called: GetAllAppUsersAsync...");
                var allUsers = await _repository.GetAllAppUsersAsync();

                var allUsersDtoList = new List<AppUserDto>();

                foreach (var user in allUsers)
                {
                    var userDto = await user.ToDoAsync(user);

                    allUsersDtoList.Add(userDto);
                }

                var allUsersResult = Result<List<AppUserDto>>.Success(allUsersDtoList);

                _logger.LogInformation("AppUserService successfully retrieved all users from the repository layer...");
                _logger.LogInformation("AppUserService sending users list...");

                return allUsersResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to retrieve users...", ex.Message);
                return Result<List<AppUserDto>>.Failure($"Failed to retrieve all users...\n{ex.Message}");
            }            
        }

        public async Task<Result<List<AppUserDto>>> GetAllAppAdminsAsync()
        {
            try
            {
                _logger.LogInformation("AppUserServiceMethod called: GetAllAppAdminsAsync()...");

                var allAdmins = await _repository.GetAllAdminAppUsersAsync();

                var allAdminsDtoList = new List<AppUserDto>();

                foreach (var admin in allAdmins)
                {
                    var adminDto = await admin.ToDoAsync(admin);
                    allAdminsDtoList.Add(adminDto);
                }

                var allAdminsResult = Result<List<AppUserDto>>.Success(allAdminsDtoList);

                return allAdminsResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result<List<AppUserDto>>.Failure($"Failed to retrieve admins list...\n{ex.Message}...");
            }
        }

        public async Task<Result<List<AppUserDto>>> GetAllRegularAppUsersAsync()
        {
            try
            {
                _logger.LogInformation("AppUserService method called: GetAllRegularAppUsersAsync...");
                var allRegularUsers = await _repository.GetAllRegularAppUsersAsync();

                var allRegularUserDtosList = new List<AppUserDto>();

                foreach (var user in allRegularUsers)
                {
                    var userDto = await user.ToDoAsync(user);
                    allRegularUserDtosList.Add(userDto);
                }

                return Result<List<AppUserDto>>.Success(allRegularUserDtosList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result<List<AppUserDto>>.Failure($"Failed to retrieve non-admin users list...\n{ex.Message}");
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

        public async Task<Result<AppUserDto>> GetAppUserByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation("AppUserService method called: GetAppUserByIdAsync...");
                var appUser = await _repository.GetAppUserByIdAsync(id);

                var appUserDto = await appUser.ToDoAsync(appUser);

                return Result<AppUserDto>.Success(appUserDto);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get user with ID: {id}...\n{ex.Message}");
                return Result<AppUserDto>.Failure(ex.Message);
            }
        }

        public async Task<Result<AppUserDto>> UpdateAppUserByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation("AppUserService method called: UpdateAppUserByIdAsync...");

                var appUser = await _repository.GetAppUserByIdAsync(id);

                await _repository.UpdateAppUserAsync(appUser);

                var appUserDto = await appUser.ToDoAsync(appUser);

                return Result<AppUserDto>.Success(appUserDto);
            }
            catch (Exception ex)
            {
                return Result<AppUserDto>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteAppUserAsync(string id)
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

        public async Task<Result<AppUserDto>> GetAppUserByEmailAsync(string userEmail)
        {
            try
            {
                _logger.LogInformation("AppUserService method called: GetAppUserByEmailAsync...");

                var appUser = await _repository.GetAppUserByEmailAsync(userEmail);

                var appUserDto = await appUser.ToDoAsync(appUser);

                _logger.LogInformation($"Successfully retrieved user with email: {userEmail}...");

                return Result<AppUserDto>.Success(appUserDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("AppUserService Exception thrown: ", ex.Message);
                throw;
            }
        }
    }
}
