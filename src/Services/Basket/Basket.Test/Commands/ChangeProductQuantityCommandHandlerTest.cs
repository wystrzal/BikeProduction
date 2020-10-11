using Basket.Application.Commands;
using Basket.Application.Commands.Handlers;
using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Test.Commands
{
    public class ChangeProductQuantityCommandHandlerTest
    {
        private readonly Mock<IBasketRedisService> basketRedisService;

        public ChangeProductQuantityCommandHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
        }

        [Fact]
        public async Task ChangeProductQuantityCommandHandler_NullBasket()
        {
            //Arrange
            var command = new ChangeProductQuantityCommand();

            basketRedisService.Setup(x => x.GetBasket(It.IsAny<string>())).Returns(Task.FromResult((UserBasketDto)null));

            var commandHandler = new ChangeProductQuantityCommandHandler(basketRedisService.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
        }

        [Fact]
        public async Task ChangeProductQuantityCommandHandler_NullBasketProduct()
        {
            //Arrange
            var command = new ChangeProductQuantityCommand();

            basketRedisService.Setup(x => x.GetBasket(It.IsAny<string>())).Returns(Task.FromResult(It.IsAny<UserBasketDto>()));

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
                Products = new List<BasketProduct> { new BasketProduct { Id = 1 } }
            };

            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult(userBasketDto));

            var commandHandler = new ChangeProductQuantityCommandHandler(basketRedisService.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);

            basketRedisService.Verify(x => x.SaveBasket(userId, It.IsAny<UserBasketDto>()), Times.Once);
        }
    }
}
