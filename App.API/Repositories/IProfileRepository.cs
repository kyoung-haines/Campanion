using App.API.Models.Identity;

namespace App.API.Repositories
{
    public interface IProfileRepository
    {
        public Task<Result<Profile>> GetProfileByIdAsync(int id);
        public Task<Result<Profile>> UpdateProfileAsync(int id);
        public Task<Result<bool>> DeleteProfileAsync(int id);
    }
}
