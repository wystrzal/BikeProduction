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
    public class AddPartCommandHandlerTest
    {
        private readonly Mock<IPartRepository> partRepository;
        private readonly Mock<IMapper> mapper;

        private readonly AddPartCommand command;
        private readonly AddPartCommandHandler commandHandler;
        private readonly Part part;

        public AddPartCommandHandlerTest()
        {
            partRepository = new Mock<IPartRepository>();
            mapper = new Mock<IMapper>();
            command = new AddPartCommand();
            commandHandler = new AddPartCommandHandler(partRepository.Object, mapper.Object);
            part = new Part();
        }

        [Fact]
        public async Task AddPartCommandHandler_Success()
        {
            //Arrange
            mapper.Setup(x => x.Map<Part>(command)).Returns(part);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            partRepository.Verify(x => x.Add(part), Times.Once);
            partRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);
        }
    }
}
