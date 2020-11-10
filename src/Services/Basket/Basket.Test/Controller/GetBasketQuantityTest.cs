using Basket.API.Controllers;
using Basket.Application.Queries;
using BikeExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Test.Controller
{
    public class GetBasketQuantityTest
    {
        private const int quantity = 1;
        private const string userId = "1";

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<BasketController>> logger;

        private readonly BasketController controller;

        public GetBasketQuantityTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<BasketController>>();
            controller = new BasketController(mediator.Object, logger.Object);
        }

        [Fact]
        public async Task GetBasketQuantity_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetBasketQuantityQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(quantity));

            //Act
            var action = await controller.GetBasketQuantity(userId) as OkObjectResult;

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

            //Act
            var action = await controller.GetBasketQuantity(It.IsAny<string>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
