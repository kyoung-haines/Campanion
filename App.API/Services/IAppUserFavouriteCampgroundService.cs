using App.API.Models.Identity;
using Campanion.Shared.Dtos.AppUserDtos;
using Microsoft.AspNetCore.Identity;
namespace App.API.Services
{
    public interface IAppUserFavouriteCampgroundService
    {
        public Task<Result<bool>> DeleteFavouriteCampgroundAsync(AppUserFavouriteCampgroundDto favCampgroundDto);
        public Task<Result<AppUserFavouriteCampgroundDto>> AddFavouriteCampgroundAsync(AppUserFavouriteCampgroundDto favCampgroundDto);
        public Task<Result<AppUserFavouriteCampgroundsDto>> GetAllFavouriteCampgroundsByUserIdAsync(string appUserId);
        public Task<Result<List<AppUserFavouriteCampgroundDto>>> GetAllAppUsersFavouriteCampgroundDtos();
    }
}
