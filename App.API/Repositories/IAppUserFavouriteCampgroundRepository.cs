using App.API.Models.Campgrounds;
namespace App.API.Repositories
{
    public interface IAppUserFavouriteCampgroundRepository
    {
        Task<List<AppUserFavouriteCampground>> GetAllFavouriteCampgroundsAsync(string appUserId);
        Task<AppUserFavouriteCampground> GetFavouriteCampgroundByPrimaryKey(int campId, string userId);
        Task<bool> DeleteFavouriteCampgroundAsync(AppUserFavouriteCampground favouriteCampground);
        Task<AppUserFavouriteCampground> UpdateFavouriteCampgroundAsync(AppUserFavouriteCampground favouriteCampground);
    }
}
