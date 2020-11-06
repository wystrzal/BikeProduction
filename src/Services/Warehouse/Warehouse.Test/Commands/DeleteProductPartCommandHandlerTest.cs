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
    public class DeleteProductPartCommandHandlerTest
    {
        private const int id = 1;
        private const string reference = "1";

        private readonly Mock<IProductPartRepo> productPartRepo;

        private readonly DeleteProductPartCommand command;
        private readonly DeleteProductPartCommandHandler commandHandler;
        private readonly ProductsParts productsParts;

        public DeleteProductPartCommandHandlerTest()
        {
            productPartRepo = new Mock<IProductPartRepo>();
            command = new DeleteProductPartCommand(id, reference);
            commandHandler = new DeleteProductPartCommandHandler(productPartRepo.Object);
            productsParts = new ProductsParts();
        }

        [Fact]
        public async Task DeleteProductPartCommandHandler_Success()
        {
            //Arrange
            productPartRepo.Setup(x => x.GetProductPart(reference, id)).Returns(Task.FromResult(productsParts));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            productPartRepo.Verify(x => x.Delete(productsParts), Times.Once);
            productPartRepo.Verify(x => x.SaveAllAsync(), Times.Once);
        }
    }
}
