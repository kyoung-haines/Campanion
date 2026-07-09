using App.API.Models.Identity;
using Campanion.Shared.Dtos.AuthDtos;
namespace App.API.Services
{
    public interface IAuthService
    {
        public Task<Result<RegisterResponseDto>> RegisterNewUserAsync(RegistrationDto regDto);
    }
}
