using App.API.Models.Campgrounds;
using App.API.Models.Identity;
using App.API.Models.Social;
using App.API.Models.Trips;
using Microsoft.EntityFrameworkCore;

namespace App.API.Data
{
    public class CampanionDbContext : DbContext
    {
        public CampanionDbContext(DbContextOptions<CampanionDbContext> options) : base(options) { }

        public DbSet<Campground> Campgrounds { get; set; }
        public DbSet<AppUserFavouriteCampground> AppUserFavouriteCampgrounds { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<AppUserTrip> AppUserTrips { get; set; }
        public DbSet<TripCampground> TripCampgrounds { get; set; }
    }
}
