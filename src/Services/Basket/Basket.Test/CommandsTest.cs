using Basket.Application.Commands;
using Basket.Application.Commands.Handlers;
using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Test
{
    public class CommandsTest
    {
        private readonly Mock<IBasketRedisService> basketRedisService;

        public CommandsTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
        }

        [Fact]
        public async Task AddProductCommandHandler_Success()
        {
            //Arrange
            var userId = "1";
            var command = new AddProductCommand { UserId = userId, Product = new Core.Models.BasketProduct() };

            basketRedisService.Setup(x => x.GetBasket(userId)).Verifiable();

            basketRedisService.Setup(x => x.RemoveBasket(userId)).Verifiable();

            basketRedisService.Setup(x => x.SaveBasket(userId, It.IsAny<string>())).Verifiable();

            var commandHandler = new AddProductCommandHandler(basketRedisService.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);

            basketRedisService.Verify(x => x.GetBasket(userId), Times.Once);

            basketRedisService.Verify(x => x.RemoveBasket(userId), Times.Once);

            basketRedisService.Verify(x => x.SaveBasket(userId, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task RemoveProductCommandHandler_NullBasket_Success()
        {
            //Arrange
            var userId = "1";
            var command = new RemoveProductCommand(userId, 1);

            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult((UserBasketDto)null));

            var commandHandler = new RemoveProductCommandHandler(basketRedisService.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
        }

        [Fact]
        public async Task RemoveProductCommandHandler_Success()
        {
            //Arrange
            var userId = "1";
            var command = new RemoveProductCommand(userId, 1);
            var userBasketDto = new UserBasketDto
            {
                UserId = userId,
                Products = new List<Core.Models.BasketProduct>()
                {
                    new Core.Models.BasketProduct {Id = 1} 
                }
            };

            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult(userBasketDto));

            basketRedisService.Setup(x => x.RemoveBasket(userId)).Verifiable();

            basketRedisService.Setup(x => x.SaveBasket(userId, It.IsAny<string>())).Verifiable();


            var commandHandler = new RemoveProductCommandHandler(basketRedisService.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);

            basketRedisService.Verify(x => x.RemoveBasket(userId), Times.Once);

            basketRedisService.Verify(x => x.SaveBasket(userId, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ClearBasketCommandHandler_Success()
        {
            //Arrange
            var userId = "1";
            var command = new ClearBasketCommand(userId);

            basketRedisService.Setup(x => x.RemoveBasket(userId)).Verifiable();

            var commandHandler = new ClearBasketCommandHandler(basketRedisService.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);

            basketRedisService.Verify(x => x.RemoveBasket(userId), Times.Once);
        }

        [Fact]
        public async Task ChangeProductQuantityCommandHandler_NullBasket_Success()
        {
            //Arrange
            var userId = "1";
            var command = new ChangeProductQuantityCommand
            {
                UserId = userId,
                ProductId = 1,
                ChangeQuantityAction = Core.Dtos.Enums.ChangeProductQuantityEnum.ChangeQuantityAction.Minus
            };

            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult((UserBasketDto)null));

            var commandHandler = new ChangeProductQuantityCommandHandler(basketRedisService.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
        }

        [Fact]
        public async Task ChangeProductQuantityCommandHandler_NullBasketProduct_Success()
        {
            //Arrange
            var userId = "1";
            var command = new ChangeProductQuantityCommand
            {
                UserId = userId,
                ProductId = 1,
                ChangeQuantityAction = Core.Dtos.Enums.ChangeProductQuantityEnum.ChangeQuantityAction.Minus
            };

            var userBasketDto = new UserBasketDto
            {
                UserId = userId
            };

            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult(userBasketDto));

            var commandHandler = new ChangeProductQuantityCommandHandler(basketRedisService.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
        }

        [Fact]
        public async Task ChangeProductQuantityCommandHandler_Success()
        {
            //Arrange
            var userId = "1";
            var command = new ChangeProductQuantityCommand
            {
                UserId = userId,
                ProductId = 1,
                ChangeQuantityAction = Core.Dtos.Enums.ChangeProductQuantityEnum.ChangeQuantityAction.Minus
            };

            var userBasketDto = new UserBasketDto
            {
                UserId = userId,
                Products = new List<Core.Models.BasketProduct> { new Core.Models.BasketProduct { Id = 1 } }
            };

            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult(userBasketDto));

            basketRedisService.Setup(x => x.RemoveBasket(userId)).Verifiable();

            basketRedisService.Setup(x => x.SaveBasket(userId, It.IsAny<string>())).Verifiable();

            var commandHandler = new ChangeProductQuantityCommandHandler(basketRedisService.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);

            basketRedisService.Verify(x => x.RemoveBasket(userId), Times.Once);

            basketRedisService.Verify(x => x.SaveBasket(userId, It.IsAny<string>()), Times.Once);
        }
    }
}
