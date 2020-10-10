using AutoMapper;
using BikeBaseRepository;
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
    public class UpdateProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IMapper> mapper;
        private readonly Mock<IBus> bus;

        public UpdateProductCommandHandlerTest()
        {
            productRepository = new Mock<IProductRepository>();
            mapper = new Mock<IMapper>();
            bus = new Mock<IBus>();
        }

        [Fact]
        public async Task UpdateProductCommandHandler_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            var product = new Product { Id = 1, Reference = "2", ProductName = "test" };
            var command = new UpdateProductCommand { Id = 1, Reference = "1", ProductName = "test" };

            productRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(product));
            mapper.Setup(x => x.Map(command, product)).Verifiable();
            productRepository.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(Product)));

            var commandHandler = new UpdateProductCommandHandler(productRepository.Object, mapper.Object, bus.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            mapper.Verify(x => x.Map(command, product), Times.Once);
        }

        [Fact]
        public async Task UpdateProductCommandHandler_Success()
        {
            //Arrange
            var product = new Product { Id = 1, Reference = "1", ProductName = "test" };
            var command = new UpdateProductCommand { Id = 1, Reference = "1", ProductName = "test" };

            productRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(product));
            mapper.Setup(x => x.Map(command, product)).Verifiable();
            productRepository.Setup(x => x.SaveAllAsync()).Verifiable();
            bus.Setup(x => x.Publish(It.IsAny<ProductUpdatedEvent>(), It.IsAny<CancellationToken>())).Verifiable();

            var commandHandler = new UpdateProductCommandHandler(productRepository.Object, mapper.Object, bus.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            mapper.Verify(x => x.Map(command, product), Times.Once);
            productRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            bus.Verify(x => x.Publish(It.IsAny<ProductUpdatedEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateProductCommandHandler_PublishEvent_Success()
        {
            //Arrange
            var product = new Product { Id = 1, Reference = "2", ProductName = "test" };
            var command = new UpdateProductCommand { Id = 1, Reference = "1", ProductName = "test" };

            productRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(product));
            mapper.Setup(x => x.Map(command, product)).Verifiable();
            productRepository.Setup(x => x.SaveAllAsync()).Verifiable();
            bus.Setup(x => x.Publish(It.IsAny<ProductUpdatedEvent>(), It.IsAny<CancellationToken>())).Verifiable();

            var commandHandler = new UpdateProductCommandHandler(productRepository.Object, mapper.Object, bus.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            mapper.Verify(x => x.Map(command, product), Times.Once);
            productRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            bus.Verify(x => x.Publish(It.IsAny<ProductUpdatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
