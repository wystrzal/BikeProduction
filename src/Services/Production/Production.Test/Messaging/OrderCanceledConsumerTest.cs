using BaseRepository.Exceptions;
using BikeExtensions;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Production.Application.Messaging.Consumers;
using Production.Core.Interfaces;
using Production.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Production.Test.Messaging
{
    public class OrderCanceledConsumerTest
    {
        private const int id = 1;

        private readonly Mock<IProductionQueueRepo> productionQueueRepo;
        private readonly Mock<ILogger<OrderCanceledConsumer>> logger;

        private readonly OrderCanceledConsumer consumer;
        private readonly List<ProductionQueue> productionQueues;

        public OrderCanceledConsumerTest()
        {
            productionQueueRepo = new Mock<IProductionQueueRepo>();
            logger = new Mock<ILogger<OrderCanceledConsumer>>();
            consumer = new OrderCanceledConsumer(productionQueueRepo.Object, logger.Object);
            productionQueues = new List<ProductionQueue> { new ProductionQueue(), new ProductionQueue() };
        }

        [Fact]
        public async Task OrderCanceledConsumer_Success()
        {
            //Arrange
            var context = GetContext();

            productionQueueRepo.Setup(x => x.GetByConditionToList(It.IsAny<Func<ProductionQueue, bool>>()))
                .Returns(Task.FromResult(productionQueues));

            //Act
            await consumer.Consume(context);

            //Assert
            productionQueueRepo.Verify(x => x.Delete(It.IsAny<ProductionQueue>()), Times.Exactly(productionQueues.Count));
            productionQueueRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task OrderCanceledConsumer_ThrowsNullDataException()
        {
            //Arrange
            var productionQueues = new List<ProductionQueue>();
            var context = GetContext();

            productionQueueRepo.Setup(x => x.GetByConditionToList(It.IsAny<Func<ProductionQueue, bool>>()))
                .Returns(Task.FromResult(productionQueues));

            //Assert
            await Assert.ThrowsAsync<NullDataException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task OrderCanceledConsumer_ThrowsArgumentException()
        {
            //Arrange
            var context = GetContext(It.IsAny<int>());

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task OrderCanceledConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            var context = GetContext();

            productionQueueRepo.Setup(x => x.GetByConditionToList(It.IsAny<Func<ProductionQueue, bool>>()))
                .Returns(Task.FromResult(productionQueues));

            productionQueueRepo.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(ProductionQueue)));

            //Assert
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            productionQueueRepo.Verify(x => x.Delete(It.IsAny<ProductionQueue>()), Times.Exactly(productionQueues.Count));
            logger.VerifyLogging(LogLevel.Error);
        }

        private ConsumeContext<OrderCanceledEvent> GetContext(int id = id)
        {
            var orderCanceledEvent = new OrderCanceledEvent(id);
            return Mock.Of<ConsumeContext<OrderCanceledEvent>>(x => x.Message == orderCanceledEvent);
        }
    }
}
