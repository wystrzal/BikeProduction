using Common.Application.Messaging;
using Delivery.Application.Commands;
using Delivery.Application.Commands.Handlers;
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

namespace Delivery.Test.Commands
{
    public class StartDeliveryCommandHandlerTest
    {
        private readonly Mock<ILoadingPlaceRepo> loadingPlaceRepo;
        private readonly Mock<IBus> bus;

        private readonly StartDeliveryCommand command;
        private readonly StartDeliveryCommandHandler commandHandler;
        private readonly List<PackToDelivery> packsToDelivery;
        private readonly LoadingPlace loadingPlace;

        public StartDeliveryCommandHandlerTest()
        {
            loadingPlaceRepo = new Mock<ILoadingPlaceRepo>();
            bus = new Mock<IBus>();
            command = new StartDeliveryCommand(It.IsAny<int>());
            commandHandler = new StartDeliveryCommandHandler(loadingPlaceRepo.Object, bus.Object);
            packsToDelivery = new List<PackToDelivery> { new PackToDelivery(), new PackToDelivery() };
            loadingPlace = new LoadingPlace { PacksToDelivery = packsToDelivery };
        }

        [Fact]
        public async Task StartDeliveryCommandHandler_Success()
        {
            //Arrange            
            loadingPlaceRepo.Setup(x => x.GetByConditionWithIncludeFirst(It.IsAny<Func<LoadingPlace, bool>>(),
                It.IsAny<Expression<Func<LoadingPlace, ICollection<PackToDelivery>>>>())).Returns(Task.FromResult(loadingPlace));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            bus.Verify(x => x.Publish(It.IsAny<ChangeOrderStatusEvent>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            loadingPlaceRepo.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);
        }
    }
}
