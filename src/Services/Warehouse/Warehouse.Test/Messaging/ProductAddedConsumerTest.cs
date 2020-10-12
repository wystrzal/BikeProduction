using AutoMapper;
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
    public class ProductAddedConsumerTest
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IMapper> mapper;
        private readonly Mock<ILogger<ProductAddedConsumer>> logger;

        public ProductAddedConsumerTest()
        {
            productRepository = new Mock<IProductRepository>();
            mapper = new Mock<IMapper>();
            logger = new Mock<ILogger<ProductAddedConsumer>>();
        }

        [Fact]
        public async Task ProductAddedConsumer_Success()
        {
            //Arrange
            var product = new Product();
            var productAddedEvent = new ProductAddedEvent(It.IsAny<string>(), It.IsAny<string>());
            var context = Mock.Of<ConsumeContext<ProductAddedEvent>>(x => x.Message == productAddedEvent);

            mapper.Setup(x => x.Map<Product>(context.Message)).Returns(product);

            var consumer = new ProductAddedConsumer(productRepository.Object, mapper.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            productRepository.Verify(x => x.Add(product), Times.Once);
            productRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task ProductAddedConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            var product = new Product();
            var productAddedEvent = new ProductAddedEvent(It.IsAny<string>(), It.IsAny<string>());
            var context = Mock.Of<ConsumeContext<ProductAddedEvent>>(x => x.Message == productAddedEvent);

            mapper.Setup(x => x.Map<Product>(context.Message)).Returns(product);
            productRepository.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(Product)));

            var consumer = new ProductAddedConsumer(productRepository.Object, mapper.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            productRepository.Verify(x => x.Add(product), Times.Once);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
