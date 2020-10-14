using BikeBaseRepository;
using BikeExtensions;
using Castle.Core.Logging;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Production.Application.Messaging.Consumers;
using Production.Core.Interfaces;
using Production.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Production.Test.Messaging
{
    public class OrderCanceledConsumerTest
    {
        private readonly Mock<IProductionQueueRepo> productionQueueRepo;
        private readonly Mock<ILogger<OrderCanceledConsumer>> logger;

        public OrderCanceledConsumerTest()
        {
            productionQueueRepo = new Mock<IProductionQueueRepo>();
            logger = new Mock<ILogger<OrderCanceledConsumer>>();
        }

        [Fact]
        public async Task OrderCanceledConsumer_Success()
        {
            //Arrange
            var id = 1;
            var productionQueues = new List<ProductionQueue> { new ProductionQueue(), new ProductionQueue() };
            var orderCanceledEvent = new OrderCanceledEvent(id);
            var context = Mock.Of<ConsumeContext<OrderCanceledEvent>>(x => x.Message == orderCanceledEvent);

            productionQueueRepo.Setup(x => x.GetByConditionToList(It.IsAny<Func<ProductionQueue, bool>>()))
                .Returns(Task.FromResult(productionQueues));

            var consumer = new OrderCanceledConsumer(productionQueueRepo.Object, logger.Object);

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
            var id = 1;
            var productionQueues = new List<ProductionQueue>();
            var orderCanceledEvent = new OrderCanceledEvent(id);
            var context = Mock.Of<ConsumeContext<OrderCanceledEvent>>(x => x.Message == orderCanceledEvent);

            productionQueueRepo.Setup(x => x.GetByConditionToList(It.IsAny<Func<ProductionQueue, bool>>()))
                .Returns(Task.FromResult(productionQueues));

            var consumer = new OrderCanceledConsumer(productionQueueRepo.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<NullDataException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task OrderCanceledConsumer_ThrowsArgumentException()
        {
            //Arrange
            var orderCanceledEvent = new OrderCanceledEvent(It.IsAny<int>());
            var context = Mock.Of<ConsumeContext<OrderCanceledEvent>>(x => x.Message == orderCanceledEvent);

            var consumer = new OrderCanceledConsumer(productionQueueRepo.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task OrderCanceledConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            var id = 1;
            var productionQueues = new List<ProductionQueue> { new ProductionQueue(), new ProductionQueue() };
            var orderCanceledEvent = new OrderCanceledEvent(id);
            var context = Mock.Of<ConsumeContext<OrderCanceledEvent>>(x => x.Message == orderCanceledEvent);

            productionQueueRepo.Setup(x => x.GetByConditionToList(It.IsAny<Func<ProductionQueue, bool>>()))
                .Returns(Task.FromResult(productionQueues));

            productionQueueRepo.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(ProductionQueue)));

            var consumer = new OrderCanceledConsumer(productionQueueRepo.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            productionQueueRepo.Verify(x => x.Delete(It.IsAny<ProductionQueue>()), Times.Exactly(productionQueues.Count));
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
