using Microsoft.IdentityModel.Tokens;
using Microsoft.Testing.Platform.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using App.API.Services;
using App.API.Repositories;
using App.API.Models.Identity;

namespace App.API.Tests.Services
{
    [TestClass]
    public class ProfileServiceTests
    {
        private Mock<ILogger<ProfileService>> _mockLogger;
        private Mock<IProfileRepository> _mockProfileRepository;
        private AppUser _testUser;
        private Profile _testProfile;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILogger<ProfileService>>();
            _mockProfileRepository = new Mock<IProfileRepository>();

            _testUser = new AppUser
            {
                Id = "1",
                Email = "mockuser1@mockeruser.ca",
                AppUserCountry = "Canada",
                AppUserFirstName = "Test",
                AppUserLastName = "User1",
                AppUserProvince = "ON"
            };

            _testProfile = new Profile
            {
                AppUserId = 1,
                ProfileCreatedAt = DateTime.UtcNow,
                ProfileImagePath = "https://picsum.photos/seed/picsum/200",
                ProfileOwner = _testUser
            };
        }

        [TestMethod]
        public Task GetProfileByIdAsyncValidIdReturnsProfile()
        {

        }
    }
}
