﻿using BikeExtensions;
using Catalog.Application.Messaging.Consumers;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Core.Models.MessagingModels;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test.Messaging
{
    public class OrderCreatedConsumerTest
    {
        private readonly Mock<IChangeProductsPopularityService> changeProductsPopularityService;
        private readonly Mock<ILogger<OrderCreatedConsumer>> logger;

        private readonly OrderCreatedConsumer consumer;

        public OrderCreatedConsumerTest()
        {
            changeProductsPopularityService = new Mock<IChangeProductsPopularityService>();
            logger = new Mock<ILogger<OrderCreatedConsumer>>();
            consumer = new OrderCreatedConsumer(changeProductsPopularityService.Object, logger.Object);
        }

        [Fact]
        public async Task OrderCreatedConsumer_Success()
        {
            //Arrange
            var context = GetContext();

            //Act
            await consumer.Consume(context);

            //Assert
            changeProductsPopularityService
                .Verify(x => x.ChangeProductsPopularity(It.IsAny<List<OrderItem>>(), It.IsAny<bool>()), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task OrderCreatedConsumer_ThrowsException()
        {
            //Arrange
            var context = GetContext();

            changeProductsPopularityService
                .Setup(x => x.ChangeProductsPopularity(It.IsAny<List<OrderItem>>(), It.IsAny<bool>())).ThrowsAsync(new Exception());

            //Assert
            await Assert.ThrowsAsync<Exception>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        private ConsumeContext<OrderCreatedEvent> GetContext()
        {
            var orderCreatedEvent = new OrderCreatedEvent();

            return Mock.Of<ConsumeContext<OrderCreatedEvent>>(x => x.Message == orderCreatedEvent);
        }
    }
}
