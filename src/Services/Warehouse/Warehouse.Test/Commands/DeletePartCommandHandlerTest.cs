﻿using MediatR;
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
    public class DeletePartCommandHandlerTest
    {
        private readonly Mock<IPartRepository> partRepository;

        public DeletePartCommandHandlerTest()
        {
            partRepository = new Mock<IPartRepository>();
        }

        [Fact]
        public async Task DeletePartCommandHandler_ThrowsPartNotFoundException()
        {
            //Arrange
            var command = new DeletePartCommand(It.IsAny<int>());

            partRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult((Part)null));

            var commandHandler = new DeletePartCommandHandler(partRepository.Object);

            //Assert
            await Assert.ThrowsAsync<PartNotFoundException>(() => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task DeletePartCommandHandler_Success()
        {
            //Arrange
            var part = new Part();
            var command = new DeletePartCommand(It.IsAny<int>());

            partRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(part));

            partRepository.Setup(x => x.Delete(part)).Verifiable();
            partRepository.Setup(x => x.SaveAllAsync()).Verifiable();

            var commandHandler = new DeletePartCommandHandler(partRepository.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            partRepository.Verify(x => x.Delete(part), Times.Once);
            partRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);
        }
    }
}