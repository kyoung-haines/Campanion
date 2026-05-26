using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using App.API.Repositories;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using App.API.Models.Campgrounds;
using App.API.Enums;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using App.API.Services;

namespace App.API.Tests.Services
{
    [TestClass]
    public class CampgroundServiceTests
    {
        private Mock<ICampgroundRepository> _campRepo;
        private Mock<ILogger<CampgroundService>> _logger;
        private CampgroundService _campService;
        private Campground _testCampground1;
        private Campground _testCampground2;
        private List<Campground> _testCampgrounds;

        [TestInitialize]
        public void TestInitialize()
        {
            _campRepo = new Mock<ICampgroundRepository>();
            _logger = new Mock<ILogger<CampgroundService>>();
            _campService = new CampgroundService(_logger.Object, _campRepo.Object);
            _testCampgrounds = new List<Campground>();

            _testCampground1 = new Campground
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

            _testCampground2 = new Campground
            {
                CampgroundId = 2,
                CampgroundName = "Test Campground 2",
                CampgroundCity = "Guelph",
                CampgroundCountry = "Canada",
                CampgroundEmail = "testcampground2@test.ca",
                CampgroundProvince = "ON",
                CampgroundStreetName = "Test Road",
                CampgroundPostalCode = "N1M1M1",
                CampgroundPhone = "2222222222",
                CampgroundType = CampgroundType.NATIONAL,
                CampgroundIsOpenYearRound = true,
                CampgroundHasActivities = false,
                CampgroundHasFacilities = false
            };

            _testCampgrounds.AddRange<Campground>(_testCampground1, _testCampground2);

        }

        [TestMethod]
        public async Task DeleteCampgroundAsyncValidIdDeleteReturnsSuccess()
        {
            _campRepo.Setup(repo => repo.DeleteCampgroundAsync(1))
                .ReturnsAsync(Result<bool>.Success(true));

            var expectedResult = Result<bool>.Success(true);

            var actualResult = await _campService.DeleteCampgroundAsync(1);

            Assert.AreEqual(expectedResult.Succeeded, actualResult.Succeeded);
        }

        [TestMethod]
        public async Task DeleteCampgroundAsyncInvalidIdDeleteReturnsFailure()
        {
            _campRepo.Setup(repo => repo.DeleteCampgroundAsync(3))
                .ReturnsAsync(Result<bool>.Failure("Failed to delete the campground from the database."));

            var expectedResult = new Result<bool>();
            var expectedError = expectedResult.Error = "Failed to delete the campground from the database.";
                
            var actualResult = await _campService.DeleteCampgroundAsync(3);
            var actualError = actualResult.Error;

            Assert.AreEqual(expectedError, actualError);
        }

        [TestMethod]
        public async Task GetAllCampgroundsAsyncReturnsAllCampgrounds()
        {
            var successResult = new Result<List<Campground>>();
            successResult.Succeeded = true;
            successResult.Data = _testCampgrounds;

            _campRepo.Setup(repo => repo.GetAllCampgroundsAsync())
                .ReturnsAsync(successResult);

            var expectedResult = successResult;

            var actualResult = await _campService.GetAllCampgroundsAsync();

            Assert.AreEqual(expectedResult.Data, actualResult.Data);
        }
    }
}
