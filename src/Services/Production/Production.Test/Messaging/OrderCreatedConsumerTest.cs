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
        private const int id = 1;

        private readonly Mock<IProductionQueueRepo> productionQueueRepo;
        private readonly Mock<ILogger<OrderCreatedConsumer>> logger;

        private readonly OrderCreatedConsumer consumer;
        private readonly List<OrderItem> orderItems;

        public OrderCreatedConsumerTest()
        {
            productionQueueRepo = new Mock<IProductionQueueRepo>();
            logger = new Mock<ILogger<OrderCreatedConsumer>>();
            consumer = new OrderCreatedConsumer(productionQueueRepo.Object, logger.Object);
            orderItems = new List<OrderItem> { new OrderItem(), new OrderItem() };
        }

        [Fact]
        public async Task OrderCreatedConsumer_Success()
        {
            //Arrange          
            var context = GetContext(orderItems);

            productionQueueRepo.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

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
            var orderItems = new List<OrderItem>();
            var context = GetContext(orderItems);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task OrderCreatedConsumer_ThrowsArgumentException()
        {
            //Arrange
            var context = GetContext(orderItems, It.IsAny<int>());

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task OrderCreatedConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            var context = GetContext(orderItems);

            productionQueueRepo.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(ProductionQueue)));

            //Assert
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            productionQueueRepo.Verify(x => x.Add(It.IsAny<ProductionQueue>()), Times.Exactly(orderItems.Count));
            logger.VerifyLogging(LogLevel.Error);
        }

        private ConsumeContext<OrderCreatedEvent> GetContext(List<OrderItem> orderItems, int id = id)
        {
            var orderCreatedEvent = new OrderCreatedEvent(orderItems, id);
            return Mock.Of<ConsumeContext<OrderCreatedEvent>>(x => x.Message == orderCreatedEvent);
        }
    }
}
