using AutoMapper;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
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

        public AddPartCommandHandlerTest()
        {
            partRepository = new Mock<IPartRepository>();
            mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task AddPartCommandHandler_Success()
        {
            //Arrange
            var part = new Part();
            var command = new AddPartCommand();

            mapper.Setup(x => x.Map<Part>(command)).Returns(part);

            partRepository.Setup(x => x.Add(part)).Verifiable();
            partRepository.Setup(x => x.SaveAllAsync()).Verifiable();

            var commandHandler = new AddPartCommandHandler(partRepository.Object, mapper.Object);

            //Act
            var action = await commandHandler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            partRepository.Verify(x => x.Add(part), Times.Once);
            partRepository.Verify(x => x.SaveAllAsync(), Times.Once);
            Assert.Equal(Unit.Value, action);
        }
    }
}
