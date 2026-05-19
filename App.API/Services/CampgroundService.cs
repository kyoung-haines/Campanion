using App.API.Models.Campgrounds;

namespace App.API.Services
{
    public interface CampgroundService
    {

        Task<bool> CampgroundIsAdded(IEnumerable<Campground> campgrounds)
        {

        }
    }
}