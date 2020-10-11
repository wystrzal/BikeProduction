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
            var productionQueues = new List<ProductionQueue> { new ProductionQueue() };
            var orderCanceledEvent = new OrderCanceledEvent(It.IsAny<int>());
            var context = Mock.Of<ConsumeContext<OrderCanceledEvent>>(x => x.Message == orderCanceledEvent);

            productionQueueRepo.Setup(x => x.GetByConditionToList(It.IsAny<Func<ProductionQueue, bool>>()))
                .Returns(Task.FromResult(productionQueues));

            var consumer = new OrderCanceledConsumer(productionQueueRepo.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            productionQueueRepo.Verify(x => x.Delete(It.IsAny<ProductionQueue>()), Times.Once);
            productionQueueRepo.Verify(x => x.SaveAllAsync(), Times.Once);
        }
    }
}
