using App.API.Enums;
using App.API.Exceptions.AppUserExceptions;
using App.API.Models.Campgrounds;
using App.API.Models.Identity;
using App.API.Repositories;
using App.API.Services;
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
    public class AppUserFavouriteCampgroundTests
    {
        Mock<IAppUserFavouriteCampgroundRepository> _repo;
        Mock<ILogger> _logger;
        AppUserFavouriteCampgroundService _service;

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

        AppUserFavouriteCampground _favCampground = new AppUserFavouriteCampground 
        {
            AppUserId = 1,
            CampgroundId = 1,
            FavouritedAt = DateTime.UtcNow
        };

        [TestInitialize]
        public void TestInitialize()
        {
            _repo = new Mock<IAppUserFavouriteCampgroundRepository>();
            _logger = new Mock<ILogger>();
            _service = new AppUserFavouriteCampgroundService(_logger.Object, _repo.Object);

            _favCampgrounds.Add(_favCampground);
        }

        [TestMethod]
        public async Task DeleteFavouriteCampgroundAsyncReturnsSuccess()
        {
            _repo.Setup(repo => repo.DeleteFavouriteCampgroundAsync(_favCampground))
                .ReturnsAsync(Result<bool>.Success(true));

            var expectedResult = Result<bool>.Success(true);

            var actualResult = await _service.DeleteFavouriteCampgroundAsync(_favCampground);

            Assert.AreEqual(expectedResult.Succeeded, actualResult.Succeeded);
        }

        [TestMethod]
        public async Task DeleteFavouriteCampgroundAsyncReturnsFailure()
        {
            _repo.Setup(repo => repo.DeleteFavouriteCampgroundAsync(_favCampground))
                .ReturnsAsync(Result<bool>.Failure("Failed to delete favourite."));

            var expectedError = "Failed to delete favourite.";

            var actualResult = await _service.DeleteFavouriteCampgroundAsync(_favCampground);
            var actualError = actualResult.Error;
            Assert.AreEqual(expectedError, actualError);
        }

        [TestMethod]
        public async Task GetAllFavouriteCampgroundsAsyncValidIdReturnSuccess()
        {
            _repo.Setup(repo => repo.GetAllFavouriteCampgroundsAsync(1))
                .ReturnsAsync(Result<List<AppUserFavouriteCampground>>.Success(_favCampgrounds));

            var expectedResult = Result<List<AppUserFavouriteCampground>>.Success(_favCampgrounds);

            var actualResult = await _service.GetAllFavouriteCampgroundsAsync(1);

            Assert.AreEqual(expectedResult.Data, actualResult.Data);
        }

        [TestMethod]
        public async Task GetAllFavouriteCampgroundsAsyncInvalidIdReturnsFailure()
        {
            var appUserId = 99; //invalid ID

            _repo.Setup(repo => repo.GetAllFavouriteCampgroundsAsync(appUserId))
                .ReturnsAsync(Result<List<AppUserFavouriteCampground>>.Failure($"Invalid UserID. Verify that ID: {appUserId} exists."));

            var expectedResult = Result<List<AppUserFavouriteCampground>>.Failure($"Invalid UserID. Verify that ID: {appUserId} exists.");

            var actualResult = await _service.GetAllFavouriteCampgroundsAsync(appUserId);

            Assert.AreEqual(expectedResult.Error.ToString(), actualResult.Error.ToString());
    
        }
    }
}
