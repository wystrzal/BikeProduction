using Delivery.Application.Commands;
using Delivery.Application.Commands.Handlers;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
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
    public class DeleteLoadingPlaceCommandHandlerTest
    {
        private const int loadingPlaceId = 1;

        private readonly Mock<ILoadingPlaceRepo> loadingPlaceRepo;

        private readonly DeleteLoadingPlaceCommand command;
        private readonly DeleteLoadingPlaceCommandHandler commandHandler;

        public DeleteLoadingPlaceCommandHandlerTest()
        {
            loadingPlaceRepo = new Mock<ILoadingPlaceRepo>();
            command = new DeleteLoadingPlaceCommand(loadingPlaceId);
            commandHandler = new DeleteLoadingPlaceCommandHandler(loadingPlaceRepo.Object);
        }

        [Fact]
        public async Task DeleteLoadingPlaceCommandHandler_Success()
        {
            //Arrange
            var loadingPlace = new LoadingPlace { PacksToDelivery = new List<PackToDelivery>() };

            loadingPlaceRepo.Setup(x => x.GetByConditionWithIncludeFirst(
                It.IsAny<Func<LoadingPlace, bool>>(), It.IsAny<Expression<Func<LoadingPlace, ICollection<PackToDelivery>>>>()))
                .Returns(Task.FromResult(loadingPlace));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            loadingPlaceRepo.Verify(x => x.Delete(loadingPlace), Times.Once);
            loadingPlaceRepo.Verify(x => x.SaveAllAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteLoadingPlaceCommandHandler_PacksCountGreaterThanZero_ThrowsArgumentException()
        {
            //Arrange
            var packToDeliveries = new List<PackToDelivery> { new PackToDelivery() };
            var loadingPlace = new LoadingPlace { PacksToDelivery = packToDeliveries };

            loadingPlaceRepo.Setup(x => x.GetByConditionWithIncludeFirst(
                It.IsAny<Func<LoadingPlace, bool>>(), It.IsAny<Expression<Func<LoadingPlace, ICollection<PackToDelivery>>>>()))
                .Returns(Task.FromResult(loadingPlace));

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void DeleteLoadingPlaceCommandHandler_LoadingPlaceId_ThrowsArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new DeleteLoadingPlaceCommand(It.IsAny<int>()));
        }
    }
}
