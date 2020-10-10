﻿using Castle.Core.Logging;
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
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<ILogger<ProductUpdatedConsumer>> logger;

        public ProductUpdatedConsumerTest()
        {
            productRepository = new Mock<IProductRepository>();
            logger = new Mock<ILogger<ProductUpdatedConsumer>>();
        }

        [Fact]
        public async Task ProductUpdatedConsumer_Success()
        {
            //Arrange
            var product = new Product();
            var productUpdatedEvent = new ProductUpdatedEvent();
            var context = Mock.Of<ConsumeContext<ProductUpdatedEvent>>(x => x.Message == productUpdatedEvent);

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>())).Returns(Task.FromResult(product));
            productRepository.Setup(x => x.SaveAllAsync()).Verifiable();

            var consumer = new ProductUpdatedConsumer(productRepository.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            productRepository.Verify(x => x.SaveAllAsync(), Times.Once);
        }
    }
}
