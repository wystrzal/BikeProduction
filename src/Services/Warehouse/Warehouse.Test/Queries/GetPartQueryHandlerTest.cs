using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Application.Mapping;
using Warehouse.Application.Queries;
using Warehouse.Application.Queries.Handlers;
using Warehouse.Core.Exceptions;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Xunit;

namespace Warehouse.Test.Queries
{
    public class GetPartQueryHandlerTest
    {
        private readonly Mock<IPartRepository> partRepository;
        private readonly Mock<IMapper> mapper;

        public GetPartQueryHandlerTest()
        {
            partRepository = new Mock<IPartRepository>();
            mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetPartQueryHandler_ThrowsPartNotFoundException()
        {
            //Arrange
            var request = new GetPartQuery(It.IsAny<int>());

            partRepository.Setup(x => x.GetPart(It.IsAny<int>())).Returns(Task.FromResult((Part)null));

            var queryHandler = new GetPartQueryHandler(partRepository.Object, mapper.Object);

            //Assert
            await Assert.ThrowsAsync<PartNotFoundException>(()
                => queryHandler.Handle(request, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task GetPartQueryHandler_Success()
        {
            //Arrange
            var id = 1;
            var request = new GetPartQuery(id);
            var part = new Part { Id = id };
            var partDto = new GetPartDto { Id = id };

            partRepository.Setup(x => x.GetPart(id)).Returns(Task.FromResult(part));
            mapper.Setup(x => x.Map<GetPartDto>(part)).Returns(partDto);

            var queryHandler = new GetPartQueryHandler(partRepository.Object, mapper.Object);

            //Act
            var action = await queryHandler.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(id, action.Id);
        }
    }
}
