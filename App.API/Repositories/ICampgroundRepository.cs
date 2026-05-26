using App.API.Models.Campgrounds;

namespace App.API.Repositories
{
    public interface ICampgroundRepository
    {
        public Task<Result<List<Campground>>> GetAllCampgroundsAsync();
        public Task<Result<Campground>> GetCampgroundByIdAsync(int id);
        public Task UpdateCampgroundAsync(Campground campground);
        public Task<Result<bool>> DeleteCampgroundAsync(int id);
        public Task AddCampgroundAsync(Campground newCampground);
    }
}
