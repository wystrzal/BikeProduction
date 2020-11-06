using Basket.API.Controllers;
using Basket.Application.Commands;
using BikeExtensions;
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
    public class DeleteProductTest
    {
        private const string userId = "1";
        private const int productId = 1;

        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<BasketController>> logger;
        
        private readonly BasketController controller;      

        public DeleteProductTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<BasketController>>();
            controller = new BasketController(mediator.Object, logger.Object);
        }

        [Fact]
        public async Task DeleteProduct_OkResult()
        {
            //Act
            var action = await controller.DeleteProduct(userId, productId) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<RemoveProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<RemoveProductCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            //Act
            var action = await controller.DeleteProduct(userId, productId) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public void DeleteProduct_ProductIdEqualZero_ArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new RemoveProductCommand(userId, It.IsAny<int>()));
        }
    }
}
