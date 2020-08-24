using Basket.API.Controllers;
using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Core.Dtos;
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

namespace Basket.Test
{
    public class ControllerTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<BasketController>> logger;

        public ControllerTest()
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

        [Fact]
        public async Task AddProduct_OkResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<AddProductCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new BasketController(mediator.Object, logger.Object);

            //Act
            var action = await controller.AddProduct(It.IsAny<AddProductCommand>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<AddProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }


        [Fact]
        public async Task AddProduct_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<AddProductCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var controller = new BasketController(mediator.Object, logger.Object);

            //Act
            var action = await controller.AddProduct(It.IsAny<AddProductCommand>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
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

        [Fact]
        public async Task ClearBasket_OkResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<ClearBasketCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new BasketController(mediator.Object, logger.Object);

            //Act
            var action = await controller.ClearBasket(It.IsAny<string>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<ClearBasketCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task ClearBasket_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<ClearBasketCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var controller = new BasketController(mediator.Object, logger.Object);

            //Act
            var action = await controller.ClearBasket(It.IsAny<string>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task DeleteProduct_OkResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<RemoveProductCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new BasketController(mediator.Object, logger.Object);

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

            var controller = new BasketController(mediator.Object, logger.Object);

            //Act
            var action = await controller.DeleteProduct(It.IsAny<string>(), It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }
    }
}
