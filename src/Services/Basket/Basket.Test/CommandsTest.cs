using Basket.Application.Commands;
using Basket.Application.Commands.Handlers;
using Basket.Core.Dtos;
using Basket.Core.Interfaces;
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
        public async Task AddProductCommand_Success()
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
            basketRedisService.Verify(x => x.GetBasket(userId), Times.Once);

            basketRedisService.Verify(x => x.RemoveBasket(userId), Times.Once);

            basketRedisService.Verify(x => x.SaveBasket(userId, It.IsAny<string>()), Times.Once);
        }

    }
}
