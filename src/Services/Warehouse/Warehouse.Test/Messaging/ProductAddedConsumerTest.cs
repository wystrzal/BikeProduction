using AutoMapper;
using BaseRepository.Exceptions;
using BikeExtensions;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Warehouse.Application.Messaging.Consumers;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Xunit;

namespace Warehouse.Test.Messaging
{
    public class ProductAddedConsumerTest
    {
        private const string productName = "test";
        private const string reference = "test";

        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IMapper> mapper;
        private readonly Mock<ILogger<ProductAddedConsumer>> logger;

        private readonly ProductAddedConsumer consumer;
        private readonly Product product;

        public ProductAddedConsumerTest()
        {
            productRepository = new Mock<IProductRepository>();
            mapper = new Mock<IMapper>();
            logger = new Mock<ILogger<ProductAddedConsumer>>();
            consumer = new ProductAddedConsumer(productRepository.Object, mapper.Object, logger.Object);
            product = new Product();
        }

        [Fact]
        public async Task ProductAddedConsumer_Success()
        {
            //Arrange
            var context = GetContext();

            mapper.Setup(x => x.Map<Product>(context.Message)).Returns(product);
            
            //Act
            await consumer.Consume(context);

            //Assert
            productRepository.Verify(x => x.Add(product), Times.Once);
            productRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task ProductAddedConsumer_NullProductName_ThrowsArgumentNullException()
        {
            //Arrange
            var context = GetContext(It.IsAny<string>());

            mapper.Setup(x => x.Map<Product>(context.Message)).Returns(product);

            //Act
            await Assert.ThrowsAsync<ArgumentNullException>(() => consumer.Consume(context));

            //Assert
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task ProductAddedConsumer_NullReference_ThrowsArgumentNullException()
        {
            //Arrange
            var context = GetContext(reference: It.IsAny<string>());

            mapper.Setup(x => x.Map<Product>(context.Message)).Returns(product);

            //Act
            await Assert.ThrowsAsync<ArgumentNullException>(() => consumer.Consume(context));

            //Assert
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task ProductAddedConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            var context = GetContext();

            mapper.Setup(x => x.Map<Product>(context.Message)).Returns(product);
            productRepository.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(Product)));

            //Assert
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            productRepository.Verify(x => x.Add(product), Times.Once);
            logger.VerifyLogging(LogLevel.Error);
        }

        private ConsumeContext<ProductAddedEvent> GetContext(string productName = productName, string reference = reference)
        {
            var productAddedEvent = new ProductAddedEvent(productName, reference);
            return Mock.Of<ConsumeContext<ProductAddedEvent>>(x => x.Message == productAddedEvent);
        }
    }
}
