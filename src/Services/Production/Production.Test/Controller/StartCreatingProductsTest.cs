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
    public class StartCreatingProductsTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<ProductionQueueController>> logger;

        public StartCreatingProductsTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<ProductionQueueController>>();
        }

        [Fact]
        public async Task StartCreatingProducts_OkResult()
        {
            //Arrange
            var id = 1;

            mediator.Setup(x => x.Send(It.IsAny<StartCreatingProductsCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new ProductionQueueController(mediator.Object, logger.Object);

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
            var id = 1;

            mediator.Setup(x => x.Send(It.IsAny<StartCreatingProductsCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new ProductionQueueController(mediator.Object, logger.Object);

            //Act
            var action = await controller.StartCreatingProducts(id) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }
    }
}
