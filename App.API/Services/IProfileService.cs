using App.API.Models.Identity;

namespace App.API.Services
{
    public interface IProfileService
    {
        public Task<Result<Profile>> GetProfileByProfileIdAsync(int id);
        public Task<Result<Profile>> UpdateProfileAsync(int id);
        public Task<Result<bool>> DeleteProfileAsync(int id);
        public Task<Result<Profile>> CreateNewProfileAsync(AppUser newUser);
        public Task<Result<Profile>> GetProfileByAppUserIdAsync(int appUserId);
    }
}
