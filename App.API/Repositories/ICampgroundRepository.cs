using App.API.Models.Campgrounds;

namespace App.API.Repositories
{
    public interface ICampgroundRepository
    {
        public Task<IEnumerable<Campground>> GetAllCampgroundsAsync();
        public Task<Campground> GetCampgroundByIdAsync(int id);
        public Task UpdateCampgroundAsync(Campground campground);
        public Task DeleteCampground(int id);
    }
}
