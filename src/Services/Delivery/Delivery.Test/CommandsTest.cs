using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
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
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Delivery.Application.Messaging.MessagingModels.OrderStatusEnum;

namespace Delivery.Test
{
    public class CommandsTest
    {
        private readonly Mock<ILoadingPlaceRepo> loadingPlaceRepo;
        private readonly Mock<IPackToDeliveryRepo> packToDeliveryRepo;
        private readonly Mock<IBus> bus;

        public CommandsTest()
        {
            loadingPlaceRepo = new Mock<ILoadingPlaceRepo>();
            packToDeliveryRepo = new Mock<IPackToDeliveryRepo>();
            bus = new Mock<IBus>();
        }

        [Fact]
        public async Task CompleteDeliveryCommandHandler_Success()
        {
            //Arrange
            var id = 1;
            var packsToDelivery = new List<PackToDelivery> { new PackToDelivery(), new PackToDelivery() };
            var loadingPlace = new LoadingPlace { Id = id, PacksToDelivery = packsToDelivery };
            var command = new CompleteDeliveryCommand(id);

            loadingPlaceRepo.Setup(x => x.GetByConditionWithIncludeFirst(It.IsAny<Func<LoadingPlace, bool>>(),
                It.IsAny<Expression<Func<LoadingPlace, ICollection<PackToDelivery>>>>())).Returns(Task.FromResult(loadingPlace));

            bus.Setup(x => x.Publish(It.IsAny<ChangeOrderStatusEvent>(), It.IsAny<CancellationToken>())).Verifiable();

            loadingPlaceRepo.Setup(x => x.SaveAllAsync()).Verifiable();

            var commandHandler = new CompleteDeliveryCommandHandler(loadingPlaceRepo.Object, bus.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            bus.Verify(x => x.Publish(It.IsAny<ChangeOrderStatusEvent>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            loadingPlaceRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);
        }

        [Fact]
        public async Task StartDeliveryCommandHandler_Success()
        {
            //Arrange
            var id = 1;
            var packsToDelivery = new List<PackToDelivery> { new PackToDelivery(), new PackToDelivery() };
            var loadingPlace = new LoadingPlace { Id = id, PacksToDelivery = packsToDelivery };
            var command = new StartDeliveryCommand(id);

            loadingPlaceRepo.Setup(x => x.GetByConditionWithIncludeFirst(It.IsAny<Func<LoadingPlace, bool>>(),
                It.IsAny<Expression<Func<LoadingPlace, ICollection<PackToDelivery>>>>())).Returns(Task.FromResult(loadingPlace));

            bus.Setup(x => x.Publish(It.IsAny<ChangeOrderStatusEvent>(), It.IsAny<CancellationToken>())).Verifiable();

            loadingPlaceRepo.Setup(x => x.SaveAllAsync()).Verifiable();

            var commandHandler = new StartDeliveryCommandHandler(loadingPlaceRepo.Object, bus.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            bus.Verify(x => x.Publish(It.IsAny<ChangeOrderStatusEvent>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            loadingPlaceRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);
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
