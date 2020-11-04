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
    public class GetBasketQueryHandlerTest
    {
        private const string userId = "1";

        private readonly Mock<IBasketRedisService> basketRedisService;

        private readonly GetBasketQuery query;
        private readonly GetBasketQueryHandler queryHandler;
        private readonly UserBasketDto basketDto;

        public GetBasketQueryHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
            query = new GetBasketQuery(userId);
            queryHandler = new GetBasketQueryHandler(basketRedisService.Object);
            basketDto = new UserBasketDto { Products = new List<BasketProduct> { new BasketProduct() } };
        }

        [Fact]
        public async Task GetBasketQueryHandler_Success()
        {
            //Arrange 
            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult(basketDto));

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(basketDto.Products.Count, action.Products.Count);
        }
    }
}
