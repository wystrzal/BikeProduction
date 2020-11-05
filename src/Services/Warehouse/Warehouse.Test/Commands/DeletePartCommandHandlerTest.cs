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
    public class DeletePartCommandHandlerTest
    {
        private const int id = 1;

        private readonly Mock<IPartRepository> partRepository;

        private readonly DeletePartCommand command;
        private readonly DeletePartCommandHandler commandHandler;
        private readonly Part part;

        public DeletePartCommandHandlerTest()
        {
            partRepository = new Mock<IPartRepository>();
            command = new DeletePartCommand(id);
            commandHandler = new DeletePartCommandHandler(partRepository.Object);
            part = new Part();
        }

        [Fact]
        public async Task DeletePartCommandHandler_Success()
        {
            //Arrange
            partRepository.Setup(x => x.GetById(id)).Returns(Task.FromResult(part));

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            partRepository.Verify(x => x.Delete(part), Times.Once);
            partRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);
        }
    }
}
