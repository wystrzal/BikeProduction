using AutoMapper;
using MediatR;
using Microsoft.Extensions.Primitives;
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
    public class AddStoragePlaceCommandHandlerTest
    {
        private readonly Mock<IStoragePlaceRepo> storagePlaceRepo;
        private readonly Mock<IMapper> mapper;

        public AddStoragePlaceCommandHandlerTest()
        {
            storagePlaceRepo = new Mock<IStoragePlaceRepo>();
            mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task AddStoragePlaceCommandHandler_Success()
        {
            //Arrange
            var storagePlae = new StoragePlace();
            var command = new AddStoragePlaceCommand();

            mapper.Setup(x => x.Map<StoragePlace>(command)).Returns(storagePlae);

            var commandHandler = new AddStoragePlaceCommandHandler(storagePlaceRepo.Object, mapper.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            storagePlaceRepo.Verify(x => x.Add(storagePlae), Times.Once);
            storagePlaceRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);
        }
    }
}
