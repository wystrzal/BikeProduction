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
using System.Linq.Expressions;
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

        private readonly AddProductCommand command;
        private readonly AddProductCommandHandler commandHandler;
        private readonly Product product;

        public AddProductCommandHandlerTest()
        {
            productRepository = new Mock<IProductRepository>();
            bus = new Mock<IBus>();
            mapper = new Mock<IMapper>();
            command = new AddProductCommand();
            commandHandler = new AddProductCommandHandler(productRepository.Object, mapper.Object, bus.Object);
            product = new Product();
        }

        [Fact]
        public async Task AddProductCommandHandler_Success()
        {
            //Arrange
            mapper.Setup(x => x.Map<Product>(command)).Returns(product);

            productRepository.Setup(x => x.Add(product));
            productRepository.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            productRepository.Verify(x => x.CheckIfExistByCondition(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            bus.Verify(x => x.Publish(It.IsAny<ProductAddedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
