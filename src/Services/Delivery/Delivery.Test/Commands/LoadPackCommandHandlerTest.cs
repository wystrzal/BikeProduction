using Common.Application.Messaging;
using Delivery.Application.Commands;
using Delivery.Application.Commands.Handlers;
using Delivery.Core.Exceptions;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using MassTransit;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Commands
{
    public class LoadPackCommandHandlerTest
    {
        private const int id = 1;

        private readonly Mock<ILoadingPlaceRepo> loadingPlaceRepo;
        private readonly Mock<IPackToDeliveryRepo> packToDeliveryRepo;
        private readonly Mock<IBus> bus;

        private readonly LoadPackCommand command;
        private readonly LoadPackCommandHandler commandHandler;

        public LoadPackCommandHandlerTest()
        {
            loadingPlaceRepo = new Mock<ILoadingPlaceRepo>();
            packToDeliveryRepo = new Mock<IPackToDeliveryRepo>();
            bus = new Mock<IBus>();
            command = new LoadPackCommand(id, id);
            commandHandler = new LoadPackCommandHandler(packToDeliveryRepo.Object, loadingPlaceRepo.Object, bus.Object);
        }

        [Fact]
        public async Task LoadPackCommandHandler_ThrowsLackOfSpaceException()
        {
            //Arrange
            int productsQuantity = 100;
            int loadedQuantity = 50;
           
            var packToDelivery = new PackToDelivery { ProductsQuantity = productsQuantity };
            var loadingPlace = new LoadingPlace { LoadedQuantity = loadedQuantity, AmountOfSpace = productsQuantity };

            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<PackToDelivery, bool>>()))
                .Returns(Task.FromResult(packToDelivery));

            loadingPlaceRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<LoadingPlace, bool>>()))
                .Returns(Task.FromResult(loadingPlace));

            //Assert
            await Assert.ThrowsAsync<LackOfSpaceException>(() => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task LoadPackCommandHandler_Success()
        {
            //Arrange
            int productsQuantity = 50;
            int loadedQuantity = 0;

            var packToDelivery = new PackToDelivery { ProductsQuantity = productsQuantity };
            var loadingPlace = new LoadingPlace { LoadedQuantity = loadedQuantity, AmountOfSpace = productsQuantity };

            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<PackToDelivery, bool>>()))
                .Returns(Task.FromResult(packToDelivery));

            loadingPlaceRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<LoadingPlace, bool>>()))
                .Returns(Task.FromResult(loadingPlace));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            loadingPlaceRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            bus.Verify(x => x.Publish(It.IsAny<ChangeOrderStatusEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void LoadPackCommandHandler_LoadingPlaceId_ThrowsArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new LoadPackCommand(It.IsAny<int>(), id));
        }

        [Fact]
        public void LoadPackCommandHandler_PackId_ThrowsArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new LoadPackCommand(id, It.IsAny<int>()));
        }
    }
}
