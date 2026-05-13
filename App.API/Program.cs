using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using App.API.Data;
using App.API.Seeders;
using App.API.Models;
using App.API.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            builder.Services.AddDbContext<CampanionDbContext>(options => options.UseSqlServer(connectionString));

            // Adding Identity related config
            // See the anon function and AddRoles<IdentityRole>() additions - required auth for all users
            // see AddAuthorization() middleware 
            builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CampanionDbContext>();

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                
                using var scope = app.Services.CreateScope();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await RoleSeeder.SeedRolesAsync(roleManager);
                await UserSeeder.SeedUsersAsync(userManager);

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

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
