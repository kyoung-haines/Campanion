using App.API.Enums;
using App.API.Exceptions.AppUserExceptions;
using App.API.Models.Campgrounds;
using App.API.Models.Identity;
using App.API.Repositories;
using App.API.Services;
using Campanion.Shared.Dtos.AppUserDtos;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.API.Tests.Services
{
    [TestClass]
    public class AppUserFavouriteCampgroundServiceTests
    {
        Mock<IAppUserFavouriteCampgroundRepository> _repo;
        Mock<ILogger<AppUserFavouriteCampgroundService>> _logger;
        AppUserFavouriteCampgroundService _appUserFavouriteCampgroundService;
        Mock<ICampgroundRepository> _mockCampgroundRepository;
        Mock<ILogger<CampgroundService>> _mockCampgroundServiceLogger;
        CampgroundService _campgroundService;
        AppUserFavouriteCampground _favCampground = new();
        AppUserFavouriteCampgroundDto _favCampgroundDto = new();
        List<AppUserFavouriteCampgroundDto> _favCampgroundDtos = new();

        AppUser _testUser = new AppUser
        {
            AppUserFirstName = "Test",
            AppUserLastName = "Tester",
            AppUserCountry = "Canada",
            AppUserProvince = "ON"
        };

        Campground _testCampground = new Campground 
        {
            CampgroundId = 1,
            CampgroundName = "Test Campground 1",
            CampgroundCity = "Guelph",
            CampgroundCountry = "Canada",
            CampgroundEmail = "testcampground1@test.ca",
            CampgroundProvince = "ON",
            CampgroundStreetName = "Test Street",
            CampgroundPostalCode = "N1G4V6",
            CampgroundPhone = "1111111111",
            CampgroundType = CampgroundType.PROVINCIAL,
            CampgroundIsOpenYearRound = true,
            CampgroundHasActivities = false,
            CampgroundHasFacilities = false
        };

        List<AppUserFavouriteCampground> _favCampgrounds = new List<AppUserFavouriteCampground>();

        [TestInitialize]
        public void TestInitialize()
        {
            _repo = new Mock<IAppUserFavouriteCampgroundRepository>();
            _logger = new Mock<ILogger<AppUserFavouriteCampgroundService>>();
            _mockCampgroundRepository = new Mock<ICampgroundRepository>();
            _mockCampgroundServiceLogger = new Mock<ILogger<CampgroundService>>();
            _campgroundService = new CampgroundService(_mockCampgroundServiceLogger.Object, _mockCampgroundRepository.Object);
            _appUserFavouriteCampgroundService = new AppUserFavouriteCampgroundService(_logger.Object, _repo.Object, _campgroundService);

            _favCampground.AppUserId = "1";
            _favCampground.CampgroundId = 1;
            _favCampground.FavouritedAt = DateTime.Now;

            _favCampgrounds.Add(_favCampground);

            _favCampgroundDto = _favCampground.ToDto(_favCampground);
        }

        [TestMethod]
        public async Task DeleteFavouriteCampgroundAsyncReturnsSuccess()
        {
            _repo.Setup(repo => repo.DeleteFavouriteCampgroundAsync(_favCampground))
                .ReturnsAsync(true);

            var expectedResult = Result<bool>.Success(true);

            var actualResult = await _appUserFavouriteCampgroundService.DeleteFavouriteCampgroundAsync(_favCampgroundDto);

            Assert.AreEqual(expectedResult.Succeeded, actualResult.Succeeded);
        }

        [TestMethod]
        public async Task DeleteFavouriteCampgroundAsyncReturnsFailure()
        {
            _repo.Setup(repo => repo.DeleteFavouriteCampgroundAsync(_favCampground))
                .ReturnsAsync(false);

            var expectedError = "Failed to delete favourite.";

            var actualResult = await _appUserFavouriteCampgroundService.DeleteFavouriteCampgroundAsync(_favCampgroundDto);
            var actualError = actualResult.Error;
            Assert.AreEqual(expectedError, actualError);
        }

        [TestMethod]
        public async Task GetAllFavouriteCampgroundsAsyncValidIdReturnSuccess()
        {
            _repo.Setup(repo => repo.GetAllFavouriteCampgroundsByUserIdAsync("1"))
                .ReturnsAsync(_favCampgrounds);

            var expectedResult = Result<List<AppUserFavouriteCampgroundDto>>.Success(_favCampgroundDtos);

            var actualResult = await _appUserFavouriteCampgroundService.GetAllFavouriteCampgroundsByUserIdAsync("1");

            Assert.AreEqual(expectedResult.Data, actualResult.Data);
        }

        [TestMethod]
        public async Task GetAllFavouriteCampgroundsAsyncInvalidIdReturnsFailure()
        {
            var appUserId = "99"; //invalid ID

            _repo.Setup(repo => repo.GetAllFavouriteCampgroundsByUserIdAsync(appUserId))
                .ReturnsAsync(_favCampgrounds);

            int expectedCount = 0;
            var result = await _appUserFavouriteCampgroundService.GetAllFavouriteCampgroundsByUserIdAsync(appUserId);
            int actualCount = result.Data.ToList().Count();

            Assert.HasCount(expectedCount, actualCount);
    
        }
    }
}
