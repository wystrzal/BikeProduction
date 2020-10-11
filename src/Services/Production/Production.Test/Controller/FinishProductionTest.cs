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
    public class FinishProductionTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<ProductionQueueController>> logger;

        public FinishProductionTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<ProductionQueueController>>();
        }

        [Fact]
        public async Task FinishProduction_OkResult()
        {
            //Arrange
            var controller = new ProductionQueueController(mediator.Object, logger.Object);

            //Act
            var action = await controller.FinishProduction(It.IsAny<int>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<FinishProductionCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task FinishProduction_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<FinishProductionCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new ProductionQueueController(mediator.Object, logger.Object);

            //Act
            var action = await controller.FinishProduction(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }
    }
}
