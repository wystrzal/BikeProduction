using BikeBaseRepository;
using BikeExtensions;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Production.Application.Messaging.Consumers;
using Production.Core.Interfaces;
using Production.Core.Models;
using Production.Core.Models.MessagingModels;
using System;
using System.Collections.Generic;
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
            var id = 1;
            var orderItems = new List<OrderItem> { new OrderItem(), new OrderItem() };
            var orderCreatedEvent = new OrderCreatedEvent(orderItems, id);
            var context = Mock.Of<ConsumeContext<OrderCreatedEvent>>(x => x.Message == orderCreatedEvent);

            productionQueueRepo.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            var consumer = new OrderCreatedConsumer(productionQueueRepo.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            productionQueueRepo.Verify(x => x.Add(It.IsAny<ProductionQueue>()), Times.Exactly(orderItems.Count));
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task OrderCreatedConsumer_ThrowsArgumentNullException()
        {
            //Arrange
            var id = 1;
            var orderItems = new List<OrderItem>();
            var orderCreatedEvent = new OrderCreatedEvent(orderItems, id);
            var context = Mock.Of<ConsumeContext<OrderCreatedEvent>>(x => x.Message == orderCreatedEvent);

            var consumer = new OrderCreatedConsumer(productionQueueRepo.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task OrderCreatedConsumer_ThrowsException()
        {
            //Arrange
            var orderItems = new List<OrderItem> { new OrderItem() };
            var orderCreatedEvent = new OrderCreatedEvent(orderItems, It.IsAny<int>());
            var context = Mock.Of<ConsumeContext<OrderCreatedEvent>>(x => x.Message == orderCreatedEvent);

            var consumer = new OrderCreatedConsumer(productionQueueRepo.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task OrderCreatedConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            var id = 1;
            var orderItems = new List<OrderItem> { new OrderItem(), new OrderItem() };
            var orderCreatedEvent = new OrderCreatedEvent(orderItems, id);
            var context = Mock.Of<ConsumeContext<OrderCreatedEvent>>(x => x.Message == orderCreatedEvent);

            productionQueueRepo.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(ProductionQueue)));

            var consumer = new OrderCreatedConsumer(productionQueueRepo.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            productionQueueRepo.Verify(x => x.Add(It.IsAny<ProductionQueue>()), Times.Exactly(orderItems.Count));
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
