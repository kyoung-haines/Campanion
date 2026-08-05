using App.API.Models.Campgrounds;
using App.API.Data;
using App.API.Exceptions.CampgroundExceptions;
using Microsoft.EntityFrameworkCore;

namespace App.API.Repositories
{
    public class CampgroundRepository : ICampgroundRepository
    {
        private readonly ILogger<CampgroundRepository> _logger;
        private readonly CampanionDbContext _context;

        public CampgroundRepository(ILogger<CampgroundRepository> logger, CampanionDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> DeleteCampgroundAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Repository Method Called: DeleteCampgroundAsync()...");
                _logger.LogInformation($"Attempting to delete campground with ID: {id}...");

                // var campground = await _context.FindAsync<Campground>(id);
                var campground = await GetCampgroundByIdAsync(id);

                if(campground == null)
                {
                    _logger.LogWarning($"Campground with ID: {id} is not in the system...");
                    throw new InvalidCampgroundIdException($"The CampgroundId: {id} does not exist. No Campground found.");
                }

                _logger.LogInformation("Campground found. Attempting to delete...");

                _context.Remove<Campground>(campground);

                await _context.SaveChangesAsync();


                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error deleting the campground from the database.");
                throw;
            }
        }

        public async Task<IEnumerable<Campground>> GetAllCampgroundsAsync()
        {
            try
            {
                _logger.LogInformation("Repository Method called: GetAllCampgroundsAsync()...");
                _logger.LogInformation("Attempting to retrieve all campgrounds...");

                var campgrounds = await _context.Campgrounds.ToListAsync();

                if(campgrounds.Count() > 0)
                {
                    _logger.LogInformation("Campgrounds successfully retrieved...");
                }

                return campgrounds;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error retrieving all Campgrounds from database...");
                throw;
            }
        }

        public async Task<Campground> GetCampgroundByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("CampgroundRepository method called: GetCampgroundByIdAsync()...");
                _logger.LogInformation($"Attempting to retrieve campground: {id}...");

                var campground = await _context.FindAsync<Campground>(id);

                if(campground == null)
                {
                    _logger.LogWarning($"Campground: {id} doesn't exist in the system...");
                    throw new InvalidCampgroundIdException($"No campground with ID: {id} exists. Please check the value.");
                }

                return campground;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve campground with ID: {id}...See exception for details.");
                throw;
            }
        }

        public async Task<Campground> UpdateCampgroundAsync(Campground originalCampground)
        {
            try
            {
                if (originalCampground == null)
                {
                    throw new CampgroundException("Cannot update the record. The given object is null.");
                }
                _logger.LogInformation($"Attempting to update campground with ID: {originalCampground.CampgroundId}...");
                
                _context.Update(originalCampground);
               
                await _context.SaveChangesAsync();
               
                _logger.LogInformation($"Campground with ID: {originalCampground.CampgroundId} successfully updated...");

                return originalCampground;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Campground failed to update. See stack trace for details...");
                throw;
            }
        }

        public async Task<Campground> AddCampgroundAsync(Campground newCampground)
        {
            try
            {
                _logger.LogInformation($"Attempting to add new campgroundwith ID: {newCampground.CampgroundId}...");
                
                _context.Add<Campground>(newCampground);
               
                await _context.SaveChangesAsync();
                
                _logger.LogInformation($"Campground successfully added...");

                return newCampground;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add the campground to the database. See stack trace for details...");
                throw;
            }
        }
    }
}