using Basket.Application.Commands;
using Basket.Application.Commands.Handlers;
using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using MediatR;
using Moq;
using System.Collections.Generic;
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
        private readonly UserBasketDto basketDto;

        public RemoveProductCommandHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
            command = new RemoveProductCommand(userId, productId);
            commandHandler = new RemoveProductCommandHandler(basketRedisService.Object);

            basketDto = new UserBasketDto
            {
                Products = new List<BasketProduct>()
                {
                    new BasketProduct {Id = productId}
                }
            };
        }

        [Fact]
        public async Task RemoveProductCommandHandler_Success()
        {
            //Arrange
            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult(basketDto));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            basketRedisService.Verify(x => x.SaveBasket(userId, It.IsAny<UserBasketDto>()), Times.Once);
        }
    }
}
