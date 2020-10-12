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
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<IdentityController>> logger;

        public RegisterUserTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<IdentityController>>();
        }

        [Fact]
        public async Task RegisterUser_ModelIsNotValid_BadRequestObjectResult()
        {
            //Arrange
            var command = new RegisterCommand();

            var controller = new IdentityController(mediator.Object, logger.Object);

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
            var command = new RegisterCommand();

            mediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new IdentityController(mediator.Object, logger.Object);

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
            var password = "User123";
            var userName = "user123";
            var command = new RegisterCommand { Password = password, UserName = userName };

            var controller = new IdentityController(mediator.Object, logger.Object);

            //Act
            var action = await controller.RegisterUser(command) as OkObjectResult;

            //Assert
            mediator.Verify(x => x.Send(command, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }
    }
}
