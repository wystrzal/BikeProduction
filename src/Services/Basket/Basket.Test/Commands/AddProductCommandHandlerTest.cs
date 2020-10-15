using Basket.Application.Commands;
using Basket.Application.Commands.Handlers;
using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Test.Commands
{
    public class AddProductCommandHandlerTest
    {
        private readonly Mock<IBasketRedisService> basketRedisService;

        private readonly AddProductCommand command;
        private readonly AddProductCommandHandler commandHandler;

        public AddProductCommandHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
            command = new AddProductCommand { UserId = It.IsAny<string>(), Product = new BasketProduct() };
            commandHandler = new AddProductCommandHandler(basketRedisService.Object);
        }

        [Fact]
        public async Task AddProductCommandHandler_Success()
        {
            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            basketRedisService.Verify(x => x.GetBasket(It.IsAny<string>()), Times.Once);
            basketRedisService.Verify(x => x.SaveBasket(It.IsAny<string>(), It.IsAny<UserBasketDto>()), Times.Once);
        }
    }
}
