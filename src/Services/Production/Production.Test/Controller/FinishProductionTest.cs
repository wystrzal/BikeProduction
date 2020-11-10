using BikeExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    public class FinishProductionTest
    {
        private const int id = 1;
        private const string token = "1";

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<ProductionController>> logger;

        private readonly ProductionController controller;

        public FinishProductionTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<ProductionController>>();
            controller = new ProductionController(mediator.Object, logger.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "VeryLongToken";
        }

        [Fact]
        public async Task FinishProduction_OkResult()
        {
            //Act
            var action = await controller.FinishProduction(id) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<FinishProductionCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task FinishProduction_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<FinishProductionCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.FinishProduction(id) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public void FinishProduction_NullToken_ThrowsArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new FinishProductionCommand(id, It.IsAny<string>()));
        }
    }
}
