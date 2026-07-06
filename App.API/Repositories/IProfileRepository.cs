using App.API.Models.Identity;

namespace App.API.Repositories
{
    public interface IProfileRepository
    {
        public Task<Profile> GetProfileByIdAsync(int id);
        public Task<Profile> UpdateProfileAsync(int id);
        public Task<bool> DeleteProfileAsync(int id);
        public Task<Profile> CreateNewProfileAsync(AppUser newUser);
    }
}
