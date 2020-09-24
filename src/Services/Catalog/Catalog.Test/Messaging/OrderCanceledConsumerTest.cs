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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test.Messaging
{
    public class OrderCanceledConsumerTest
    {
        private readonly Mock<IChangeProductsPopularityService> changeProductsPopularityService;
        private readonly Mock<ILogger<OrderCanceledConsumer>> logger;

        public OrderCanceledConsumerTest()
        {
            changeProductsPopularityService = new Mock<IChangeProductsPopularityService>();
            logger = new Mock<ILogger<OrderCanceledConsumer>>();
        }

        [Fact]
        public async Task OrderCanceledConsumer_Success()
        {
            //Arrange
            var orderItems = new List<OrderItem> { new OrderItem() };

            var orderCanceledEvent = new OrderCanceledEvent { OrderItems = orderItems };

            var context = Mock.Of<ConsumeContext<OrderCanceledEvent>>(x =>
                x.Message == orderCanceledEvent);

            changeProductsPopularityService
                .Setup(x => x.ChangeProductsPopularity(It.IsAny<List<OrderItem>>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(true)).Verifiable();

            var consumer = new OrderCanceledConsumer(changeProductsPopularityService.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            changeProductsPopularityService
                .Verify(x => x.ChangeProductsPopularity(It.IsAny<List<OrderItem>>(), It.IsAny<bool>()), Times.Once);
        }
    }
}
