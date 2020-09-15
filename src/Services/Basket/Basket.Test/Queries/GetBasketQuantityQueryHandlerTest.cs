using Basket.Application.Queries;
using Basket.Application.Queries.Handlers;
using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Test.Queries
{
    public class GetBasketQuantityQueryHandlerTest
    {
        private readonly Mock<IBasketRedisService> basketRedisService;

        public GetBasketQuantityQueryHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
        }

        [Fact]
        public async Task GetBasketQuantityQueryHandler_Success()
        {
            //Arrange
            var userId = "1";
            var query = new GetBasketQuantityQuery(userId);
            var userBasketDto = new UserBasketDto
            {
                Products = new List<BasketProduct> { new BasketProduct(), new BasketProduct() }
            };

            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult(userBasketDto));

            var queryHandler = new GetBasketQuantityQueryHandler(basketRedisService.Object);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(userBasketDto.Products.Count, action);
        }

        [Fact]
        public async Task GetBasketQuantityQueryHandler_NullBaset_Success()
        {
            //Arrange
            var userId = "1";
            var query = new GetBasketQuantityQuery(userId);

            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult((UserBasketDto)null));

            var queryHandler = new GetBasketQuantityQueryHandler(basketRedisService.Object);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(0, action);
        }
    }
}
