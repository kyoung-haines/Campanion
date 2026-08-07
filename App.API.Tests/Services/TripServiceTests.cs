using Campanion.Shared.Dtos.TripDtos;
using App.API.Models.Identity;
using App.API.Models.Trips;
using App.API.Repositories;
using App.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.API.Tests.Services
{
    [TestClass]
    public class TripServiceTests
    {
        private Mock<ILogger<TripService>> _mockLogger;
        private Mock<ITripRepository> _mockRepo;
        private ITripService _tripService;
        private Trip _testTrip1;
        private Trip _testTrip2;
        private List<Trip> _testTripList;
        private TripDto _testTripDto;
        private List<TripDto> _testTripDtoList;
        private AppUser _testUser1;
        private AppUser _testUser2;

        public TripServiceTests()
        {
            _testUser1 = new AppUser
            {
                Id = "1",
                Email = "mockuser1@mockeruser.ca",
                AppUserCountry = "Canada",
                AppUserFirstName = "Test",
                AppUserLastName = "User1",
                AppUserProvince = "ON"
            };

            _testUser2 = new AppUser
            {
                Id = "2",
                Email = "mockuser2@mockeruser.ca",
                AppUserCountry = "Canada",
                AppUserFirstName = "Test",
                AppUserLastName = "User2",
                AppUserProvince = "ON"
            };

            _testTrip1 = new Trip
            {
                TripId = 1,
                TripName = "TestTrip01",
                TripStatus = Enums.TripStatus.ACTIVE,
                TripStartDate = DateTime.Now,
                TripEndDate = DateTime.Now.AddDays(8),
                TripCreationDate = DateTime.Now,
                TripAttendees = new List<AppUser>()
            };

            _testTrip1.TripAttendees.AddRange<AppUser>(_testUser1, _testUser2);

            _testTrip2 = new Trip
            {
                TripId = 2,
                TripName = "TestTrip02",
                TripStatus = Enums.TripStatus.ACTIVE,
                TripStartDate = DateTime.Now,
                TripEndDate = DateTime.Now.AddDays(10),
                TripCreationDate = DateTime.Now,
                TripAttendees = new List<AppUser>()
            };

            _testTrip2.TripAttendees.AddRange<AppUser>(_testUser1, _testUser2);

            _testTripList = new List<Trip> { _testTrip1, _testTrip2 };

            _mockLogger = new Mock<ILogger<TripService>>();
            _mockRepo = new Mock<ITripRepository>();
            _tripService = new TripService(_mockLogger.Object, _mockRepo.Object);
            _testTripDto = new TripDto();
            _testTripDtoList = new List<TripDto>();
        }
        
        [TestInitialize]
        public void TestInitialize()
        {
            
        }

        [TestMethod]
        public async Task GetAllTripsAsyncReturnsListOfTrips()
        {
            _mockRepo.Setup(repo => repo.GetAllTripsAsync())
                .ReturnsAsync(_testTripList);

            var expectedResult = Result<List<Trip>>.Success(_testTripList);
            var expectedDataCount = expectedResult.Data.Count();

            var actualResult = await _tripService.GetAllTripsAsync();
            var actualDataCount = actualResult.Data.Count();

            Assert.AreEqual(expectedDataCount, actualDataCount);
        }

        [TestMethod]
        public async Task GetAllTripsAsyncEmptyListReturnsEmptyList()
        {
            var emptyTripList = new List<Trip>();
            _mockRepo.Setup(repo => repo.GetAllTripsAsync())
                .ReturnsAsync(emptyTripList);

            var expectedResult = emptyTripList;

            var actualResult = await _tripService.GetAllTripsAsync();

            Assert.AreEqual(expectedResult.Count(), actualResult.Data.Count());
        }

        [TestMethod]
        public async Task DeleteTripValidIdDeletesTrip()
        {
            _mockRepo.Setup(repo => repo.GetTripByTripIdAsync(1))
                .ReturnsAsync(_testTrip1);
            _mockRepo.Setup(repo => repo.DeleteTripAsync(1))
                .ReturnsAsync(true);

            var expectedResult = Result<bool>.Success(true);

            var actualResult = await _tripService.DeleteTripAsync(1);

            Assert.IsTrue(expectedResult.Succeeded);
            Assert.IsTrue(actualResult.Succeeded);
        }

        [TestMethod]
        public async Task DeleteTripInvalidIdReturnsFailure()
        {
            _mockRepo.Setup(repo => repo.DeleteTripAsync(999))
                .ReturnsAsync(false);

            var expectedResult = Result<bool>.Failure("Failed to delete trip...");
            var expectedSucceeded = expectedResult.Succeeded;

            var actualResult = await _tripService.DeleteTripAsync(999);
            var actualSucceeded = actualResult.Succeeded;

            Assert.IsFalse(expectedSucceeded);
            Assert.IsFalse(actualSucceeded);
        }

        [TestMethod]
        public async Task CreateTripValidObjectReturnsSuccess()
        {

        }

        [TestMethod]
        public async Task CreateTripInvalidObjectReturnsFailure()
        {

        }
    }
}
