using Basket.Application.Queries;
using Basket.Application.Queries.Handlers;
using Basket.Core.Dtos;
using Basket.Core.Interfaces;
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

        public GetBasketQueryHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
            query = new GetBasketQuery(userId);
            queryHandler = new GetBasketQueryHandler(basketRedisService.Object);
        }

        [Fact]
        public async Task GetBasketQueryHandler_Success()
        {
            //Arrange 
            var userBasketDto = new UserBasketDto { UserId = userId };

            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult(userBasketDto));

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(userId, action.UserId);
        }
    }
}
