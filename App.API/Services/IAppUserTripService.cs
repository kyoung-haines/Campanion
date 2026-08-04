using App.API.Models.Identity;
using App.API.Models.Trips;

namespace App.API.Services
{
    public interface IAppUserTripService
    {
        public Task<Result<AppUserTrip>> AddNewAppUserTripAsync(string appUserId, int tripId);
        public Task<Result<IEnumerable<AppUserTrip>>> RetrieveAllAppUserTripsByUserIdAsync(string appUserId);
    }
}
