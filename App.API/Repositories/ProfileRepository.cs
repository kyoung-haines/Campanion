using App.API.Data;
using App.API.Exceptions.AppUserExceptions;
using App.API.Exceptions.ProfileExceptions;
using App.API.Models.Identity;

namespace App.API.Repositories
{
    public class ProfileRepository :IProfileRepository
    {
        private CampanionDbContext _context;
        private ILogger<ProfileRepository> _logger;

        public ProfileRepository(CampanionDbContext context, ILogger<ProfileRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> DeleteProfileAsync(int id)
        {
            try
            {
                _logger.LogInformation("ProfileRepository method called: DeleteProfileAsync...");
                _logger.LogInformation($"Attempting to retrieve profile with ID {id}...");

                var profile = await _context.FindAsync<Profile>(id);

                if (profile == null)
                {
                    _logger.LogWarning($"Profile with ID {id} not found...");
                    throw new InvalidProfileIdException(); // throws default message
                }

                var delete = _context.Remove<Profile>(profile);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete profile with ID {id}...");
                return false;
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
        public async Task<Profile> GetProfileByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("ProfileRepository method called: GetProfileByIdAsync...");
                _logger.LogInformation($"Attempting to retrieve profile with ID {id}...");

                var profile = await _context.FindAsync<Profile>(id);

                if (profile == null)
                {
                    _logger.LogWarning($"Profile with ID {id} not found...");
                    throw new InvalidProfileIdException();
                }

                _logger.LogInformation($"Profile with ID {id} successfully retrieved...");

                return profile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve profile with ID: {id}...");
                throw;
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
        public async Task<Profile> UpdateProfileAsync(int id)
        {
            try
            {
                _logger.LogInformation("ProfileRepository method called: UpdateProfileAsync...");
                _logger.LogInformation($"Attempting to retrieve profile with ID: {id}...");
                var profile = await GetProfileByIdAsync(id);

                if(profile == null)
                {
                    _logger.LogWarning($"Profile with ID: {id} not found...");
                    throw new InvalidProfileIdException(); // throws default message
                }

                _context.Update<Profile>(profile);
                var saveResult = await _context.SaveChangesAsync();

                if (saveResult == 0)
                {
                    _logger.LogError($"Failed to save profile...");

                    while (saveResult == 0)
                    {
                        saveResult = await _context.SaveChangesAsync();
                    }
                }

                return profile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update profile with ID: {id}...");
                throw;
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
        public async Task<Profile> CreateNewProfileAsync(AppUser newUser)
        {
            try
            {
                _logger.LogInformation("ProfileRepository method called: CreateNewProfileAsync...");
                _logger.LogInformation($"Attempting to create new profile for user with ID: {newUser.Id}...");

                if (newUser == null)
                {
                    _logger.LogWarning("The AppUser object is null. Cannot add new user to database...");
                    throw new AppUserException("The AppUser object is null. Cannot create a new user from a null object");
                }

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

                return newProfile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Failled to create new profile with ID: {newUser.Id}...");
                throw;
            }
        }

        public async Task<Profile> GetProfileByAppUserId(AppUser appUser)
        {
            try
            {
                _logger.LogInformation("ProfileRepository method called: GetProfileByAppUserId...");
                _logger.LogInformation($"Attempting to retrieve profile for user: {appUser.Id}...");

                int appUserId = Convert.ToInt32(appUser.Id);

                List<Profile> allProfiles = _context.Profiles.ToList<Profile>();
                Profile profileById = new();

                foreach (var profile in allProfiles)
                {
                    if (profile.AppUserId == appUserId)
                    {
                        profileById = profile;
                    }
                }

                return profileById;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve user profile...");
                throw;
            }
        }
    }
}