using AutoMapper;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Application.Commands;
using Warehouse.Application.Commands.Handlers;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Xunit;

namespace Warehouse.Test.Commands
{
    public class UpdatePartCommandHandlerTest
    {
        private const int partId = 1;

        private readonly Mock<IPartRepository> partRepository;
        private readonly Mock<IMapper> mapper;

        private readonly UpdatePartCommand command;
        private readonly UpdatePartCommandHandler commandHandler;
        private readonly Part part;

        public UpdatePartCommandHandlerTest()
        {
            partRepository = new Mock<IPartRepository>();
            mapper = new Mock<IMapper>();
            command = new UpdatePartCommand { Id = partId };
            commandHandler = new UpdatePartCommandHandler(partRepository.Object, mapper.Object);
            part = new Part { Id = partId };
        }

        [Fact]
        public async Task UpdatePartCommandHandler_Success()
        {
            //Arrange
            partRepository.Setup(x => x.GetById(partId)).Returns(Task.FromResult(part));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(Unit.Value, action);
            partRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            mapper.Verify(x => x.Map(command, part), Times.Once);
        }

    }
}
