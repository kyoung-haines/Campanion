using App.API.Data;
using App.API.Models.Identity;

namespace App.API.Repositories
{
    public class ProfileRepository
    {
        private CampanionDbContext _context;
        private ILogger<ProfileRepository> _logger;

        public ProfileRepository(CampanionDbContext context, ILogger<ProfileRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result<bool>> DeleteProfileAsync(int id)
        {
            try
            {
                _logger.LogInformation("ProfileRepository method called: DeleteProfileAsync...");
                _logger.LogInformation($"Attempting to retrieve profile with ID {id}...");

                var profile = await _context.FindAsync<Profile>(id);

                if (profile == null)
                {
                    _logger.LogWarning($"Profile with ID {id} not found...");
                }

                var delete = _context.Remove<Profile>(profile);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete profile with ID {id}...");
                return Result<bool>.Failure("An error occurred while attempting to delete the profile.");
            }


        }
        public async Task<Result<Profile>> GetProfileByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("ProfileRepository method called: GetProfileByIdAsync...");
                _logger.LogInformation($"Attempting to retrieve profile with ID {id}...");

                var profile = await _context.FindAsync<Profile>(id);

                if(profile == null)
                {
                    _logger.LogWarning($"Profile with ID {id} not found...");
                }

                _logger.LogInformation($"Profile with ID {id} successfully retrieved...");

                return Result<Profile>.Success(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve profile with ID: {id}...");
                return Result<Profile>.Failure("Failed to retrieve profile.");
            }
        }
        public async Task<Result<Profile>> UpdateProfileAsync(int id)
        {
            try
            {
                var profile = await GetProfileByIdAsync(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}