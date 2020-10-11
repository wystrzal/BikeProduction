﻿using Common.Application.Commands;
using Common.Application.Messaging;
using MassTransit;
using MediatR;
using Moq;
using Production.Application.Commands;
using Production.Application.Commands.Handlers;
using Production.Core.Exceptions;
using Production.Core.Interfaces;
using Production.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Production.Core.Models.Enums.ProductionStatusEnum;

namespace Production.Test.Commands
{
    public class FinishProductionCommandHandlerTest
    {
        private readonly Mock<IProductionQueueRepo> productionQueueRepo;
        private readonly Mock<IBus> bus;

        public FinishProductionCommandHandlerTest()
        {
            productionQueueRepo = new Mock<IProductionQueueRepo>();
            bus = new Mock<IBus>();
        }

        [Fact]
        public async Task FinishProductionCommandHandler_ThrowsProductsNotBeingCreatedException()
        {
            //Arrange
            productionQueueRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult((ProductionQueue)null));

            var command = new FinishProductionCommand(It.IsAny<int>());

            var commandHandler = new FinishProductionCommandHandler(productionQueueRepo.Object, bus.Object);

            //Assert
            await Assert.ThrowsAsync<ProductsNotBeingCreatedException>(()
                => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task FinishProductionCommandHandler_CheckIfProductionFinishedCountNotEqualZero_Success()
        {
            //Arrange
            var productionQueue = new ProductionQueue { ProductionStatus = ProductionStatus.BeingCreated };
            var productionQueueList = new List<ProductionQueue> { new ProductionQueue() };

            productionQueueRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(productionQueue));

            productionQueueRepo.Setup(x => x.GetByConditionToList(It.IsAny<Func<ProductionQueue, bool>>()))
                .Returns(Task.FromResult(productionQueueList));

            var command = new FinishProductionCommand(It.IsAny<int>());

            var commandHandler = new FinishProductionCommandHandler(productionQueueRepo.Object, bus.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            productionQueueRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            bus.Verify(x => x.Publish(It.IsAny<ProductionFinishedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(Unit.Value, action);
        }

        [Fact]
        public async Task FinishProductionCommandHandler_CheckIfProductionFinishedCountEqualZero_Success()
        {
            //Arrange
            var productionQueue = new ProductionQueue { ProductionStatus = ProductionStatus.BeingCreated };
            var productionQueueList = new List<ProductionQueue>();

            productionQueueRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(productionQueue));

            productionQueueRepo.Setup(x => x.GetByConditionToList(It.IsAny<Func<ProductionQueue, bool>>()))
                .Returns(Task.FromResult(productionQueueList));

            var command = new FinishProductionCommand(It.IsAny<int>());

            var commandHandler = new FinishProductionCommandHandler(productionQueueRepo.Object, bus.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            productionQueueRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            bus.Verify(x => x.Publish(It.IsAny<ProductionFinishedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            bus.Verify(x => x.Publish(It.IsAny<PackReadyToSendEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(Unit.Value, action);
        }
    }
}
