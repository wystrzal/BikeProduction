using BaseRepository.Exceptions;
using BikeExtensions;
using Common.Application.Messaging;
using Delivery.Application.Messaging.Consumers;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Messaging
{
    public class ProductionFinishedConsumerTest
    {
        private readonly Mock<ICustomerOrderService> customerOrderService;
        private readonly Mock<IPackToDeliveryRepo> packToDeliveryRepo;
        private readonly Mock<ILogger<ProductionFinishedConsumer>> logger;

        private readonly ProductionFinishedConsumer consumer;
        private readonly PackToDelivery packToDelivery;
        private readonly ConsumeContext<ProductionFinishedEvent> context;

        public ProductionFinishedConsumerTest()
        {
            customerOrderService = new Mock<ICustomerOrderService>();
            packToDeliveryRepo = new Mock<IPackToDeliveryRepo>();
            logger = new Mock<ILogger<ProductionFinishedConsumer>>();
            consumer = new ProductionFinishedConsumer(customerOrderService.Object, packToDeliveryRepo.Object, logger.Object);
            packToDelivery = new PackToDelivery();
            context = GetContext();
        }

        [Fact]
        public async Task ProductionFinishedConsumer_CatchNullDataException_Success()
        {
            //Arrange
            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Expression<Func<PackToDelivery, bool>>>()))
                .Throws<NullDataException>();

            customerOrderService.Setup(x => x.GetOrder(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(new Order()));

            //Act
            await consumer.Consume(context);

            //Assert
            packToDeliveryRepo.Verify(x => x.Add(It.IsAny<PackToDelivery>()), Times.Once);
            packToDeliveryRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task ProductionFinishedConsumer_CatchNullDataException_ThrowsException()
        {
            //Arrange
            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Expression<Func<PackToDelivery, bool>>>()))
                .Throws<NullDataException>();

            customerOrderService.Setup(x => x.GetOrder(It.IsAny<int>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            //Assert
            await Assert.ThrowsAsync<Exception>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task ProductionFinishedConsumer_Success()
        {
            //Arrange
            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Expression<Func<PackToDelivery, bool>>>()))
                .Returns(Task.FromResult(packToDelivery));

            //Act
            await consumer.Consume(context);

            //Assert
            packToDeliveryRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task ProductionFinishedConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Expression<Func<PackToDelivery, bool>>>()))
                .Returns(Task.FromResult(packToDelivery));

            packToDeliveryRepo.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(PackToDelivery)));

            //Assert
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        private ConsumeContext<ProductionFinishedEvent> GetContext()
        {
            var productionFinishedEvent = new ProductionFinishedEvent(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            return Mock.Of<ConsumeContext<ProductionFinishedEvent>>(x => x.Message == productionFinishedEvent);
        }
    }
}
