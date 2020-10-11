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
            var orderCanceledEvent = new OrderCanceledEvent();

            var context = Mock.Of<ConsumeContext<OrderCanceledEvent>>(x => x.Message == orderCanceledEvent);

            var consumer = new OrderCanceledConsumer(changeProductsPopularityService.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            changeProductsPopularityService
                .Verify(x => x.ChangeProductsPopularity(It.IsAny<List<OrderItem>>(), It.IsAny<bool>()), Times.Once);
        }
    }
}
