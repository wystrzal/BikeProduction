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
    public class RemoveProductCommandHandlerTest
    {
        private const string userId = "1";
        private const int productId = 1;

        private readonly Mock<IBasketRedisService> basketRedisService;

        private readonly RemoveProductCommand command;
        private readonly RemoveProductCommandHandler commandHandler;

        public RemoveProductCommandHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
            command = new RemoveProductCommand(userId, productId);
            commandHandler = new RemoveProductCommandHandler(basketRedisService.Object);
        }

        [Fact]
        public async Task RemoveProductCommandHandler_NullBasket_Success()
        {
            //Arrange
            basketRedisService.Setup(x => x.GetBasket(It.IsAny<string>())).Returns(Task.FromResult((UserBasketDto)null));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
        }

        [Fact]
        public async Task RemoveProductCommandHandler_Success()
        {
            //Arrange
            var userBasketDto = new UserBasketDto
            {
                UserId = userId,
                Products = new List<BasketProduct>()
                {
                    new BasketProduct {Id = productId}
                }
            };

            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult(userBasketDto));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            basketRedisService.Verify(x => x.SaveBasket(userId, It.IsAny<UserBasketDto>()), Times.Once);
        }
    }
}
