﻿using Basket.Application.Commands;
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
using static Basket.Core.Dtos.Enums.ChangeProductQuantityEnum;

namespace Basket.Test.Commands
{
    public class ChangeProductQuantityCommandHandlerTest
    {
        private const string userId = "1";
        private const int productId = 1;
        private const ChangeQuantityAction action = ChangeQuantityAction.Minus;

        private readonly Mock<IBasketRedisService> basketRedisService;

        private readonly ChangeProductQuantityCommand command;
        private readonly ChangeProductQuantityCommandHandler commandHandler;
        private readonly UserBasketDto basketDto;

        public ChangeProductQuantityCommandHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
            command = new ChangeProductQuantityCommand { UserId = userId, ProductId = productId, ChangeQuantityAction = action };
            commandHandler = new ChangeProductQuantityCommandHandler(basketRedisService.Object);

            basketDto = new UserBasketDto
            {
                Products = new List<BasketProduct> { new BasketProduct { Id = productId } }
            };
        }

        [Fact]
        public async Task ChangeProductQuantityCommandHandler_NullBasketProduct()
        {
            //Arrange
            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult(new UserBasketDto()));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
        }

        [Fact]
        public async Task ChangeProductQuantityCommandHandler_Success()
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
