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
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<ProductionQueueController>> logger;

        public ConfirmProductionTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<ProductionQueueController>>();
        }

        [Fact]
        public async Task ConfirmProduction_OkResult()
        {
            //Arrange
            var controller = new ProductionQueueController(mediator.Object, logger.Object);

            //Act
            var action = await controller.ConfirmProduction(It.IsAny<int>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<ConfirmProductionCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task ConfirmProduction_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<ConfirmProductionCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new ProductionQueueController(mediator.Object, logger.Object);

            //Act
            var action = await controller.ConfirmProduction(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }
    }
}
