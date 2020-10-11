using BikeBaseRepository;
using Common.Application.Messaging;
using Delivery.Application.Messaging.Consumers;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Messaging
{
    public class ProductionFinishedConsumerTest
    {
        private readonly Mock<ICustomerOrderService> customerOrderService;
        private readonly Mock<IPackToDeliveryRepo> packToDeliveryRepo;
        private readonly Mock<ILogger<ProductionFinishedConsumer>> logger;

        public ProductionFinishedConsumerTest()
        {
            customerOrderService = new Mock<ICustomerOrderService>();
            packToDeliveryRepo = new Mock<IPackToDeliveryRepo>();
            logger = new Mock<ILogger<ProductionFinishedConsumer>>();
        }

        [Fact]
        public async Task ProductionFinishedConsumer_NullDataException_Success()
        {
            //Arrange
            var productionFinishedEvent = new ProductionFinishedEvent(It.IsAny<int>(), It.IsAny<int>());
            var context = Mock.Of<ConsumeContext<ProductionFinishedEvent>>(x => x.Message == productionFinishedEvent);

            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<PackToDelivery, bool>>()))
                .Throws<NullDataException>();

            customerOrderService.Setup(x => x.GetOrder(It.IsAny<int>())).Returns(Task.FromResult(new Order()));

            var consumer = new ProductionFinishedConsumer(customerOrderService.Object, packToDeliveryRepo.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            packToDeliveryRepo.Verify(x => x.Add(It.IsAny<PackToDelivery>()), Times.Once);
            packToDeliveryRepo.Verify(x => x.SaveAllAsync(), Times.Once);
        }

        [Fact]
        public async Task ProductionFinishedConsumer_Success()
        {
            //Arrange
            var productionFinishedEvent = new ProductionFinishedEvent(It.IsAny<int>(), It.IsAny<int>());
            var context = Mock.Of<ConsumeContext<ProductionFinishedEvent>>(x => x.Message == productionFinishedEvent);
            var packToDelivery = new PackToDelivery();

            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<PackToDelivery, bool>>()))
                .Returns(Task.FromResult(packToDelivery));

            var consumer = new ProductionFinishedConsumer(customerOrderService.Object, packToDeliveryRepo.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            packToDeliveryRepo.Verify(x => x.SaveAllAsync(), Times.Once);
        }
    }
}
