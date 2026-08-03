using App.API.Models.Campgrounds;
namespace App.API.Repositories
{
    public interface IAppUserFavouriteCampgroundRepository
    {
        Task<Result<List<AppUserFavouriteCampground>>> GetAllFavouriteCampgroundsAsync(string appUserId);
        Task<Result<AppUserFavouriteCampground>> GetFavouriteCampgroundByPrimaryKey(int campId, string userId);

        Task<Result<bool>> DeleteFavouriteCampgroundAsync(AppUserFavouriteCampground favouriteCampground);
    }
}
