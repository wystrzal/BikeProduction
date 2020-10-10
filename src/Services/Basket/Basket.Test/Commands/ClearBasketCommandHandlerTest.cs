using Basket.Application.Commands;
using Basket.Application.Commands.Handlers;
using Basket.Core.Interfaces;
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
    public class ClearBasketCommandHandlerTest
    {
        private readonly Mock<IBasketRedisService> basketRedisService;

        public ClearBasketCommandHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
        }

        [Fact]
        public async Task ClearBasketCommandHandler_Success()
        {
            //Arrange
            var userId = "1";
            var command = new ClearBasketCommand(userId);

            var commandHandler = new ClearBasketCommandHandler(basketRedisService.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);

            basketRedisService.Verify(x => x.RemoveBasket(userId), Times.Once);
        }
    }
}
