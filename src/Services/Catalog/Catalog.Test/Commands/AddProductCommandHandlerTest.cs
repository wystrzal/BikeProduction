using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Commands.Handlers;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Common.Application.Messaging;
using MassTransit;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test.Commands
{
    public class AddProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IBus> bus;
        private readonly Mock<IMapper> mapper;

        public AddProductCommandHandlerTest()
        {
            productRepository = new Mock<IProductRepository>();
            bus = new Mock<IBus>();
            mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task AddProductCommandHandler_Success()
        {
            //Arrange
            var command = new AddProductCommand();
            var product = new Product();

            mapper.Setup(x => x.Map<Product>(command)).Returns(product);

            productRepository.Setup(x => x.Add(product));
            productRepository.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            bus.Setup(x => x.Publish(It.IsAny<ProductAddedEvent>(), It.IsAny<CancellationToken>())).Verifiable();

            var commandHandler = new AddProductCommandHandler(productRepository.Object, mapper.Object, bus.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            bus.Verify(x => x.Publish(It.IsAny<ProductAddedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
