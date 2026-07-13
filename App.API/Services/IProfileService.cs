using App.API.Models.Identity;
using Campanion.Shared.Dtos.ProfileDtos;

namespace App.API.Services
{
    public interface IProfileService
    {
        public Task<Result<Profile>> GetProfileByProfileIdAsync(int id);
        public Task<Result<Profile>> UpdateProfileAsync(int id);
        public Task<Result<bool>> DeleteProfileAsync(int id);
        public Task<Result<Profile>> CreateNewProfileAsync(AppUser newUser);
        public Task<Result<Profile>> GetProfileByAppUserIdAsync(AppUser user);
        public Task<ProfileResponseDto> ConvertProfileObjectToResponseDto(Profile profile);
    }
}
