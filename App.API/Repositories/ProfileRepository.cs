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

        /// <summary>
        /// Asynchronous method that retrieves a profile by its unique identifier (primary key).
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// A Result object of type Profile that contains the respective Profile if successful, or an error message if the operation fails.
        /// See <see cref="Result{T}"/> for more details on the return object.
        /// </returns>
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

        /// <summary>
        /// Asynchronous method that updates a given profile.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// A Result object of type Profile that contains an updated object of the respective Profile if the update is successful, or an error message if the operation fails.
        /// See <see cref="Result{T}"/> for more details on the return object.
        /// </returns>
        public async Task<Result<Profile>> UpdateProfileAsync(int id)
        {
            try
            {
                _logger.LogInformation("ProfileRepository method called: UpdateProfileAsync...");
                _logger.LogInformation($"Attempting to retrieve profile with ID: {id}...");
                var profileResult = await GetProfileByIdAsync(id);

                if(profileResult.Succeeded == false)
                {
                    _logger.LogWarning($"Profile with ID: {id} not found...");
                }

                return profileResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update profile with ID: {id}...");
                return Result<Profile>.Failure("An error occurred while attempting to update the profile.");
            }
        }

        /// <summary>
        /// Asynchronous method that creates a new <see cref="Profile"/> object in the database.
        /// The <see cref="Profile"/> object created uses data retrieved from the respective <see cref="AppUser"/>
        /// at the time of registration, and is associated with the <see cref="AppUser"/> through a foreign key relationship.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// A Result object of type Profile that contains the respective Profile if successful, or an error message if the operation fails.
        /// See <see cref="Result{T}"/> for more details on the return object.
        /// </returns>
        public async Task<Result<Profile>> CreateNewProfileAsync(AppUser newUser)
        {
            try
            {
                _logger.LogInformation("ProfileRepository method called: CreateNewProfileAsync...");
                _logger.LogInformation($"Attempting to create new profile for user with ID: {newUser.Id}...");

                // Create new profile object using data from the new user
                var newProfile = new Profile
                {
                    AppUserId = Convert.ToInt32(newUser.Id),
                    ProfileCreatedAt = DateTime.UtcNow,
                    ProfileImagePath = "https://picsum.photos/seed/picsum/200",
                    ProfileOwner = newUser
                };

                var addResult = await _context.Profiles.AddAsync(newProfile);

                var saveResult = await _context.SaveChangesAsync();

                if(saveResult == 0)
                {
                    _logger.LogError($"Failed to save new profile to the database.");
                }

                return Result<Profile>.Success(newProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Failled to create new profile with ID: {newUser.Id}...");
                return Result<Profile>.Failure("Failed to create new profile");
            }
        }
    }
}