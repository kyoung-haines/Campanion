using App.API.Models.Campgrounds;
namespace App.API.Services
{
    public interface IAppUserFavouriteCampgroundService
    {
        public Task<bool> DeleteFavouriteCampgroundAsync(AppUserFavouriteCampground favCampground);
        public Task<AppUserFavouriteCampground> AddFavouriteCampgroundAsync(AppUserFavouriteCampground favCampground);
        public Task<List<AppUserFavouriteCampground>> GetAllFavouriteCampgroundsAsync();
        public Task<AppUserFavouriteCampground> GetFavouriteCampgroundByPrimaryKey(int campId, int userId);

    }
}
