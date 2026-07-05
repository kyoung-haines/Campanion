using App.API.Data;
using App.API.Dtos.Trips.TripsDtos;
using App.API.Models.Identity;
using App.API.Models.Trips;
using App.API.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.API.Tests.Repositories
{
    [TestClass]
    public class TripRepositoryTests
    {
        private Mock<ILogger<ITripRepository>> _mockLogger;
        private Mock<CampanionDbContext> _mockCampanionDbContext;
        private ITripRepository _tripRepository;

        private Trip _testTrip1;
        private Trip _testTrip2;
        private List<Trip> _testTripList;
        private AppUser _testUser1;
        private AppUser _testUser2;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILogger<ITripRepository>>();
            _mockCampanionDbContext = new Mock<CampanionDbContext>();
            _tripRepository = new TripRepository(_mockLogger.Object, _mockCampanionDbContext.Object);

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
        }

        [TestMethod]
        public async Task GetTripByIdValidIdReturnsResultWithTrip()
        {
            _mockCampanionDbContext.Setup(repo => repo.Trips.FindAsync(1))
                .ReturnsAsync(_testTrip1);

            var expectedResult = _testTrip1;

            var actualResult = await _tripRepository.GetTripByTripIdAsync(1);

            //Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
