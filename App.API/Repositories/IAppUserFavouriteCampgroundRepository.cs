using App.API.Models.Campgrounds;
namespace App.API.Repositories
{
    public interface IAppUserFavouriteCampgroundRepository
    {
        Task<Result<List<AppUserFavouriteCampground>>> GetAllFavouriteCampgroundsAsync(int appUserId);
        Task<Result<AppUserFavouriteCampground>> GetFavouriteCampgroundByPrimaryKey(int campId, int userId);

        Task<Result<bool>> DeleteFavouriteCampgroundAsync(AppUserFavouriteCampground favouriteCampground);
    }
}
