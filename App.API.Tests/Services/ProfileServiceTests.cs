using App.API.Exceptions.ProfileExceptions;
using App.API.Exceptions.TripExceptions;
using App.API.Models.Identity;
using App.API.Repositories;
using App.API.Services;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace App.API.Tests.Services
{
    [TestClass]
    public class ProfileServiceTests
    {
        private Mock<ILogger<ProfileService>> _mockLogger;
        private Mock<IProfileRepository> _mockProfileRepository;
        private ProfileService _profileService;
        private AppUser _testUser;
        private AppUser _testUserUpdated;
        private Profile _testProfile;
        private Profile _testProfileUpdated;
        private Mock<AppUserTripService> _mockTripService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILogger<ProfileService>>();
            _mockProfileRepository = new Mock<IProfileRepository>();
            _mockTripService = new Mock<AppUserTripService>();
            _profileService = new ProfileService(_mockLogger.Object, _mockProfileRepository.Object, _mockTripService.Object);
            _testUser = new AppUser
            {
                Id = "1",
                Email = "mockuser1@mockeruser.ca",
                AppUserCountry = "Canada",
                AppUserFirstName = "Test",
                AppUserLastName = "User1",
                AppUserProvince = "ON"
            };

            _testUserUpdated = new AppUser
            {
                Id = "1",
                Email = "mockuser1updated@mockeruser.ca",
                AppUserCountry = "Canada",
                AppUserFirstName = "Test",
                AppUserLastName = "User1Updated",
                AppUserProvince = "ON"
            };

            _testProfile = new Profile
            {
                AppUserId = "!",
                ProfileCreatedAt = DateTime.UtcNow,
                ProfileImagePath = "https://picsum.photos/seed/picsum/200",
                ProfileOwner = _testUser
            };

            _testProfileUpdated = new Profile
            {
                AppUserId = "1",
                ProfileCreatedAt = DateTime.UtcNow,
                ProfileImagePath = "https://picsum.photos/seed/picsum/200",
                ProfileOwner = _testUserUpdated
            };
        }

        

        [TestMethod]
        public async Task GetProfileByIdAsyncValidIdReturnsProfile()
        {
            _mockProfileRepository.Setup(repo => repo.GetProfileByIdAsync(1))
                .ReturnsAsync(_testProfile);

            var expectedResult = Result<Profile>.Success(_testProfile);

            var actualResult = await _profileService.GetProfileByProfileIdAsync(1);

            Assert.AreEqual(expectedResult.Data, actualResult.Data);
        }

        [TestMethod]
        public async Task GetProfileByIdAsyncInvalidIdReturnsException()
        {
            _mockProfileRepository.Setup(repo => repo.GetProfileByIdAsync(999))
                .ThrowsAsync(new InvalidProfileIdException(""));

            var expectedResult = Result<Profile>.Failure("Profile not found.");
            var actualResult = await _profileService.GetProfileByProfileIdAsync(999);
            var actualError = actualResult.Error;

            Assert.IsFalse(actualResult.Succeeded);
        }

        [TestMethod]
        public async Task UpdateProfileValidIdUpdatesProfile()
        {
            _mockProfileRepository.Setup(repo => repo.UpdateProfileAsync(_testProfile))
                .ReturnsAsync(_testProfile);

            var profileDto = await _testProfile.ConvertProfileObjectToResponseDto(_testProfile, _testUser);

            var expectedResult = Result<Profile>.Success(_testProfileUpdated);
            var actualResult = await _profileService.UpdateProfileAsync(profileDto);

            Assert.AreEqual(expectedResult.Data, actualResult.Data);
            Assert.AreNotEqual(actualResult.Data, _testProfile);
        }

        [TestMethod]
        public async Task CreateNewProfileAsyncValidIdCreatesProfileAndReturns()
        {
            _mockProfileRepository.Setup(repo => repo.CreateNewProfileAsync(_testUser))
                .ReturnsAsync(_testProfile);

            var expectedResult = Result<Profile>.Success(_testProfile);
            var actualResult = await _profileService.CreateNewProfileAsync(_testUser);

            Assert.AreEqual(expectedResult.Data, actualResult.Data);
        }
    }    
}