using App.API.Models.Identity;
using App.API.Models.Trips;

namespace App.API.Repositories
{
    public interface IAppUserTripRepository
    {
        public Task<AppUserTrip> AddNewTripAsync(AppUser appUser, Trip trip);
        public Task<IEnumerable<AppUserTrip>> RetrieveAllTripsByUserIdAsync(AppUser appUser, Trip trip);
    }
}
