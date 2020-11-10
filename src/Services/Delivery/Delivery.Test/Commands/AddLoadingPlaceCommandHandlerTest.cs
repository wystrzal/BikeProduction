using AutoMapper;
using Delivery.Application.Commands;
using Delivery.Application.Commands.Handlers;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Delivery.Core.Models.Enums.LoadingPlaceStatusEnum;

namespace Delivery.Test.Commands
{
    public class AddLoadingPlaceCommandHandlerTest
    {
        private readonly Mock<ILoadingPlaceRepo> loadingPlaceRepo;
        private readonly Mock<IMapper> mapper;

        private readonly AddLoadingPlaceCommand command;
        private readonly AddLoadingPlaceCommandHandler commandHandler;
        private readonly LoadingPlace loadingPlace;

        public AddLoadingPlaceCommandHandlerTest()
        {
            loadingPlaceRepo = new Mock<ILoadingPlaceRepo>();
            mapper = new Mock<IMapper>();
            command = new AddLoadingPlaceCommand();
            commandHandler = new AddLoadingPlaceCommandHandler(loadingPlaceRepo.Object, mapper.Object);
            loadingPlace = new LoadingPlace();
        }

        [Fact]
        public async Task AddLoadingPlaceCommandHandler_Success()
        {
            //Arrange
            mapper.Setup(x => x.Map<LoadingPlace>(command)).Returns(loadingPlace);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            Assert.Equal(LoadingPlaceStatus.Waiting_For_Loading, loadingPlace.LoadingPlaceStatus);
            loadingPlaceRepo.Verify(x => x.Add(loadingPlace), Times.Once);
            loadingPlaceRepo.Verify(x => x.SaveAllAsync(), Times.Once);
        }
    }
}
