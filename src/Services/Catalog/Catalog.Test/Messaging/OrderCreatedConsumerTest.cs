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

        public OrderCreatedConsumerTest()
        {
            changeProductsPopularityService = new Mock<IChangeProductsPopularityService>();
            logger = new Mock<ILogger<OrderCreatedConsumer>>();
        }

        [Fact]
        public async Task OrderCreatedConsumer_Success()
        {
            //Arrange
            var orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    Id = 1,
                    Price = 1,
                    ProductName = "Test",
                    Quantity = 1,
                    Reference = "1"
                }
            };

            var orderCreatedEvent = new OrderCreatedEvent { OrderItems = orderItems };

            var context = Mock.Of<ConsumeContext<OrderCreatedEvent>>(x =>
                x.Message == orderCreatedEvent);

            changeProductsPopularityService
                .Setup(x => x.ChangeProductsPopularity(It.IsAny<List<OrderItem>>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(true)).Verifiable();

            var consumer = new OrderCreatedConsumer(changeProductsPopularityService.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            changeProductsPopularityService
                .Verify(x => x.ChangeProductsPopularity(It.IsAny<List<OrderItem>>(), It.IsAny<bool>()), Times.Once);
        }
    }
}
