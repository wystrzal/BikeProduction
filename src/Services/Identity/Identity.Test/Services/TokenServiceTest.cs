using Identity.Core.Models;
using Identity.Infrastructure.Services;
using Identity.Test.MockHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Identity.Test.Services
{
    public class TokenServiceTest
    {
        private readonly Mock<IConfigurationSection> configurationSection;
        private readonly Mock<IConfiguration> configuration;
        private readonly Mock<UserManager<User>> userManager;

        public TokenServiceTest()
        {
            configurationSection = new Mock<IConfigurationSection>();
            configuration = new Mock<IConfiguration>();
            userManager = CustomMock.GetMockUserManager();
        }

        [Fact]
        public void GenerateToken_Success()
        {
            //Arrange
            var user = new User();
            var section = "AppSettings:Token";
            var key = "VeryLongKeyForTest";

            configurationSection.Setup(a => a.Value).Returns(key);
            configuration.Setup(a => a.GetSection(section)).Returns(configurationSection.Object);

            var service = new TokenService(configuration.Object);

            //Act
            var action = service.GenerateToken(user, userManager.Object);

            //Arrange
            Assert.NotNull(action);
        }
    }
}
