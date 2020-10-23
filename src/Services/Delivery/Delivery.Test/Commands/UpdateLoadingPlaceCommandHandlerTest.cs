using AutoMapper;
using Delivery.Application.Commands;
using Delivery.Application.Commands.Handlers;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
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
    public class UpdateLoadingPlaceCommandHandlerTest
    {
        private const int loadingPlaceId = 1;
        private const int loadedQuantity = 10;
        private const int lowerAmountOfSpace = 0;
        private const int greaterAmountOfSpace = 50;

        private readonly Mock<ILoadingPlaceRepo> loadingPlaceRepo;
        private readonly Mock<IMapper> mapper;

        private readonly UpdateLoadingPlaceCommandHandler commandHandler;
        private readonly LoadingPlace loadingPlace;

        public UpdateLoadingPlaceCommandHandlerTest()
        {
            loadingPlaceRepo = new Mock<ILoadingPlaceRepo>();
            mapper = new Mock<IMapper>();
            commandHandler = new UpdateLoadingPlaceCommandHandler(loadingPlaceRepo.Object, mapper.Object);
            loadingPlace = new LoadingPlace { LoadedQuantity = loadedQuantity };
        }

        [Fact]
        public async Task UpdateLoadingPlaceCommandHandler_Success()
        {
            //Arrange
            var command = new UpdateLoadingPlaceCommand { Id = loadingPlaceId, AmountOfSpace = greaterAmountOfSpace };

            loadingPlaceRepo.Setup(x => x.GetById(loadingPlaceId)).Returns(Task.FromResult(loadingPlace));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            mapper.Verify(x => x.Map(command, loadingPlace), Times.Once);
            loadingPlaceRepo.Verify(x => x.SaveAllAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateLoadingPlaceCommandHandler_AmountOfSpaceLowerThanLoadedQuantity_ThrowsArgumentException()
        {
            //Arrange
            var command = new UpdateLoadingPlaceCommand { Id = loadingPlaceId, AmountOfSpace = lowerAmountOfSpace };

            loadingPlaceRepo.Setup(x => x.GetById(loadingPlaceId)).Returns(Task.FromResult(loadingPlace));

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
