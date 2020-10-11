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
        private readonly Mock<IMapper> mapper;
        private readonly Mock<UserManager<User>> userManager;

        public RegisterCommandHandlerTest()
        {
            mapper = new Mock<IMapper>();
            userManager = CustomMock.GetMockUserManager();
        }

        [Fact]
        public async Task RegisterCommandHandler_ThrowsUserAlreadyExistException()
        {
            //Arrange
            var user = new User();
            var command = new RegisterCommand { UserName = "user", Password = "User123" };

            mapper.Setup(x => x.Map<User>(command)).Returns(user);

            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            var commandHandler = new RegisterCommandHandler(mapper.Object, userManager.Object);

            //Assert
            await Assert.ThrowsAsync<UserAlreadyExistException>(() => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task RegisterCommandHandler_ThrowsUserNotAddedException()
        {
            //Arrange
            var user = new User();
            var command = new RegisterCommand { UserName = "user", Password = "User123" };

            mapper.Setup(x => x.Map<User>(command)).Returns(user);

            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            userManager.Setup(x => x.CreateAsync(user, It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed()));

            var commandHandler = new RegisterCommandHandler(mapper.Object, userManager.Object);

            //Assert
            await Assert.ThrowsAsync<UserNotAddedException>(() => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task RegisterCommandHandler_Success()
        {
            //Arrange
            var user = new User();
            var command = new RegisterCommand { UserName = "user", Password = "User123" };

            mapper.Setup(x => x.Map<User>(command)).Returns(user);

            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult((User)null));

            userManager.Setup(x => x.CreateAsync(user, It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));

            var commandHandler = new RegisterCommandHandler(mapper.Object, userManager.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
        }
    }
}
