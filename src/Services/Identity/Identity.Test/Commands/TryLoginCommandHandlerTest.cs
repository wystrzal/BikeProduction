using Identity.Application.Commands;
using Identity.Application.Commands.Handlers;
using Identity.Core.Exceptions;
using Identity.Core.Interfaces;
using Identity.Core.Models;
using Identity.Test.MockHelpers;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Identity.Test.Commands
{
    public class TryLoginCommandHandlerTest
    {
        private readonly Mock<ITokenService> tokenService;
        private readonly Mock<UserManager<User>> userManager;
        private readonly Mock<SignInManager<User>> signInManager;
        private readonly Mock<IBus> bus;

        private readonly TryLoginCommand command;
        private readonly TryLoginCommandHandler commandHandler;
        private readonly User user;
        private readonly TokenModel tokenModel;

        public TryLoginCommandHandlerTest()
        {
            userManager = CustomMock.GetMockUserManager();
            signInManager = CustomMock.GetMockSignInManager();
            tokenService = new Mock<ITokenService>();
            bus = new Mock<IBus>();
            command = new TryLoginCommand();
            commandHandler = new TryLoginCommandHandler(userManager.Object, signInManager.Object, tokenService.Object, bus.Object);
            user = new User();
            tokenModel = new TokenModel(It.IsAny<string>(), It.IsAny<string>());
        }

        [Fact]
        public async Task TryLoginCommandHandler_ThrowsUserNotFoundException()
        {
            //Arrange
            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            //Assert
            await Assert.ThrowsAsync<UserNotFoundException>(
                () => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task TryLoginCommandHandler_ThrowsLoginFailedException()
        {
            //Arrange
            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            signInManager.Setup(x => x.CheckPasswordSignInAsync(user, It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInResult.Failed));

            //Assert
            await Assert.ThrowsAsync<LoginFailedException>(
                () => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task TryLoginCommandHandler_Success()
        {
            //Arrange
            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            signInManager.Setup(x => x.CheckPasswordSignInAsync(user, It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInResult.Success));

            tokenService.Setup(x => x.GenerateToken(user, userManager.Object)).Returns(Task.FromResult(tokenModel));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(action);
        }
    }
}
