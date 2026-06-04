using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.API.Repositories;
using App.API.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace App.API.Tests.Services
{
    [TestClass]
    public class AppUserServiceTests
    {
        private Mock<ILogger> _mockLogger;
        private Mock<IAppUserRepository> _mockAppUserRepository;
        private IAppUserService _userService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILogger>();
            _mockAppUserRepository = new Mock<IAppUserRepository>();
            _userService = new AppUserService(_mockLogger.Object, _mockAppUserRepository.Object);
        }

        [TestMethod]
        public void GetAllAppUsersAsync()
        {

        }
    }
}
