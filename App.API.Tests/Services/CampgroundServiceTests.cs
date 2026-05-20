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

namespace App.API.Tests.Services
{
    [TestClass]
    public class CampgroundServiceTests
    {
        private Mock<ICampgroundRepository> _campRepo;
        private Campground _testCampground1;
        private Campground _testCampground2;
        private List<Campground> _testCampgrounds;

        [TestInitialize]
        public void TestInitialize()
        {
            _campRepo = new Mock<ICampgroundRepository>();

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

            _testCampgrounds.AddRange(_testCampground1, _testCampground2);
        }

        [TestMethod]
        public async Task DeleteCampgroundAsyncValidIdDeletesCampground()
        {
        }
    }
}
