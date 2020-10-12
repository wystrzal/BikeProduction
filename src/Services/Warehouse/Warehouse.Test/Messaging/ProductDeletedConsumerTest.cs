using BikeBaseRepository;
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
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<ILogger<ProductDeletedConsumer>> logger;

        public ProductDeletedConsumerTest()
        {
            productRepository = new Mock<IProductRepository>();
            logger = new Mock<ILogger<ProductDeletedConsumer>>();
        }

        [Fact]
        public async Task ProductDeletedConsumer_Success()
        {
            //Arrange
            var product = new Product();
            var productDeletedEvent = new ProductDeletedEvent(It.IsAny<string>());
            var context = Mock.Of<ConsumeContext<ProductDeletedEvent>>(x => x.Message == productDeletedEvent);

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>())).Returns(Task.FromResult(product));

            var consumer = new ProductDeletedConsumer(productRepository.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            productRepository.Verify(x => x.Delete(product), Times.Once);
            productRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task ProductDeletedConsumer_ThrowsNullDataException()
        {
            //Arrange
            var productDeletedEvent = new ProductDeletedEvent(It.IsAny<string>());
            var context = Mock.Of<ConsumeContext<ProductDeletedEvent>>(x => x.Message == productDeletedEvent);

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>())).ThrowsAsync(new NullDataException());

            var consumer = new ProductDeletedConsumer(productRepository.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<NullDataException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task ProductDeletedConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            var product = new Product();
            var productDeletedEvent = new ProductDeletedEvent(It.IsAny<string>());
            var context = Mock.Of<ConsumeContext<ProductDeletedEvent>>(x => x.Message == productDeletedEvent);

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>())).Returns(Task.FromResult(product));
            productRepository.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(Product)));

            var consumer = new ProductDeletedConsumer(productRepository.Object, logger.Object);

            //Act
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            productRepository.Verify(x => x.Delete(product), Times.Once);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
