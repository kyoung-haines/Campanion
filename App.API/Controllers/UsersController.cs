using App.API.Data;
using App.API.Models.Identity;
using App.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAppUserService _userService;

        public UsersController(IAppUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("allusers")]
        [AllowAnonymous]
        public async Task<IEnumerable<AppUser>> GetAllAppUsers()
        {
            var allAppUsersResult = await _userService.GetAllAppUsersAsync();

            var allAppUsers = allAppUsersResult.Data;

            return allAppUsers;
        }
    }
}
