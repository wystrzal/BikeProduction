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
    public class AddProductCommandHandlerTest
    {
        private readonly Mock<IBasketRedisService> basketRedisService;

        public AddProductCommandHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
        }

        [Fact]
        public async Task AddProductCommandHandler_Success()
        {
            //Arrange
            var command = new AddProductCommand { UserId = It.IsAny<string>(), Product = new BasketProduct() };

            var commandHandler = new AddProductCommandHandler(basketRedisService.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            basketRedisService.Verify(x => x.GetBasket(It.IsAny<string>()), Times.Once);
            basketRedisService.Verify(x => x.SaveBasket(It.IsAny<string>(), It.IsAny<UserBasketDto>()), Times.Once);
        }
    }
}
