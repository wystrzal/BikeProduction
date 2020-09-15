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
        private readonly Mock<IBasketRedisService> basketRedisService;

        public GetBasketQueryHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
        }

        [Fact]
        public async Task GetBasketQueryHandler_Success()
        {
            //Arrange 
            var userId = "1";
            var query = new GetBasketQuery(userId);
            var userBasketDto = new UserBasketDto { UserId = userId };

            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult(userBasketDto));

            var queryHandler = new GetBasketQueryHandler(basketRedisService.Object);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(userId, action.UserId);
        }
    }
}
