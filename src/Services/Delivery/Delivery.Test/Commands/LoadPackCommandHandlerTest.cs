﻿using Common.Application.Messaging;
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
        private readonly Mock<ILoadingPlaceRepo> loadingPlaceRepo;
        private readonly Mock<IPackToDeliveryRepo> packToDeliveryRepo;
        private readonly Mock<IBus> bus;

        public LoadPackCommandHandlerTest()
        {
            loadingPlaceRepo = new Mock<ILoadingPlaceRepo>();
            packToDeliveryRepo = new Mock<IPackToDeliveryRepo>();
            bus = new Mock<IBus>();
        }

        [Fact]
        public async Task LoadPackCommandHandler_ThrowsPackNotFoundException()
        {
            //Arrange
            var command = new LoadPackCommand(It.IsAny<int>(), It.IsAny<int>());

            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<PackToDelivery, bool>>()))
                .Returns(Task.FromResult((PackToDelivery)null));

            var commandHandler = new LoadPackCommandHandler(packToDeliveryRepo.Object, loadingPlaceRepo.Object, bus.Object);

            //Assert
            await Assert.ThrowsAsync<PackNotFoundException>(() => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task LoadPackCommandHandler_ThrowsLoadingPlaceNotFoundException()
        {
            //Arrange
            var command = new LoadPackCommand(It.IsAny<int>(), It.IsAny<int>());
            var packToDelivery = new PackToDelivery();

            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<PackToDelivery, bool>>()))
                .Returns(Task.FromResult(packToDelivery));

            loadingPlaceRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<LoadingPlace, bool>>()))
                .Returns(Task.FromResult((LoadingPlace)null));

            var commandHandler = new LoadPackCommandHandler(packToDeliveryRepo.Object, loadingPlaceRepo.Object, bus.Object);

            //Assert
            await Assert.ThrowsAsync<LoadingPlaceNotFoundException>(() => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task LoadPackCommandHandler_ThrowsLackOfSpaceException()
        {
            //Arrange
            var command = new LoadPackCommand(It.IsAny<int>(), It.IsAny<int>());
            var packToDelivery = new PackToDelivery { ProductsQuantity = 100 };
            var loadingPlace = new LoadingPlace { LoadedQuantity = 50, AmountOfSpace = 100 };

            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<PackToDelivery, bool>>()))
                .Returns(Task.FromResult(packToDelivery));

            loadingPlaceRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<LoadingPlace, bool>>()))
                .Returns(Task.FromResult(loadingPlace));

            var commandHandler = new LoadPackCommandHandler(packToDeliveryRepo.Object, loadingPlaceRepo.Object, bus.Object);

            //Assert
            await Assert.ThrowsAsync<LackOfSpaceException>(() => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task LoadPackCommandHandler_Success()
        {
            //Arrange
            var command = new LoadPackCommand(It.IsAny<int>(), It.IsAny<int>());
            var packToDelivery = new PackToDelivery { ProductsQuantity = 50 };
            var loadingPlace = new LoadingPlace { LoadedQuantity = 50, AmountOfSpace = 100 };

            packToDeliveryRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<PackToDelivery, bool>>()))
                .Returns(Task.FromResult(packToDelivery));

            loadingPlaceRepo.Setup(x => x.GetByConditionFirst(It.IsAny<Func<LoadingPlace, bool>>()))
                .Returns(Task.FromResult(loadingPlace));

            loadingPlaceRepo.Setup(x => x.SaveAllAsync()).Verifiable();

            bus.Setup(x => x.Publish(It.IsAny<ChangeOrderStatusEvent>(), It.IsAny<CancellationToken>())).Verifiable();

            var commandHandler = new LoadPackCommandHandler(packToDeliveryRepo.Object, loadingPlaceRepo.Object, bus.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            loadingPlaceRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            bus.Verify(x => x.Publish(It.IsAny<ChangeOrderStatusEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}