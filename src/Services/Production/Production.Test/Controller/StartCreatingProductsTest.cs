using BikeExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Production.API.Controllers;
using Production.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Production.Test.Controller
{
    public class StartCreatingProductsTest
    {
        private const int id = 1;

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<ProductionController>> logger;

        private readonly ProductionController controller;

        public StartCreatingProductsTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<ProductionController>>();
            controller = new ProductionController(mediator.Object, logger.Object);
        }

        [Fact]
        public async Task StartCreatingProducts_OkResult()
        {
            //Act
            var action = await controller.StartCreatingProducts(id) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<StartCreatingProductsCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task StartCreatingProducts_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<StartCreatingProductsCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.StartCreatingProducts(id) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
