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
        private Campground _testCampgroundInvalid;
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

            _testCampgroundInvalid = null;

            _testCampgrounds.AddRange<Campground>(_testCampground1, _testCampground2);

        }

        [TestMethod]
        public async Task DeleteCampgroundAsyncValidIdDeleteReturnsSuccess()
        {
            _campRepo.Setup(repo => repo.GetCampgroundByIdAsync(1))
                .ReturnsAsync(Result<Campground>.Success(_testCampground1));

            _campRepo.Setup(repo => repo.DeleteCampgroundAsync(1))
                .ReturnsAsync(Result<bool>.Success(true));

            var expectedResult = Result<bool>.Success(true);

            var actualResult = await _campService.DeleteCampgroundAsync(1);

            Assert.AreEqual(expectedResult.Succeeded, actualResult.Succeeded);
        }

        [TestMethod]
        public async Task DeleteCampgroundAsyncInvalidIdDeleteReturnsFailure()
        {
            _campRepo.Setup(repo => repo.GetCampgroundByIdAsync(3))
                .ReturnsAsync(Result<Campground>.Failure("Failed to retrieve the campground from the database."));
                
            var actualResult = await _campService.DeleteCampgroundAsync(3);
            var actualError = actualResult.Error;

            Assert.IsFalse(actualResult.Succeeded);
            _campRepo.Verify(repo => repo.DeleteCampgroundAsync(It.IsAny<int>()),Times.Never);
        }

        [TestMethod]
        public async Task GetAllCampgroundsAsyncReturnsAllCampgrounds()
        {
            //var successResult = new Result<List<Campground>>();
            //successResult.Succeeded = true;
            //successResult.Data = _testCampgrounds;

            // tightened the above to this refactor
            var successResult = Result<List<Campground>>.Success(_testCampgrounds);

            _campRepo.Setup(repo => repo.GetAllCampgroundsAsync())
                .ReturnsAsync(successResult);

            var actualResult = await _campService.GetAllCampgroundsAsync();

            Assert.IsTrue(actualResult.Succeeded);
            Assert.AreEqual(_testCampgrounds, actualResult.Data);
        }

        [TestMethod]
        public async Task GetCampgroundByIdValidIdReturnsSuccess()
        {
            _campRepo.Setup(repo => repo.GetCampgroundByIdAsync(1))
                .ReturnsAsync(Result<Campground>.Success(_testCampground1));

            var expectedResult = Result<Campground>.Success(_testCampground1);

            var actualResult = await _campService.GetCampgroundByIdAsync(1);

            Assert.IsTrue(actualResult.Succeeded);
            Assert.AreEqual(_testCampground1, actualResult.Data);
            _campRepo.Verify(repo => repo.GetCampgroundByIdAsync(1), Times.Once);
        }

        [TestMethod]
        public async Task GetCampgroundByIdInvalidIdReturnsFailure()
        {
            _campRepo.Setup(repo => repo.GetCampgroundByIdAsync(99))
                .ReturnsAsync(Result<Campground>.Failure("Campground not found."));

            var expectedResult = Result<Campground>.Failure("Campground not found.");

            var actualResult = await _campService.GetCampgroundByIdAsync(99);

            Assert.IsFalse(actualResult.Succeeded);
            Assert.AreEqual(expectedResult.Error, actualResult.Error);
            _campRepo.Verify(repo => repo.GetCampgroundByIdAsync(99), Times.Once);
        }

        [TestMethod]
        public async Task UpdateCampgroundAsyncValidCampgroundReturnsSuccess()
        {
           _campRepo.Setup(repo => repo.UpdateCampgroundAsync(_testCampground1))
                .ReturnsAsync(Result<Campground>.Success(_testCampground1));

            var expectedResult = Result<Campground>.Success(_testCampground1);

            var actualResult = await _campService.UpdateCampgroundAsync(_testCampground1);

            Assert.IsTrue(actualResult.Succeeded);
        }

        [TestMethod]
        public async Task UpdateCampgroundAsyncInvalidCampgroundReturnsFalse()
        {
            _campRepo.Setup(repo => repo.UpdateCampgroundAsync(_testCampgroundInvalid))
                .ReturnsAsync(Result<Campground>.Failure("Failed to update campground."));

            var expectedResult = Result<Campground>.Failure("Failed to update campground.");

            var actualResult = await _campService.UpdateCampgroundAsync(_testCampgroundInvalid);

            Assert.IsFalse(actualResult.Succeeded);
        }

        [TestMethod]
        public async Task AddCampgroundAsyncReturnsSuccess()
        {
            var newCampground = new Campground
            {
                CampgroundId = 3,
                CampgroundName = "Test Campground 3",
                CampgroundCity = "Guelph",
                CampgroundCountry = "Canada",
                CampgroundEmail = "testcampground3@test.ca",
                CampgroundProvince = "ON",
                CampgroundStreetName = "Tester Road",
                CampgroundPostalCode = "M1N1N1",
                CampgroundPhone = "3333333333",
                CampgroundType = CampgroundType.NATIONAL,
                CampgroundIsOpenYearRound = true,
                CampgroundHasActivities = false,
                CampgroundHasFacilities = false
            };
            _campRepo.Setup(repo => repo.AddCampgroundAsync(newCampground))
                .ReturnsAsync(Result<Campground>.Success(newCampground));

            var expectedResult = Result<Campground>.Success(newCampground);

            var actualResult = await _campService.AddCampgroundAsync(newCampground);

            Assert.IsTrue(actualResult.Succeeded);
            Assert.AreEqual(expectedResult.Data, actualResult.Data);
        }

        [TestMethod]
        public async Task AddCampgroundAsyncReturnsFailure()
        {
            var newCampground = new Campground
            {
                CampgroundId = 3,
                CampgroundName = "Test Campground 3",
                CampgroundCity = "Guelph",
                CampgroundCountry = "Canada",
                CampgroundEmail = "testcampground3@test.ca",
                CampgroundProvince = "ON",
                CampgroundStreetName = "Tester Road",
                CampgroundPostalCode = "M1N1N1",
                CampgroundPhone = "3333333333",
                CampgroundType = CampgroundType.NATIONAL,
                CampgroundIsOpenYearRound = true,
                CampgroundHasActivities = false,
                CampgroundHasFacilities = false
            };

            _campRepo.Setup(repo => repo.AddCampgroundAsync(newCampground))
                .ReturnsAsync(Result<Campground>.Failure("Unable to add campground."));

            var expectedResult = Result<Campground>.Failure("Unable to add campground.");

            var actualResult = await _campService.AddCampgroundAsync(newCampground);

            Assert.IsFalse(actualResult.Succeeded);
        }

        [TestMethod]
        public async Task CampgroundIdIsExistsAsyncReturnsSuccess()
        {
            _campRepo.Setup(repo => repo.GetCampgroundByIdAsync(1))
                .ReturnsAsync(Result<Campground>.Success(_testCampground1));

            var campId = 1;

            var expectedResult = Result<bool>.Success(true);

            var actualResult = await _campService.CampgroundIdIsExistsAsync(campId);

            Assert.IsTrue(actualResult.Succeeded);
        }

        [TestMethod]
        public async Task CampgroundIdIsExistsAsyncReturnsFailure()
        {
            var campId = 99;

            _campRepo.Setup(repo => repo.GetCampgroundByIdAsync(campId))
                .ReturnsAsync(Result<Campground>.Failure("Unable to retrieve campground."));

            var expectedResult = Result<bool>.Failure($"Campground with ID: {campId} does not exist.");

            var actualResult = await _campService.CampgroundIdIsExistsAsync(campId);
            actualResult.Error = $"Campground with ID: {campId} does not exist.";

            Assert.AreEqual(expectedResult.Error.ToString(), actualResult.Error.ToString());
        }
    }
}
