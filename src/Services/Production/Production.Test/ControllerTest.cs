using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Production.API.Controllers;
using Production.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Production.Test
{
    public class ControllerTest
    {
        private readonly Mock<IMediator> mediator;

        public ControllerTest()
        {
            mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task ConfirmProduction_OkResult()
        {
            //Arrange
            var id = 1;

            mediator.Setup(x => x.Send(It.IsAny<ConfirmProductionCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new ProductionQueueController(mediator.Object);

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
            var id = 1;

            mediator.Setup(x => x.Send(It.IsAny<ConfirmProductionCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new ProductionQueueController(mediator.Object);

            //Act
            var action = await controller.ConfirmProduction(id) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task StartCreatingProducts_OkResult()
        {
            //Arrange
            var id = 1;

            mediator.Setup(x => x.Send(It.IsAny<StartCreatingProductsCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new ProductionQueueController(mediator.Object);

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

            var controller = new ProductionQueueController(mediator.Object);

            //Act
            var action = await controller.StartCreatingProducts(id) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }
    }
}
