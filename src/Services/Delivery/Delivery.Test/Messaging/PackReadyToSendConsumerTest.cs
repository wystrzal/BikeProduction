using BikeBaseRepository;
using BikeExtensions;
using Common.Application.Commands;
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

        private readonly PackReadyToSendConsumer consumer;
        private readonly ConsumeContext<PackReadyToSendEvent> context;
        private readonly PackToDelivery pack;

        public PackReadyToSendConsumerTest()
        {
            packToDeliveryRepo = new Mock<IPackToDeliveryRepo>();
            bus = new Mock<IBus>();
            logger = new Mock<ILogger<PackReadyToSendConsumer>>();
            consumer = new PackReadyToSendConsumer(packToDeliveryRepo.Object, bus.Object, logger.Object);
            context = GetContext();
            pack = new PackToDelivery();
        }

        [Fact]
        public async Task PackReadyToSendConsumer_Success()
        {
            //Arrange
            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<PackToDelivery, bool>>()))
                .Returns(Task.FromResult(pack));

            //Act
            await consumer.Consume(context);

            //Assert
            packToDeliveryRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            bus.Verify(x => x.Publish(It.IsAny<ChangeOrderStatusEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task PackReadyToSendConsumer_ThrowsNullDataException()
        {
            //Arrange
            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<PackToDelivery, bool>>()))
                .ThrowsAsync(new NullDataException());

            //Assert
            await Assert.ThrowsAsync<NullDataException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task PackReadyToSendConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange          
            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<PackToDelivery, bool>>()))
                .Returns(Task.FromResult(pack));

            packToDeliveryRepo.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(PackToDelivery)));

            //Assert
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        private ConsumeContext<PackReadyToSendEvent> GetContext()
        {
            var packReadyToSendEvent = new PackReadyToSendEvent(It.IsAny<int>());
            return Mock.Of<ConsumeContext<PackReadyToSendEvent>>(x => x.Message == packReadyToSendEvent);
        }
    }
}
