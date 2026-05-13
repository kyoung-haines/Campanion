using App.API.Models.Campgrounds;
using App.API.Models.Identity;
using App.API.Models.Social;
using App.API.Models.Trips;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.API.Data
{
    public class CampanionDbContext : IdentityDbContext<AppUser>
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CampanionDB;Trusted_Connection=True;MultipleActiveResultSets=true;")
                    .UseSeeding((context, _) =>
                    {
                        var testAppUser = new AppUser
                        {
                            AppUserCountry = "Canada",
                            AppUserProvince = "Ontario",
                            AppUserFirstName = "Test",
                            AppUserLastName = "Tester"
                        };

                        context.Set<AppUser>().Add(testAppUser);
                        context.SaveChanges();
                    })
                    .UseAsyncSeeding(async (context, _, cancellationToken) =>
                    {
                        var testAppUser = new AppUser
                        {
                            AppUserCountry = "Canada",
                            AppUserProvince = "Ontario",
                            AppUserFirstName = "Test",
                            AppUserLastName = "Tester"
                        };

                        var tUser = await context.Set<AppUser>().AddAsync(testAppUser);
                        await context.SaveChangesAsync(cancellationToken);

                    });
            }
        }
    }
}
