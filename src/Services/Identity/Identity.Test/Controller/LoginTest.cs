using BikeExtensions;
using Castle.Core.Logging;
using Identity.API.Controllers;
using Identity.Application.Commands;
using Identity.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Identity.Test.Controller
{
    public class LoginTest
    {
        private const string token = "token";
        private const string nameIdentifier = "name";

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<IdentityController>> logger;

        private readonly TryLoginCommand command;
        private readonly IdentityController controller;
        private readonly TokenModel tokenModel;

        public LoginTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<IdentityController>>();
            command = new TryLoginCommand();
            controller = new IdentityController(mediator.Object, logger.Object);
            tokenModel = new TokenModel(token, nameIdentifier);
        }

        [Fact]
        public async Task Login_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Returns(Task.FromResult(tokenModel));        

            //Act
            var action = await controller.Login(command) as OkObjectResult;
            var value = action.Value as TokenModel;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(token, value.Token);
        }

        [Fact]
        public async Task Login_ThrowsException_BadRequestbjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.Login(command) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
