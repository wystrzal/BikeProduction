using BikeExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Production.API.Controllers;
using Production.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Production.Test.Controller
{
    public class ConfirmProductionTest
    {
        private const int id = 1;

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<ProductionController>> logger;

        private readonly ProductionController controller;

        public ConfirmProductionTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<ProductionController>>();
            controller = new ProductionController(mediator.Object, logger.Object);
        }

        [Fact]
        public async Task ConfirmProduction_OkResult()
        {
            //Act
            var action = await controller.ConfirmProduction(id) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<ConfirmProductionCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task ConfirmProduction_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<ConfirmProductionCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.ConfirmProduction(id) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
