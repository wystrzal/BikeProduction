using AutoMapper;
using Identity.Application.Commands;
using Identity.Application.Commands.Handlers;
using Identity.Core.Exceptions;
using Identity.Core.Models;
using Identity.Test.MockHelpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Identity.Test.Commands
{
    public class RegisterCommandHandlerTest
    {
        private const string userName = "user";
        private const string password = "User123";

        private readonly Mock<IMapper> mapper;
        private readonly Mock<UserManager<User>> userManager;

        private readonly RegisterCommand command;
        private readonly RegisterCommandHandler commandHandler;
        private readonly User user;

        public RegisterCommandHandlerTest()
        {
            mapper = new Mock<IMapper>();
            userManager = CustomMock.GetMockUserManager();
            command = new RegisterCommand { UserName = userName, Password = password };
            commandHandler = new RegisterCommandHandler(mapper.Object, userManager.Object);
            user = new User();
        }

        [Fact]
        public async Task RegisterCommandHandler_ThrowsUserAlreadyExistException()
        {
            //Arrange
            mapper.Setup(x => x.Map<User>(command)).Returns(user);

            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            //Assert
            await Assert.ThrowsAsync<UserAlreadyExistException>(() => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task RegisterCommandHandler_ThrowsUserNotAddedException()
        {
            //Arrange
            mapper.Setup(x => x.Map<User>(command)).Returns(user);

            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            userManager.Setup(x => x.CreateAsync(user, It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed()));

            //Assert
            await Assert.ThrowsAsync<UserNotAddedException>(() => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task RegisterCommandHandler_Success()
        {
            //Arrange
            mapper.Setup(x => x.Map<User>(command)).Returns(user);

            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            userManager.Setup(x => x.CreateAsync(user, It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
        }
    }
}
