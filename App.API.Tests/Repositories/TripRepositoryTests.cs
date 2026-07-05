using App.API.Data;
using App.API.Dtos.Trips.TripsDtos;
using App.API.Exceptions.TripExceptions;
using App.API.Models.Identity;
using App.API.Models.Trips;
using App.API.Repositories;
using Microsoft.EntityFrameworkCore;
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
        private CampanionDbContext _dbContext;
        private ITripRepository _tripRepository;
        private Mock<ILogger<TripRepository>> _mockLogger;

        private Trip _testTrip1;
        private Trip _testTrip2;
        private List<Trip> _testTripList;
        private AppUser _testUser1;
        private AppUser _testUser2;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<CampanionDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _dbContext = new CampanionDbContext(options);
            _mockLogger = new Mock<ILogger<TripRepository>>();
            _tripRepository = new TripRepository(_mockLogger.Object, _dbContext);

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

        [TestCleanup]
        public void TestCleanup()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [TestMethod]
        public async Task GetTripByIdValidIdReturnsResultWithTrip()
        {
            // Arrange - seed the in-memory database directly
            await _dbContext.Trips.AddAsync(_testTrip1);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _tripRepository.GetTripByTripIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testTrip1.TripId, result.TripId);
        }

        [TestMethod]
        public async Task GetTripByIdInvalidIdReturnsTripNotFoundException()
        {
            var exception = await Assert.ThrowsAsync<TripNotFoundException>(async () =>
            {
                await _tripRepository.GetTripByTripIdAsync(999);
            });

            Assert.IsNotNull(exception);
        }
    }
}
