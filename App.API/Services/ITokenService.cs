using App.API.Models.Identity;

namespace App.API.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(AppUser user);
    }
}
