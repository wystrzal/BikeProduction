using Identity.Core.Models;
using Identity.Infrastructure.Services;
using Identity.Test.MockHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Identity.Test.Services
{
    public class TokenServiceTest
    {
        private const string section = "AppSettings:Token";
        private const string key = "VeryLongKeyForTest";

        private readonly Mock<IConfigurationSection> configurationSection;
        private readonly Mock<IConfiguration> configuration;
        private readonly Mock<UserManager<User>> userManager;

        private readonly TokenService service;
        private readonly User user;

        public TokenServiceTest()
        {
            configurationSection = new Mock<IConfigurationSection>();
            configuration = new Mock<IConfiguration>();
            userManager = CustomMock.GetMockUserManager();
            service = new TokenService(configuration.Object);
            user = new User();
        }

        [Fact]
        public void GenerateToken_Success()
        {
            //Arrange
            configurationSection.Setup(a => a.Value).Returns(key);
            configuration.Setup(a => a.GetSection(section)).Returns(configurationSection.Object);

            //Act
            var action = service.GenerateToken(user, userManager.Object);

            //Arrange
            Assert.NotNull(action);
        }
    }
}
