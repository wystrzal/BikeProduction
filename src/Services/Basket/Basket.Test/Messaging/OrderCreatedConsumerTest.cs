using Basket.Application.Messaging.Consumers;
using Basket.Core.Interfaces;
using BikeExtensions;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Test.Messaging
{
    public class OrderCreatedConsumerTest
    {     
        private const string userId = "1";
      
        private readonly Mock<IBasketRedisService> basketRedisService;
        private readonly Mock<ILogger<OrderCreatedConsumer>> logger;
      
        private readonly OrderCreatedConsumer consumer;
   
        public OrderCreatedConsumerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
            logger = new Mock<ILogger<OrderCreatedConsumer>>();
            consumer = new OrderCreatedConsumer(basketRedisService.Object, logger.Object);
        }

        [Fact]
        public async Task OrderCreatedConsumer_Success()
        {
            //Arrange
            var context = GetContext();

            //Act
            await consumer.Consume(context);

            //Assert
            basketRedisService.Verify(x => x.RemoveBasket(userId), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task OrderCreatedConsumer_ThrowsException()
        {
            //Arrange
            var context = GetContext();

            basketRedisService.Setup(x => x.RemoveBasket(userId)).ThrowsAsync(new Exception());

            //Assert
            await Assert.ThrowsAsync<Exception>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        private ConsumeContext<OrderCreatedEvent> GetContext()
        {
            var orderCreatedEvent = new OrderCreatedEvent { UserId = userId };

            return Mock.Of<ConsumeContext<OrderCreatedEvent>>(x => x.Message == orderCreatedEvent);
        }
    }
}
