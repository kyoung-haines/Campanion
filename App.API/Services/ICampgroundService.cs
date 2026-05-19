using App.API.Models.Campgrounds;

namespace App.API.Services
{
    public interface ICampgroundService
    {
        public Task<bool> CampgroundIdIsExistsAsync(int newCampId);
        public Task<bool> IsCampgroundUpdatedAsync(Campground originalCampground, Campground updatedCampground);
        public Task DeleteCampground(int id);
        public Task<IEnumerable<Campground>> GetAllCampgroundsAsync();
        public Task<Campground> GetCampgroundByIdAsync(int id);
        public Task UpdateCampgroundAsync(int id);
        public Task AddCampgroundAsync(Campground campground);
        public Task<bool> IsCampgroundAddedAsync(int newCampgroundId);
    }
}
