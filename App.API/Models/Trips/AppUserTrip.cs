using App.API.Models.Identity;
using Campanion.Shared.Dtos.AppUserDtos;
using Microsoft.EntityFrameworkCore;


namespace App.API.Models.Trips
{
    [PrimaryKey(nameof(TripId), nameof(AppUserId))]
    public class AppUserTrip
    {
        public int TripId { get; set; }
        public string AppUserId { get; set; }
        public DateTime AppUserTripeAddedAt { get; set; } = DateTime.Now;

        // NAVIGATIONAL PROPERTIES
        public AppUser AppUser { get; set; }
        public Trip Trip { get; set; }

        // Helper Methods
        // DTO Conversion
        public async Task<AppUserTripDto> AppUserTripToDTOAsync(AppUserTrip appUserTrip)
        {
            var appUserTripDto = new AppUserTripDto
            {
                AppUserIdDto = appUserTrip.AppUserId,
                TripIdDto = appUserTrip.TripId.ToString()
            };

            return appUserTripDto;
        }
    }
}
