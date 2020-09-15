using Catalog.Application.Messaging.Consumers;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test.Messaging
{
    public class OrderCanceledConsumerTest
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<ILogger<OrderCanceledConsumer>> logger;

        public OrderCanceledConsumerTest()
        {
            productRepository = new Mock<IProductRepository>();
            logger = new Mock<ILogger<OrderCanceledConsumer>>();
        }

        [Fact]
        public async Task OrderCanceledConsumer_Success()
        {
            //Arrange
            var references = new List<string> { "1" };

            var orderCanceledEvent = new OrderCanceledEvent { References = references };

            var context = Mock.Of<ConsumeContext<OrderCanceledEvent>>(x =>
                x.Message == orderCanceledEvent);

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>()))
                .Returns(Task.FromResult(new Product())).Verifiable();

            productRepository.Setup(x => x.SaveAllAsync()).Verifiable();

            var consumer = new OrderCanceledConsumer(productRepository.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            productRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            productRepository.Verify(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>()), Times.Once);
        }
    }
}
