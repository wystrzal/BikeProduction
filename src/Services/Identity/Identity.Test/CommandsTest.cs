using Identity.Application.Commands;
using Identity.Application.Commands.Handlers;
using Identity.Core.Exceptions;
using Identity.Core.Interfaces;
using Identity.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Identity.Test
{
    public class CommandsTest
    {
        private Mock<UserManager<User>> GetMockUserManager()
        {
            var store = new Mock<IUserStore<User>>();

            return new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        }

        private Mock<SignInManager<User>> GetMockSignInManager()
        {
            var _mockUserManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object,
                null, null, null, null, null, null, null, null);
            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            return new Mock<SignInManager<User>>(_mockUserManager.Object,
                           _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null, null);
        }

        private readonly Mock<ITokenService> tokenService;

        public CommandsTest()
        {
            tokenService = new Mock<ITokenService>();
        }

        [Fact]
        public async Task TryLoginCommandHandler_ThrowsUserNotFoundException()
        {
            //Arrange
            var userManager = GetMockUserManager();
            var signInManager = GetMockSignInManager();
            var command = new TryLoginCommand { Username = "user", Password = "User123" };

            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            var commandHandler = new TryLoginCommandHandler(userManager.Object, signInManager.Object, tokenService.Object);

            //Assert
            await Assert.ThrowsAsync<UserNotFoundException>(
                () => commandHandler.Handle(command, It.IsAny<CancellationToken>()));          
        }

        [Fact]
        public async Task TryLoginCommandHandler_ThrowsLoginFailedException()
        {
            //Arrange
            var userManager = GetMockUserManager();
            var signInManager = GetMockSignInManager();
            var command = new TryLoginCommand { Username = "user", Password = "User123" };
            var user = new User();

            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            signInManager.Setup(x => x.CheckPasswordSignInAsync(user, It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInResult.Failed));

            var commandHandler = new TryLoginCommandHandler(userManager.Object, signInManager.Object, tokenService.Object);

            //Assert
            await Assert.ThrowsAsync<LoginFailedException>(
                () => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task TryLoginCommandHandler_Success()
        {
            //Arrange
            var userManager = GetMockUserManager();
            var signInManager = GetMockSignInManager();
            var command = new TryLoginCommand { Username = "user", Password = "User123" };
            var user = new User();

            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            signInManager.Setup(x => x.CheckPasswordSignInAsync(user, It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInResult.Success));

            tokenService.Setup(x => x.GenerateToken(user, userManager.Object))
                .Returns(Task.FromResult(new TokenModel("test", "test")));

            var commandHandler = new TryLoginCommandHandler(userManager.Object, signInManager.Object, tokenService.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(action);
        }
    }
}
