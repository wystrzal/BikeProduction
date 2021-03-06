﻿using AutoMapper;
using BaseRepository.Exceptions;
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
    public class UpdateProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IMapper> mapper;
        private readonly Mock<IBus> bus;

        private readonly UpdateProductCommand command;
        private readonly UpdateProductCommandHandler commandHandler;
        private readonly Product product;

        public UpdateProductCommandHandlerTest()
        {
            productRepository = new Mock<IProductRepository>();
            mapper = new Mock<IMapper>();
            bus = new Mock<IBus>();
            command = new UpdateProductCommand();
            commandHandler = new UpdateProductCommandHandler(productRepository.Object, mapper.Object, bus.Object);
            product = new Product();
        }

        [Fact]
        public async Task UpdateProductCommandHandler_ThrowsChangesNotSavedCorrectlyException()
        {
            //Arrange
            productRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(product));
            productRepository.Setup(x => x.SaveAllAsync()).ThrowsAsync(new ChangesNotSavedCorrectlyException(typeof(Product)));

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
            productRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(product));

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
            var product = new Product { Reference = "2" };
            var command = new UpdateProductCommand { Reference = "1" };

            productRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(product));

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
