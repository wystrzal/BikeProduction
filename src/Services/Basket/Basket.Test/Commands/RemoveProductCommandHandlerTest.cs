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

namespace Basket.Test.Commands
{
    public class RemoveProductCommandHandlerTest
    {
        private readonly Mock<IBasketRedisService> basketRedisService;

        public RemoveProductCommandHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
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

            basketRedisService.Setup(x => x.SaveBasket(userId, It.IsAny<string>())).Verifiable();


            var commandHandler = new RemoveProductCommandHandler(basketRedisService.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);

            basketRedisService.Verify(x => x.SaveBasket(userId, It.IsAny<string>()), Times.Once);
        }
    }
}
