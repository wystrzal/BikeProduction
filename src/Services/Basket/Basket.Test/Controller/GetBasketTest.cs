using Basket.API.Controllers;
using Basket.Application.Queries;
using Basket.Core.Dtos;
using BikeExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Test.Controller
{
    public class GetBasketTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<BasketController>> logger;

        public GetBasketTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<BasketController>>();
        }

        [Fact]
        public async Task GetBasket_OkObjectResult()
        {
            //Arrange
            var userId = "1";
            var userBasketDto = new UserBasketDto { UserId = userId };

            mediator.Setup(x => x.Send(It.IsAny<GetBasketQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(userBasketDto));

            var controller = new BasketController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetBasket(It.IsAny<string>()) as OkObjectResult;
            var value = action.Value as UserBasketDto;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(userId, value.UserId);
        }

        [Fact]
        public async Task GetBasket_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetBasketQuery>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var controller = new BasketController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetBasket(It.IsAny<string>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
