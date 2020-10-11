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
    public class StartCreatingProductsCommandHandlerTest
    {
        private readonly Mock<IProductionQueueRepo> productionQueueRepo;

        public StartCreatingProductsCommandHandlerTest()
        {
            productionQueueRepo = new Mock<IProductionQueueRepo>();
        }

        [Fact]
        public async Task StartCreatingProductsCommandHandler_ThrowsProductionQueueNotConfirmedException()
        {
            //Arrange
            var command = new StartCreatingProductsCommand(It.IsAny<int>());

            productionQueueRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult((ProductionQueue)null));

            var commandHandler = new StartCreatingProductsCommandHandler(productionQueueRepo.Object);

            //Assert
            await Assert.ThrowsAsync<ProductionQueueNotConfirmedException>(()
                => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task StartCreatingProductsCommandHandler_Success()
        {
            //Arrange
            var productionQueue = new ProductionQueue { ProductionStatus = ProductionStatus.Confirmed };
            var command = new StartCreatingProductsCommand(It.IsAny<int>());

            productionQueueRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(Task.FromResult(productionQueue));

            var commandHandler = new StartCreatingProductsCommandHandler(productionQueueRepo.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            productionQueueRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);
        }
    }
}
