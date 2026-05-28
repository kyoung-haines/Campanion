using App.API.Enums;
using App.API.Models.Campgrounds;
using App.API.Models.Identity;
using App.API.Repositories;
using App.API.Services;
using Microsoft.Extensions.Logging;
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
    }
}
