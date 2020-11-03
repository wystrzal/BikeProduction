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

        private readonly GetPartsQuery query;
        private readonly GetPartsQueryHandler queryHandler;
        private readonly List<Part> parts;
        private readonly List<GetPartsDto> partsDto;

        public GetPartsQueryHandlerTest()
        {
            partRepository = new Mock<IPartRepository>();
            mapper = new Mock<IMapper>();
            query = new GetPartsQuery();
            queryHandler = new GetPartsQueryHandler(partRepository.Object, mapper.Object);
            parts = new List<Part> { new Part(), new Part() };
            partsDto = new List<GetPartsDto> { new GetPartsDto(), new GetPartsDto() };
        }

        [Fact]
        public async Task GetPartsQueryHandler_Success()
        {
            //Arrange
            partRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(parts));
            mapper.Setup(x => x.Map<IEnumerable<GetPartsDto>>(parts)).Returns(partsDto);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(partsDto.Count, action.Count());
        }
    }
}
