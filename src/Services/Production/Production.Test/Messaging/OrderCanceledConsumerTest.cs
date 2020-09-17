﻿using Common.Application.Messaging;
using MassTransit;
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

        public OrderCanceledConsumerTest()
        {
            productionQueueRepo = new Mock<IProductionQueueRepo>();
        }

        [Fact]
        public async Task OrderCanceledConsumer_Success()
        {
            //Arrange
            var id = 1;
            var productionQueues = new List<ProductionQueue> { new ProductionQueue() };
            var orderCanceledEvent = new OrderCanceledEvent(id);
            var context = Mock.Of<ConsumeContext<OrderCanceledEvent>>(x => x.Message == orderCanceledEvent);

            productionQueueRepo.Setup(x => x.GetByConditionToList(It.IsAny<Func<ProductionQueue, bool>>()))
                .Returns(Task.FromResult(productionQueues));
            productionQueueRepo.Setup(x => x.Delete(It.IsAny<ProductionQueue>())).Verifiable();
            productionQueueRepo.Setup(x => x.SaveAllAsync()).Verifiable();

            var consumer = new OrderCanceledConsumer(productionQueueRepo.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            productionQueueRepo.Verify(x => x.Delete(It.IsAny<ProductionQueue>()), Times.Once);
            productionQueueRepo.Verify(x => x.SaveAllAsync(), Times.Once);
        }
    }
}
