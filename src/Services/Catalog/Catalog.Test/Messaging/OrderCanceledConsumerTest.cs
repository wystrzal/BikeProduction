﻿using BikeExtensions;
using Catalog.Application.Messaging.Consumers;
using Catalog.Core.Interfaces;
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
    public class OrderCanceledConsumerTest
    {
        private readonly Mock<IChangeProductsPopularityService> changeProductsPopularityService;
        private readonly Mock<ILogger<OrderCanceledConsumer>> logger;

        private readonly OrderCanceledConsumer consumer;
        private readonly ConsumeContext<OrderCanceledEvent> context;
        private readonly List<OrderItem> orderItems;

        public OrderCanceledConsumerTest()
        {
            changeProductsPopularityService = new Mock<IChangeProductsPopularityService>();
            logger = new Mock<ILogger<OrderCanceledConsumer>>();
            consumer = new OrderCanceledConsumer(changeProductsPopularityService.Object, logger.Object);
            orderItems = new List<OrderItem> { new OrderItem() };
            context = GetContext();
        }

        [Fact]
        public async Task OrderCanceledConsumer_Success()
        {
            //Act
            await consumer.Consume(context);

            //Assert
            changeProductsPopularityService
                .Verify(x => x.ChangeProductsPopularity(orderItems, It.IsAny<bool>()), Times.Once);

            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task OrderCanceledConsumer_ThrowsException()
        {
            //Arrange
            changeProductsPopularityService
                .Setup(x => x.ChangeProductsPopularity(orderItems, It.IsAny<bool>())).ThrowsAsync(new Exception());

            //Assert
            await Assert.ThrowsAsync<Exception>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        private ConsumeContext<OrderCanceledEvent> GetContext()
        {
            var orderCanceledEvent = new OrderCanceledEvent { OrderItems = orderItems };
            return Mock.Of<ConsumeContext<OrderCanceledEvent>>(x => x.Message == orderCanceledEvent);
        }
    }
}
