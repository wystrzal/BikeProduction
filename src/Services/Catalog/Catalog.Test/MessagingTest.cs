﻿using Castle.Core.Logging;
using Catalog.Application.Messaging.Consumers;
using Catalog.Application.Messaging.MessagingModels;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Common.Application.Messaging;
using MassTransit;
using MassTransit.NewIdProviders;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test
{
    public class MessagingTest
    {
        private readonly Mock<IProductRepository> productRepository;

        public MessagingTest()
        {
            productRepository = new Mock<IProductRepository>();
        }

        [Fact]
        public async Task OrderCreatedConsumer_Success()
        {
            //Arrange
            var logger = new Mock<ILogger<OrderCreatedConsumer>>();

            var orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    Id = 1,
                    Price = 1,
                    ProductName = "Test",
                    Quantity = 1,
                    Reference = "1"
                }
            };

            var orderCreatedEvent = new OrderCreatedEvent { OrderItems = orderItems };

            var context = Mock.Of<ConsumeContext<OrderCreatedEvent>>(x =>
                x.Message == orderCreatedEvent);

            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>()))
                .Returns(Task.FromResult(new Product())).Verifiable();

            productRepository.Setup(x => x.SaveAllAsync()).Verifiable();

            var consumer = new OrderCreatedConsumer(productRepository.Object, logger.Object);

            //Act
            await consumer.Consume(context);

            //Assert
            productRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            productRepository.Verify(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>()), Times.Once);
        }

        [Fact]
        public async Task OrderCanceledConsumer_Success()
        {
            //Arrange
            var logger = new Mock<ILogger<OrderCanceledConsumer>>();

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
