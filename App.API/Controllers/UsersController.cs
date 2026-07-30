using App.API.Data;
using App.API.Models;
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

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("allusers")]
        public async Task<IEnumerable<AppUser>> GetAllAppUsersAsync()
        {
            var allAppUsersResult = await _userService.GetAllAppUsersAsync();

            var allAppUsers = allAppUsersResult.Data;

            return allAppUsers;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("alladminusers")]
        public async Task<IEnumerable<AppUser>> GetAllAdminUsersAsync()
        {
            var allAdminsResult = await _userService.GetAllAppAdminsAsync();

            var allAdmins = allAdminsResult.Data;

            return allAdmins;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("allregularusers")]
        public async Task<IEnumerable<AppUser>> GetAllRegularAppUsersAsync()
        {
            var allRegularAppUsersResult = await _userService.GetAllRegularAppUsersAsync();
            var allRegularAppUsersList = allRegularAppUsersResult.Data;

            return allRegularAppUsersList;
        }

        [Authorize]
        [HttpGet("users/{userId}")]
        public async Task<AppUserDto> GetAppUserByIdAsync(string userId)
        {
            var userResult = await _userService.GetAppUserByIdAsync(userId);
            var user = userResult.Data;
            var userDto = new AppUserDto();

            return userDto;
        }
    }
}
