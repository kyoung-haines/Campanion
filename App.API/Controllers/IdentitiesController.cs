using App.API.Data;
using App.API.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentitiesController : ControllerBase
    {
        private readonly CampanionDbContext _context;
        public IdentitiesController(CampanionDbContext context)
        {
            _context = context;
        }

    //    [HttpGet]
    //    public async Task<IEnumerable<AppUser>> GetAllAppUsers()
    //    {
    //        var allAppUsers = await _context.AppUsers.ToListAsync();

    //        return allAppUsers;
    //    }
    }
}
