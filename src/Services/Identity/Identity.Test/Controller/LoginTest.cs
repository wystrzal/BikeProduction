using Identity.API.Controllers;
using Identity.Application.Commands;
using Identity.Core.Models;
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
    public class LoginTest
    {
        private readonly Mock<IMediator> mediator;

        public LoginTest()
        {
            mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task Login_OkObjectResult()
        {
            //Arrange
            var token = "token";
            var nameIdentifier = "name";
            var tokenModel = new TokenModel(token, nameIdentifier);

            var command = new TryLoginCommand();

            mediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Returns(Task.FromResult(tokenModel));

            var controller = new IdentityController(mediator.Object);

            //Act
            var action = await controller.Login(command) as OkObjectResult;
            var value = action.Value as TokenModel;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(token, value.Token);
        }

        [Fact]
        public async Task Login_ThrowException_BadRequestbjectResult()
        {
            //Arrange
            var command = new TryLoginCommand();

            mediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new IdentityController(mediator.Object);

            //Act
            var action = await controller.Login(command) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }
    }
}
