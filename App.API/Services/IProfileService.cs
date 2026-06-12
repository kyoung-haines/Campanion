using App.API.Models.Identity;

namespace App.API.Services
{
    public interface IProfileService
    {
        public Task<Result<Profile>> GetProfileByIdAsync(int id);
        public Task<Result<Profile>> UpdateProfileAsync(int id);
        public Task<Result<bool>> DeleteProfileAsync(int id);
        public Task<Result<Profile>> CreateNewProfileAsync(AppUser newUser);
    }
}
