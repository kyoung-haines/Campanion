using App.API.Models.Campgrounds;

namespace App.API.Services
{
    public interface ICampgroundService
    {
        Task<bool> CampgroundIsAdded(IEnumerable<Campground> campgrounds);
    }
}
