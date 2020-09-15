using Catalog.Application.Commands;
using Catalog.Application.Commands.Handlers;
using Catalog.Core.Exceptions;
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
    public class DeleteProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IBus> bus;

        public DeleteProductCommandHandlerTest()
        {
            productRepository = new Mock<IProductRepository>();
            bus = new Mock<IBus>();
        }

        [Fact]
        public async Task DeleteProductCommandHandler_Success()
        {
            //Arrange
            var id = 1;
            var command = new DeleteProductCommand(id);

            productRepository.Setup(x => x.GetById(id)).Returns(Task.FromResult(new Product()));
            productRepository.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            bus.Setup(x => x.Publish(It.IsAny<ProductDeletedEvent>(), It.IsAny<CancellationToken>())).Verifiable();

            var commandHandler = new DeleteProductCommandHandler(productRepository.Object, bus.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            bus.Verify(x => x.Publish(It.IsAny<ProductDeletedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProductCommandHandler_ThrowProductNotFoundException()
        {
            //Arrange
            var id = 1;
            var command = new DeleteProductCommand(id);

            productRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult((Product)null));

            var commandHandler = new DeleteProductCommandHandler(productRepository.Object, bus.Object);

            //Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
