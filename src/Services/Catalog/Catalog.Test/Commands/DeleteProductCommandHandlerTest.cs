using Catalog.Application.Commands;
using Catalog.Application.Commands.Handlers;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Common.Application.Messaging;
using MassTransit;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test.Commands
{
    public class DeleteProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IBus> bus;

        private readonly DeleteProductCommand command;
        private readonly DeleteProductCommandHandler commandHandler;
        private readonly Product product;

        public DeleteProductCommandHandlerTest()
        {
            productRepository = new Mock<IProductRepository>();
            bus = new Mock<IBus>();
            command = new DeleteProductCommand(It.IsAny<int>());
            product = new Product();
            commandHandler = new DeleteProductCommandHandler(productRepository.Object, bus.Object);
        }

        [Fact]
        public async Task DeleteProductCommandHandler_Success()
        {
            //Arrange
            productRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(product));
            productRepository.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            productRepository.Verify(x => x.Delete(product), Times.Once);
            bus.Verify(x => x.Publish(It.IsAny<ProductDeletedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
