using App.API.Models.Campgrounds;
namespace App.API.Services
{
    public interface IAppUserFavouriteCampgroundService
    {
        public Task<Result<bool>> DeleteFavouriteCampgroundAsync(AppUserFavouriteCampground favCampground);
        public Task<Result<AppUserFavouriteCampground>> AddFavouriteCampgroundAsync(AppUserFavouriteCampground favCampground);
        public Task<Result<List<AppUserFavouriteCampground>>> GetAllFavouriteCampgroundsAsync(string appUserId);
        public Task<Result<AppUserFavouriteCampground>> GetFavouriteCampgroundByPrimaryKey(int campId, string userId);

    }
}
