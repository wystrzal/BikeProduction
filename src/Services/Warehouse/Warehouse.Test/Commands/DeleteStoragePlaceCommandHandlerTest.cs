using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Application.Commands;
using Warehouse.Application.Commands.Handlers;
using Warehouse.Core.Exceptions;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Xunit;

namespace Warehouse.Test.Commands
{
    public class DeleteStoragePlaceCommandHandlerTest
    {
        private readonly Mock<IStoragePlaceRepo> storagePlaceRepo;

        public DeleteStoragePlaceCommandHandlerTest()
        {
            storagePlaceRepo = new Mock<IStoragePlaceRepo>();
        }

        [Fact]
        public async Task DeleteStoragePlaceCommandHandler_ThrowsStoragePlaceNotFoundException()
        {
            //Arrange
            var command = new DeleteStoragePlaceCommand(It.IsAny<int>());

            storagePlaceRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult((StoragePlace)null));

            var commandHandler = new DeleteStoragePlaceCommandHandler(storagePlaceRepo.Object);

            //Assert
            await Assert.ThrowsAsync<StoragePlaceNotFoundException>(() 
                => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task DeleteStoragePlaceCommandHandler_Success()
        {
            //Arrange
            var command = new DeleteStoragePlaceCommand(It.IsAny<int>());
            var storagePlace = new StoragePlace();

            storagePlaceRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(storagePlace));

            storagePlaceRepo.Setup(x => x.Delete(storagePlace)).Verifiable();
            storagePlaceRepo.Setup(x => x.SaveAllAsync()).Verifiable();

            var commandHandler = new DeleteStoragePlaceCommandHandler(storagePlaceRepo.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            storagePlaceRepo.Verify(x => x.Delete(storagePlace), Times.Once);
            storagePlaceRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);

        }
    }
}
