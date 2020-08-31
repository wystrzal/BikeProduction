using Basket.Application.Messaging.Consumers;
using Basket.Core.Interfaces;
using Castle.Core.Logging;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Test
{
    public class MessagingTest
    {
        private readonly Mock<IBasketRedisService> basketRedisService;
        public MessagingTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
        }

        [Fact]
        public async Task OrderCreatedConsumer_Success()
        {
            //Arrange
            var logger = new Mock<ILogger<OrderCreatedConsumer>>();

            var userId = "1";
            var orderCreatedEvent = new OrderCreatedEvent { UserId = userId };

            var context = Mock.Of<ConsumeContext<OrderCreatedEvent>>(x => x.Message == orderCreatedEvent);

            basketRedisService.Setup(x => x.RemoveBasket(userId)).Verifiable();

            var consumer = new OrderCreatedConsumer(basketRedisService.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            basketRedisService.Verify(x => x.RemoveBasket(userId), Times.Once);
        }
    }
}
