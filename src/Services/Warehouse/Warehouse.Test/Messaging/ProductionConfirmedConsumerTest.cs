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
            var parts = new List<Part> { new Part() };
            var productionConfirmedEvent = new ProductionConfirmedEvent();
            var context = Mock.Of<ConsumeContext<ProductionConfirmedEvent>>(x => x.Message == productionConfirmedEvent);

            productPartRepo.Setup(x => x.GetPartsForCheckAvailability(It.IsAny<string>()))
                .Returns(Task.FromResult(parts));

            productPartRepo.Setup(x => x.SaveAllAsync()).Verifiable();

            var consumer = new ProductionConfirmedConsumer(productPartRepo.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            productPartRepo.Verify(x => x.SaveAllAsync(), Times.Once);
        }
    }
}
