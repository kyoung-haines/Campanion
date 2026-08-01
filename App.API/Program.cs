using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using App.API.Data;
using App.API.Seeders;
using App.API.Models;
using App.API.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using App.API.Services;
using App.API.Repositories;

namespace App.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Retrieving and setting connection string
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            // the below AddDbContext will utilize the production SQL database - the proceeding one is in memory for testing
            builder.Services.AddDbContext<CampanionDbContext>(options => options.UseSqlServer(connectionString));
            // builder.Services.AddDbContext<CampanionDbContext>(options => options.UseInMemoryDatabase("TestDb"));

            // Logger Dependencies
            builder.Services.AddScoped<ILogger<AppUserFavouriteCampgroundRepository>, Logger<AppUserFavouriteCampgroundRepository>>();

            // RolesService - added first for potential dependencies further down
            builder.Services.AddScoped<IRolesService, RolesService>();

            // Registering Repository Layer dependencies
            // AppUserRepositoryLayer
            builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();

            // ProfileRepositoryLayer
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

            // CampgroundRepositoryLayer
            builder.Services.AddScoped<ICampgroundRepository, CampgroundRepository>();

            // AppUserFavouriteCampgroundRepository
            builder.Services.AddScoped<IAppUserFavouriteCampgroundRepository, AppUserFavouriteCampgroundRepository>();

            // TripRepository
            builder.Services.AddScoped<ITripRepository, TripRepository>();

            // Registering Service Class dependencies
            //AppUserService
            builder.Services.AddScoped<IAppUserService, AppUserService>();

            // ProfileService
            builder.Services.AddScoped<IProfileService, ProfileService>();

            // CampgroundService
            builder.Services.AddScoped<ICampgroundService, CampgroundService>();

            // AppUserFavouriteCampgroundService
            builder.Services.AddScoped<IAppUserFavouriteCampgroundService, AppUserFavouriteCampgroundService>();

            // TripService
            builder.Services.AddScoped<ITripService, TripService>();

            // AuthService
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Register TokenService - Generates JWT token for auth
            builder.Services.AddScoped<ITokenService, TokenService>();

            // See the anon function and AddRoles<IdentityRole>() additions - required auth for all users
            // see AddAuthorization() middleware 
            builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CampanionDbContext>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });


            var app = builder.Build();

            using var scope = app.Services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var logger = scope.ServiceProvider.GetService<ILogger<AppUser>>()!;
            var context = scope.ServiceProvider.GetService<CampanionDbContext>();
            var profileService = scope.ServiceProvider.GetRequiredService<IProfileService>();
            

            await RoleSeeder.SeedRolesAsync(roleManager);
            await UserSeeder.SeedUsersAsync(userManager, logger);
            await ProfileSeeder.SeedUserProfilesAsync(userManager, context, logger);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                
                //var dbContext = scope.ServiceProvider.GetRequiredService<CampanionDbContext>();
                //dbContext.Database.Migrate();

                //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                //if (!await roleManager.RoleExistsAsync(Roles.Admin))
                //{
                //    await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                //}
                //if (!await roleManager.RoleExistsAsync(Roles.Member))
                //{
                //    await roleManager.CreateAsync(new IdentityRole(Roles.Member));
                //}
            }

            app.UseRouting();

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
