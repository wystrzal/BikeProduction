using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Messaging.Consumers;
using Warehouse.Core.Exceptions;
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
        public async Task ProductDeletedConsumer_ThrowsProductNotFoundException()
        {
            //Arrange
            var productDeletedEvent = new ProductDeletedEvent(It.IsAny<string>());
            var context = Mock.Of<ConsumeContext<ProductDeletedEvent>>(x => x.Message == productDeletedEvent);

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>()))
                .Returns(Task.FromResult((Product)null));

            var consumer = new ProductDeletedConsumer(productRepository.Object, logger.Object);

            //Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => consumer.Consume(context));        
        }

        [Fact]
        public async Task ProductDeletedConsumer_Success()
        {
            //Arrange
            var product = new Product();
            var productDeletedEvent = new ProductDeletedEvent(It.IsAny<string>());
            var context = Mock.Of<ConsumeContext<ProductDeletedEvent>>(x => x.Message == productDeletedEvent);

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>()))
                .Returns(Task.FromResult(product));
            productRepository.Setup(x => x.Delete(product)).Verifiable();
            productRepository.Setup(x => x.SaveAllAsync()).Verifiable();

            var consumer = new ProductDeletedConsumer(productRepository.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            productRepository.Verify(x => x.Delete(product), Times.Once);
            productRepository.Verify(x => x.SaveAllAsync(), Times.Once);
        }
    }
}
