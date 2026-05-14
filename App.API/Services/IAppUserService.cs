using App.API.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.API.Services
{
    public interface IAppUserService
    {
        Task CreateAppUserAsync(AppUser user, string password);
        Task<AppUser> GetAppUserByIdAsync(int id);
    }
}
