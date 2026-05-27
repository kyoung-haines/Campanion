using App.API.Models.Campgrounds;

namespace App.API.Services
{
    public interface ICampgroundService
    {
        public Task<Result<Campground>> DeleteCampgroundAsync(int id);
        public Task<Result<List<Campground>>> GetAllCampgroundsAsync();
        public Task<Result<Campground>> GetCampgroundByIdAsync(int id);
        public Task<Result<Campground>> UpdateCampgroundAsync(int id);
        public Task<Result<Campground>> AddCampgroundAsync(Campground campground);
        public Task<Result<bool>> CampgroundIdIsExistsAsync(int newCampId);
        public Task<Result<bool>> IsCampgroundUpdatedAsync(Campground originalCampground, Campground updatedCampground);
        public Task<Result<bool>> IsCampgroundAddedAsync(int newCampgroundId);
    }
}
