using Basket.API.Controllers;
using Basket.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Test.Controller
{
    public class ChangeProductQuantityTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<BasketController>> logger;

        public ChangeProductQuantityTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<BasketController>>();
        }

        [Fact]
        public async Task ChangeProductQuantity_OkResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<ChangeProductQuantityCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new BasketController(mediator.Object, logger.Object);

            //Act
            var action = await controller.ChangeProductQuantity(It.IsAny<ChangeProductQuantityCommand>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<ChangeProductQuantityCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task ChangeProductQuantity_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<ChangeProductQuantityCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var controller = new BasketController(mediator.Object, logger.Object);

            //Act
            var action = await controller.ChangeProductQuantity(It.IsAny<ChangeProductQuantityCommand>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }
    }
}
