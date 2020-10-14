using BikeBaseRepository;
using BikeExtensions;
using Castle.Core.Logging;
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
    public class ProductUpdatedConsumerTest
    {
        const string productName = "test";
        const string reference = "test";
        const string oldReference = "test";

        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<ILogger<ProductUpdatedConsumer>> logger;
        private readonly Product product;
        private readonly ProductUpdatedConsumer consumer;

        public ProductUpdatedConsumerTest()
        {
            productRepository = new Mock<IProductRepository>();
            logger = new Mock<ILogger<ProductUpdatedConsumer>>();
            product = new Product();
            consumer = new ProductUpdatedConsumer(productRepository.Object, logger.Object);
        }

        [Fact]
        public async Task ProductUpdatedConsumer_Success()
        {
            //Arrange
            var context = GetContext(productName, reference, oldReference);

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>())).Returns(Task.FromResult(product));

            //Act
            await consumer.Consume(context);

            //Assert
            productRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            logger.VerifyLogging(LogLevel.Information);
        }

        [Fact]
        public async Task ProductUpdatedConsumer_NullProductName_ThrowsArgumentNullException()
        {
            //Arrange
            var context = GetContext("", reference, oldReference);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task ProductUpdatedConsumer_NullReference_ThrowsArgumentNullException()
        {
            //Arrange
            var context = GetContext(productName, "", oldReference);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task ProductUpdatedConsumer_NullOldReference_ThrowsArgumentNullException()
        {
            //Arrange
            var context = GetContext(productName, reference, "");

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task ProductUpdatedConsumer_ThrowsNullDataException()
        {
            //Arrange
            var context = GetContext(productName, reference, oldReference);

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>())).ThrowsAsync(new NullDataException());

            //Assert
            await Assert.ThrowsAsync<NullDataException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        [Fact]
        public async Task ProductUpdatedConsumer_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            var context = GetContext(productName, reference, oldReference);

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>())).Returns(Task.FromResult(product));
            productRepository.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(Product)));

            //Assert
            await Assert.ThrowsAsync<ChangesNotSavedCorrectlyException>(() => consumer.Consume(context));
            logger.VerifyLogging(LogLevel.Error);
        }

        private ConsumeContext<ProductUpdatedEvent> GetContext(string productName, string reference, string oldReference)
        {
            var productUpdatedEvent
                = new ProductUpdatedEvent { ProductName = productName, Reference = reference, OldReference = oldReference };

            return Mock.Of<ConsumeContext<ProductUpdatedEvent>>(x => x.Message == productUpdatedEvent);
        }
    }
}
