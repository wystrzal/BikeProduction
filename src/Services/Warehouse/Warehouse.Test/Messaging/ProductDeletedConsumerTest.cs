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
    public class ProductDeletedConsumerTest
    {
        private const string reference = "test";

        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<ILogger<ProductDeletedConsumer>> logger;

        private readonly ProductDeletedConsumer consumer;
        private readonly Product product;

        public ProductDeletedConsumerTest()
        {
            productRepository = new Mock<IProductRepository>();
            logger = new Mock<ILogger<ProductDeletedConsumer>>();
            consumer = new ProductDeletedConsumer(productRepository.Object, logger.Object);
            product = new Product();
        }

        [Fact]
        public async Task ProductDeletedConsumer_Success()
        {
            //Arrange     
            var context = GetContext();

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>())).Returns(Task.FromResult(product));          

            //Act
            await consumer.Consume(context);

            //Assert
            productRepository.Verify(x => x.Delete(product), Times.Once);
            productRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task ProductDeletedConsumer_ThrowsArgumentNullException()
        {
            //Arrange     
            var context = GetContext(It.IsAny<string>());

            //Act
            await Assert.ThrowsAsync<ArgumentNullException>(() => consumer.Consume(context));

            //Assert
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task ProductDeletedConsumer_ThrowsNullDataException()
        {
            //Arrange
            var context = GetContext();

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>())).ThrowsAsync(new NullDataException());

            //Assert
            await Assert.ThrowsAsync<NullDataException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task ProductDeletedConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            var context = GetContext();

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>())).Returns(Task.FromResult(product));
            productRepository.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(Product)));

            //Act
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            productRepository.Verify(x => x.Delete(product), Times.Once);
            logger.VerifyLogging(LogLevel.Error);
        }

        private ConsumeContext<ProductDeletedEvent> GetContext(string reference = reference)
        {
            var productDeletedEvent = new ProductDeletedEvent(reference);
            return Mock.Of<ConsumeContext<ProductDeletedEvent>>(x => x.Message == productDeletedEvent);
        }
    }
}
