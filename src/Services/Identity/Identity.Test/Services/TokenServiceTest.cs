using Identity.Core.Models;
using Identity.Infrastructure.Services;
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
        [Fact]
        public void GenerateToken_Success()
        {
            //Arrange
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var configurationSection = new Mock<IConfigurationSection>();
            var configMock = new Mock<IConfiguration>();
            var user = new User { UserName = "test" };

            configurationSection.Setup(a => a.Value).Returns("VeryLongKeyForTest");
            configMock.Setup(a => a.GetSection("AppSettings:Token")).Returns(configurationSection.Object);

            var service = new TokenService(configMock.Object);

            //Act
            var action = service.GenerateToken(user, userManager.Object);

            //Arrange
            Assert.NotNull(action);
        }
    }
}
