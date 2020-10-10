﻿using Common.Application.Commands;
using Common.Application.Messaging;
using Delivery.Application.Messaging.Consumers;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Messaging
{
    public class PackReadyToSendConsumerTest
    {
        private readonly Mock<IPackToDeliveryRepo> packToDeliveryRepo;
        private readonly Mock<IBus> bus;
        private readonly Mock<ILogger<PackReadyToSendConsumer>> logger;

        public PackReadyToSendConsumerTest()
        {
            packToDeliveryRepo = new Mock<IPackToDeliveryRepo>();
            bus = new Mock<IBus>();
            logger = new Mock<ILogger<PackReadyToSendConsumer>>();
        }

        [Fact]
        public async Task PackReadyToSendConsumer_Success()
        {
            //Arrange
            var packReadyToSendEvent = new PackReadyToSendEvent(1);
            var context = Mock.Of<ConsumeContext<PackReadyToSendEvent>>(x => x.Message == packReadyToSendEvent);
            var pack = new PackToDelivery();

            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<PackToDelivery, bool>>())).Returns(Task.FromResult(pack));

            var consumer = new PackReadyToSendConsumer(packToDeliveryRepo.Object, bus.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            packToDeliveryRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            bus.Verify(x => x.Publish(It.IsAny<ChangeOrderStatusEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
