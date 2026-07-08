using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.API.Models.Identity;
using App.API.Repositories;
using App.API.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace App.API.Tests.Services
{
    [TestClass]
    public class AppUserServiceTests
    {
        private Mock<ILogger<AppUserService>> _mockLogger;
        private Mock<IAppUserRepository> _mockRepository;
        private IAppUserService _userService;
        private List<AppUser> _users = new List<AppUser>();
        AppUser _mockUser1;
        AppUser _mockUser2;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILogger<AppUserService>>();
            _mockRepository = new Mock<IAppUserRepository>();
            _userService = new AppUserService(_mockLogger.Object, _mockRepository.Object);
            
            _mockUser1 = new AppUser
            {
                Id = "1",
                Email = "mockuser1@mockeruser.ca",
                AppUserCountry = "Canada", 
                AppUserFirstName = "Test",
                AppUserLastName = "User1",
                AppUserProvince = "ON"
            };

            _mockUser2 = new AppUser
            {
                Id = "2",
                Email = "mockuser2@mockeruser.ca",
                AppUserCountry = "Canada",
                AppUserFirstName = "Test",
                AppUserLastName = "User2",
                AppUserProvince = "ON"
            };

            _users.Add(_mockUser1);
            _users.Add(_mockUser2);
            
        }

        [TestMethod]
        public async Task GetAllAppUsersAsync()
        {
            var expectedResult = Result<List<AppUser>>.Success(_users);
            var expectedCount = expectedResult.Data.Count();

            _mockRepository.Setup(repo => repo.GetAllAppUsersAsync())
                .ReturnsAsync(_users);

            var actualResult = await _userService.GetAllAppUsersAsync();
            var actualCount = actualResult.Data.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
