using Basket.API.Controllers;
using Basket.Application.Queries;
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
    public class GetBasketQuantityTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<BasketController>> logger;

        public GetBasketQuantityTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<BasketController>>();
        }

        [Fact]
        public async Task GetBasketQuantity_OkObjectResult()
        {
            //Arrange
            int quantity = 1;

            mediator.Setup(x => x.Send(It.IsAny<GetBasketQuantityQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(quantity));

            var controller = new BasketController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetBasketQuantity(It.IsAny<string>()) as OkObjectResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(quantity, action.Value);
        }

        [Fact]
        public async Task GetBasketQuantity_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetBasketQuantityQuery>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var controller = new BasketController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetBasketQuantity(It.IsAny<string>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }
    }
}
