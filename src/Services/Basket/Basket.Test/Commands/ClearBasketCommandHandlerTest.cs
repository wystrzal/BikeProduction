﻿using Basket.Application.Commands;
using Basket.Application.Commands.Handlers;
using Basket.Core.Interfaces;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Test.Commands
{
    public class ClearBasketCommandHandlerTest
    {
        private const string userId = "1";

        private readonly Mock<IBasketRedisService> basketRedisService;

        private readonly ClearBasketCommand command;
        private readonly ClearBasketCommandHandler commandHandler;

        public ClearBasketCommandHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
            command = new ClearBasketCommand(userId);
            commandHandler = new ClearBasketCommandHandler(basketRedisService.Object);
        }

        [Fact]
        public async Task ClearBasketCommandHandler_Success()
        {
            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            basketRedisService.Verify(x => x.RemoveBasket(It.IsAny<string>()), Times.Once);
        }
    }
}
