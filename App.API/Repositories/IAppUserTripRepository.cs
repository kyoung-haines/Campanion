using App.API.Models.Identity;
using App.API.Models.Trips;

namespace App.API.Repositories
{
    public interface IAppUserTripRepository
    {
        public Task<AppUserTrip> AddNewAppUserTripAsync(AppUser appUser, Trip trip);
        public Task<IEnumerable<AppUserTrip>> RetrieveAllAppUserTripsByUserIdAsync(string appUserId);
        public Task<AppUserTrip> UpdateAppUserTripAsync(AppUserTrip appUserTrip);
        public Task DeleteAppUserTripAsync(int tripId);
    }
}
