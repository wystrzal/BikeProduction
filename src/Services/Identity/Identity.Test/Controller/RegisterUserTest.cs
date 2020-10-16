using BikeExtensions;
using Castle.Core.Logging;
using Identity.API.Controllers;
using Identity.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Identity.Test.Controller
{
    public class RegisterUserTest
    {
        private const string userName = "user123";
        private const string password = "User123";

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<IdentityController>> logger;

        private readonly RegisterCommand command;
        private readonly IdentityController controller;

        public RegisterUserTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<IdentityController>>();
            command = new RegisterCommand();
            controller = new IdentityController(mediator.Object, logger.Object);
        }

        [Fact]
        public async Task RegisterUser_ModelIsNotValid_BadRequestObjectResult()
        {
            //Act
            var action = await controller.RegisterUser(command) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task RegisterUser_ThrowsException_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.RegisterUser(command) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task RegisterUser_OkObjectResult()
        {
            //Arrange
            var command = new RegisterCommand { Password = password, UserName = userName };

            //Act
            var action = await controller.RegisterUser(command) as OkObjectResult;

            //Assert
            mediator.Verify(x => x.Send(command, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }
    }
}
