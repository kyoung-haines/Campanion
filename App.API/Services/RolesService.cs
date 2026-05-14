using App.API.Models;

namespace App.API.Services
{
    public class RolesService : IRolesService
    {
        public async Task<List<string>> GetAllRolesAsync()
        {
            var roles = new List<string> { "Admin", "User" };
            
            return roles;
        }
    }
}
