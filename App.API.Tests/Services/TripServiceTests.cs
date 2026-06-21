using App.API.Dtos.Trips.TripsDtos;
using App.API.Models.Identity;
using App.API.Models.Trips;
using App.API.Repositories;
using App.API.Services;
using Microsoft.AspNetCore.Mvc;
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
    public class TripServiceTests
    {
        private Mock<ILogger> _mockLogger;
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

            _testTrip1 = new Trip
            {
                TripId = 2,
                TripName = "TestTrip02",
                TripStatus = Enums.TripStatus.ACTIVE,
                TripStartDate = DateTime.Now,
                TripEndDate = DateTime.Now.AddDays(10),
                TripCreationDate = DateTime.Now,
                TripAttendees = new List<AppUser>()
            };

            _testTrip1.TripAttendees.AddRange<AppUser>(_testUser1, _testUser2);
        }

        [TestInitialize]
        public void TestInitialize(Mock<ILogger> logger, Mock<ITripRepository> repo, ITripService tripService, TripDto tripDto)
        {
            _mockLogger = logger;
            _mockRepo = repo;
            _tripService = tripService;
            _testTripDto = tripDto;
        }

        [TestMethod]
        public async Task GetAllTripsAsyncReturnsListOfTrips()
        {
            _mockRepo.Setup(repo => repo.GetAllTripsAsync())
                .ReturnsAsync(Result<List<Trip>>.Success(_testTripList));

            var expectedResult = Result<List<Trip>>.Success(_testTripList);
            var expectedDataCount = expectedResult.Data.Count();

            var actualResult = await _tripService.GetAllTripsAsync();
            var actualDataCount = actualResult.Data.Count();

            Assert.AreEqual(expectedDataCount, actualDataCount);
        }
    }
}
