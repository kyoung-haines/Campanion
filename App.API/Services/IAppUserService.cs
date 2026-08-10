using App.API.Models;
using App.API.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Campanion.Shared.Dtos.AppUserDtos;

namespace App.API.Services
{
    public interface IAppUserService
    {
        public Task<Result<List<AppUserDto>>> GetAllAppUsersAsync();
        public Task<Result<List<AppUserDto>>> GetAllAppAdminsAsync();
        public Task<Result<List<AppUserDto>>> GetAllRegularAppUsersAsync();
        public Task<IdentityResult> CreateAppUserAsync(AppUser user, string password, string role = Roles.Member);
        public Task<Result<AppUserDto>> GetAppUserByIdAsync(string id);
        public Task<Result<AppUserDto>> UpdateAppUserByIdAsync(string id);
        public Task<Result<bool>> DeleteAppUserAsync(string id);
        public Task<Result<AppUserDto>> GetAppUserByEmailAsync(string email);
    }
}
