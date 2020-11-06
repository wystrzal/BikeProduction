using Basket.API.Controllers;
using Basket.Application.Queries;
using Basket.Core.Dtos;
using Basket.Core.Models;
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
        private const string userId = "1";
       
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<BasketController>> logger;
       
        private readonly BasketController controller;
        private readonly UserBasketDto basketDto;

        public GetBasketTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<BasketController>>();
            controller = new BasketController(mediator.Object, logger.Object);
            basketDto = new UserBasketDto { Products = new List<BasketProduct> { new BasketProduct() } };
        }

        [Fact]
        public async Task GetBasket_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetBasketQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(basketDto));

            //Act
            var action = await controller.GetBasket(userId) as OkObjectResult;
            var value = action.Value as UserBasketDto;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(basketDto.Products.Count, value.Products.Count);
        }

        [Fact]
        public async Task GetBasket_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetBasketQuery>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            //Act
            var action = await controller.GetBasket(It.IsAny<string>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
