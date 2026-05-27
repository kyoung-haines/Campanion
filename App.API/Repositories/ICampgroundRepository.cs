using App.API.Models.Campgrounds;

namespace App.API.Repositories
{
    public interface ICampgroundRepository
    {
        public Task<Result<List<Campground>>> GetAllCampgroundsAsync();
        public Task<Result<Campground>> GetCampgroundByIdAsync(int id);
        public Task<Result<Campground>> UpdateCampgroundAsync(Campground campground);
        public Task<Result<bool>> DeleteCampgroundAsync(int id);
        public Task<Result<Campground>> AddCampgroundAsync(Campground newCampground);
    }
}
