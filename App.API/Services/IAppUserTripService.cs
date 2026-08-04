using App.API.Models.Identity;
using App.API.Models.Trips;

namespace App.API.Services
{
    public interface IAppUserTripService
    {
        public Task<Result<AppUserTrip>> AddNewAppUserTripAsync(AppUser appUser, Trip trip);
        public Task<Result<IEnumerable<AppUserTrip>>> RetrieveAllAppUserTripsByUserIdAsync(AppUser appUse);
    }
}
