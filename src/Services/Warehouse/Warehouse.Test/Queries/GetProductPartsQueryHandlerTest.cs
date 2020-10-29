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
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Xunit;

namespace Warehouse.Test.Queries
{
    public class GetProductPartsQueryHandlerTest
    {
        private readonly string reference = "1";

        private readonly Mock<IProductPartRepo> productPartRepo;
        private readonly Mock<IMapper> mapper;

        private readonly GetProductPartsQuery query;
        private readonly GetProductPartsQueryHandler queryHandler;
        private readonly List<Part> parts;
        private readonly List<GetProductPartsDto> productPartsDto;

        public GetProductPartsQueryHandlerTest()
        {
            productPartRepo = new Mock<IProductPartRepo>();
            mapper = new Mock<IMapper>();
            query = new GetProductPartsQuery(reference);
            queryHandler = new GetProductPartsQueryHandler(productPartRepo.Object, mapper.Object);
            parts = new List<Part> { new Part(), new Part() };
            productPartsDto = new List<GetProductPartsDto> { new GetProductPartsDto(), new GetProductPartsDto() };
        }

        [Fact]
        public async Task GetProductPartsQueryHandler_Success()
        {
            //Arrange
            productPartRepo.Setup(x => x.GetProductParts(reference)).Returns(Task.FromResult(parts));
            mapper.Setup(x => x.Map<List<GetProductPartsDto>>(parts)).Returns(productPartsDto);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(productPartsDto.Count, action.Count);
        }
    }
}
