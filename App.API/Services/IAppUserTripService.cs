using App.API.Models.Identity;
using Campanion.Shared.Dtos.AppUserDtos;

namespace App.API.Services
{
    public interface IAppUserTripService
    {
        public Task<Result<AppUserTripDto>> AddNewAppUserTripAsync(string appUserId, int tripId);
        public Task<Result<IEnumerable<AppUserTripDto>>> RetrieveAllAppUserTripsByUserIdAsync(string appUserId);
    }
}
