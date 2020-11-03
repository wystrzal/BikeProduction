using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Application.Commands;
using Warehouse.Application.Commands.Handlers;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Xunit;

namespace Warehouse.Test.Commands
{
    public class AddProductPartCommandHandlerTest
    {
        private const int id = 1;
        private const string reference = "1";

        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IProductPartRepo> productPartRepo;

        private readonly AddProductPartCommand command;
        private readonly AddProductPartCommandHandler commandHandler;
        private readonly Product product;

        public AddProductPartCommandHandlerTest()
        {
            productRepository = new Mock<IProductRepository>();
            productPartRepo = new Mock<IProductPartRepo>();
            command = new AddProductPartCommand(id, reference);
            commandHandler = new AddProductPartCommandHandler(productRepository.Object, productPartRepo.Object);
            product = new Product { Id = id };
        }

        [Fact]
        public async Task AddProductPartCommandHandler_Success()
        {
            //Arrange
            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>())).Returns(Task.FromResult(product));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            productPartRepo.Verify(x => x.Add(It.IsAny<ProductsParts>()), Times.Once);
            productPartRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);
        }

        [Fact]
        public void AddProductPartCommandHandler_NullReference_ThrowsArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new AddProductPartCommand(id, null));
        }

        [Fact]
        public void AddProductPartCommandHandler_PartIdEqualZero_ThrowsArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new AddProductPartCommand(It.IsAny<int>(), reference));
        }
    }
}
