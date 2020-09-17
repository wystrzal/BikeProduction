using Castle.Core.Logging;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Production.Application.Messaging.Consumers;
using Production.Application.Messaging.MessagingModels;
using Production.Core.Exceptions;
using Production.Core.Interfaces;
using Production.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Production.Test.Messaging
{
    public class OrderCreatedConsumerTest
    {
        private readonly Mock<IProductionQueueRepo> productionQueueRepo;
        private readonly Mock<ILogger<OrderCreatedConsumer>> logger;

        public OrderCreatedConsumerTest()
        {
            productionQueueRepo = new Mock<IProductionQueueRepo>();
            logger = new Mock<ILogger<OrderCreatedConsumer>>();
        }

        [Fact]
        public async Task OrderCreatedConsumer_Success()
        {
            //Arrange
            var orderItems = new List<OrderItem> { new OrderItem() };
            var id = 1;
            var orderCreatedEvent = new OrderCreatedEvent(orderItems, id);
            var context = Mock.Of<ConsumeContext<OrderCreatedEvent>>(x => x.Message == orderCreatedEvent);

            productionQueueRepo.Setup(x => x.Add(It.IsAny<ProductionQueue>())).Verifiable();
            productionQueueRepo.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            var consumer = new OrderCreatedConsumer(productionQueueRepo.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            productionQueueRepo.Verify(x => x.Add(It.IsAny<ProductionQueue>()), Times.Once);
        }

        [Fact]
        public async Task OrderCreatedConsumer_ThrowsProductNotAddedException()
        {
            //Arrange
            var orderItems = new List<OrderItem> { new OrderItem() };
            var id = 1;
            var orderCreatedEvent = new OrderCreatedEvent(orderItems, id);
            var context = Mock.Of<ConsumeContext<OrderCreatedEvent>>(x => x.Message == orderCreatedEvent);

            productionQueueRepo.Setup(x => x.Add(It.IsAny<ProductionQueue>())).Verifiable();
            productionQueueRepo.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(false));

            var consumer = new OrderCreatedConsumer(productionQueueRepo.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<ProductNotAddedException>(() => consumer.Consume(context));
        }
    }
}
