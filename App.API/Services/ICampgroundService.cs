using App.API.Models.Campgrounds;

namespace App.API.Services
{
    public interface ICampgroundService
    {
        public Task<Result<Campground>> DeleteCampgroundAsync(int id);
        public Task<Result<List<Campground>>> GetAllCampgroundsAsync();
        public Task<Campground> GetCampgroundByIdAsync(int id);
        public Task<Campground> UpdateCampgroundAsync(int id);
        public Task AddCampgroundAsync(Campground campground);
        public Task<bool> CampgroundIdIsExistsAsync(int newCampId);
        public Task<bool> IsCampgroundUpdatedAsync(Campground originalCampground, Campground updatedCampground);
        public Task<bool> IsCampgroundAddedAsync(int newCampgroundId);
    }
}
