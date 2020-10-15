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
        private const string userId = "1";
        private const int nullBasketQuantity = 0;
        
        private readonly Mock<IBasketRedisService> basketRedisService;
        
        private readonly GetBasketQuantityQuery query;
        private readonly GetBasketQuantityQueryHandler queryHandler;    

        public GetBasketQuantityQueryHandlerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
            query = new GetBasketQuantityQuery(userId);
            queryHandler = new GetBasketQuantityQueryHandler(basketRedisService.Object);
        }

        [Fact]
        public async Task GetBasketQuantityQueryHandler_Success()
        {
            //Arrange         
            var userBasketDto = new UserBasketDto
            {
                Products = new List<BasketProduct> { new BasketProduct(), new BasketProduct() }
            };

            basketRedisService.Setup(x => x.GetBasket(userId)).Returns(Task.FromResult(userBasketDto));

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(userBasketDto.Products.Count, action);
        }

        [Fact]
        public async Task GetBasketQuantityQueryHandler_NullBasket()
        {
            //Arrange
            basketRedisService.Setup(x => x.GetBasket(It.IsAny<string>())).Returns(Task.FromResult((UserBasketDto)null));

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(nullBasketQuantity, action);
        }
    }
}
