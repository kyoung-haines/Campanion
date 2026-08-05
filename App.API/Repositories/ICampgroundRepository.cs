using App.API.Models.Campgrounds;

namespace App.API.Repositories
{
    public interface ICampgroundRepository
    {
        public Task<IEnumerable<Campground>> GetAllCampgroundsAsync();
        public Task<Campground> GetCampgroundByIdAsync(int id);
        public Task<Campground> UpdateCampgroundAsync(Campground campground);
        public Task<bool> DeleteCampgroundAsync(int id);
        public Task<Campground> AddCampgroundAsync(Campground newCampground);
    }
}
