﻿using Basket.Application.Messaging.Consumers;
using Basket.Core.Interfaces;
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
        private readonly Mock<IBasketRedisService> basketRedisService;
        private readonly Mock<ILogger<OrderCreatedConsumer>> logger;

        public OrderCreatedConsumerTest()
        {
            basketRedisService = new Mock<IBasketRedisService>();
            logger = new Mock<ILogger<OrderCreatedConsumer>>();
        }

        [Fact]
        public async Task OrderCreatedConsumer_Success()
        {
            //Arrange
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