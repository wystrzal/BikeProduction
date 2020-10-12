using BikeBaseRepository;
using BikeExtensions;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Messaging.Consumers;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Xunit;

namespace Warehouse.Test.Messaging
{
    public class ProductionConfirmedConsumerTest
    {
        private readonly Mock<IProductPartRepo> productPartRepo;
        private readonly Mock<ILogger<ProductionConfirmedConsumer>> logger;

        public ProductionConfirmedConsumerTest()
        {
            productPartRepo = new Mock<IProductPartRepo>();
            logger = new Mock<ILogger<ProductionConfirmedConsumer>>();
        }

        [Fact]
        public async Task ProductionConfirmedConsumer_Success()
        {
            //Arrange
            var reference = "test";
            var parts = new List<Part> { new Part() };
            var productionConfirmedEvent = new ProductionConfirmedEvent { Reference = reference };
            var context = Mock.Of<ConsumeContext<ProductionConfirmedEvent>>(x => x.Message == productionConfirmedEvent);

            productPartRepo.Setup(x => x.GetPartsForProduction(It.IsAny<string>())).Returns(Task.FromResult(parts));

            var consumer = new ProductionConfirmedConsumer(productPartRepo.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            productPartRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }


        [Fact]
        public async Task ProductionConfirmedConsumer_ThrowsArgumentNullException()
        {
            //Arrange
            var productionConfirmedEvent = new ProductionConfirmedEvent();
            var context = Mock.Of<ConsumeContext<ProductionConfirmedEvent>>(x => x.Message == productionConfirmedEvent);

            var consumer = new ProductionConfirmedConsumer(productPartRepo.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task ProductionConfirmedConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            var reference = "test";
            var parts = new List<Part> { new Part() };
            var productionConfirmedEvent = new ProductionConfirmedEvent { Reference = reference };
            var context = Mock.Of<ConsumeContext<ProductionConfirmedEvent>>(x => x.Message == productionConfirmedEvent);

            productPartRepo.Setup(x => x.GetPartsForProduction(It.IsAny<string>())).Returns(Task.FromResult(parts));
            productPartRepo.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(ProductsParts)));

            var consumer = new ProductionConfirmedConsumer(productPartRepo.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
