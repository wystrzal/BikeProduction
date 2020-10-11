using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Application.Mapping;
using Warehouse.Application.Queries;
using Warehouse.Application.Queries.Handlers;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Xunit;

namespace Warehouse.Test.Queries
{
    public class GetPartsQueryHandlerTest
    {
        private readonly Mock<IPartRepository> partRepository;
        private readonly Mock<IMapper> mapper;

        public GetPartsQueryHandlerTest()
        {
            partRepository = new Mock<IPartRepository>();
            mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetPartsQueryHandler_Success()
        {
            //Arrange
            var parts = new List<Part> { new Part(), new Part() };
            var partsDto = new List<GetPartsDto> { new GetPartsDto(), new GetPartsDto() };

            partRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(parts));
            mapper.Setup(x => x.Map<IEnumerable<GetPartsDto>>(parts)).Returns(partsDto);

            var queryHandler = new GetPartsQueryHandler(partRepository.Object, mapper.Object);

            //Act
            var action = await queryHandler.Handle(It.IsAny<GetPartsQuery>(), It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(partsDto.Count, action.Count());
        }
    }
}
