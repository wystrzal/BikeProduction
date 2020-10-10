using Identity.API.Controllers;
using Identity.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        public RegisterUserTest()
        {
            mediator = new Mock<IMediator>();
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

            var controller = new IdentityController(mediator.Object);

            //Act
            var action = await controller.RegisterUser(command) as OkObjectResult;

            //Assert
            mediator.Verify(x => x.Send(command, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }
    }
}
