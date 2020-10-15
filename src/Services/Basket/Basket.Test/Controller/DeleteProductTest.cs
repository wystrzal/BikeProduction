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
            var action = await controller.DeleteProduct(It.IsAny<string>(), It.IsAny<int>()) as OkResult;

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
            var action = await controller.DeleteProduct(It.IsAny<string>(), It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
