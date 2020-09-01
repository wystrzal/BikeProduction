using Identity.API.Controllers;
using Identity.Application.Commands;
using Identity.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Identity.Test
{
    public class ControllerTest
    {
        private readonly Mock<IMediator> mediator;

        public ControllerTest()
        {
            mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task Login_OkObjectResult()
        {
            //Arrange
            var tokenModel = new TokenModel("test", "test");
            var command = new TryLoginCommand { Username = "test", Password = "test" };

            mediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Returns(Task.FromResult(tokenModel));

            var controller = new IdentityController(mediator.Object);

            //Act
            var action = await controller.Login(command) as OkObjectResult;
            var value = action.Value as TokenModel;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal("test", value.Token);
        }

        [Fact]
        public async Task Login_ThrowException_BadRequestbjectResult()
        {
            //Arrange
            var command = new TryLoginCommand { Username = "test", Password = "test" };

            mediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new IdentityController(mediator.Object);

            //Act
            var action = await controller.Login(command) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task RegisterUser_ModelIsNotValid_BadRequestObjectResult()
        {
            //Arrange
            var command = new RegisterCommand();

            var controller = new IdentityController(mediator.Object);

            //Act
            var action = await controller.RegisterUser(command) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task RegisterUser_ThrowException_BadRequestObjectResult()
        {
            //Arrange
            var command = new RegisterCommand { Password = "User123", UserName = "user123" };

            mediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new IdentityController(mediator.Object);

            //Act
            var action = await controller.RegisterUser(command) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task RegisterUser_OkObjectResult()
        {
            //Arrange
            var command = new RegisterCommand { Password = "User123", UserName = "user123" };

            mediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Verifiable();

            var controller = new IdentityController(mediator.Object);

            //Act
            var action = await controller.RegisterUser(command) as OkObjectResult;

            //Assert
            mediator.Verify(x => x.Send(command, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }
    }
}
