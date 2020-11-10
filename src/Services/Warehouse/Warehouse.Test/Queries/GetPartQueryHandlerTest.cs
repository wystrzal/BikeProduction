using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    public class GetPartQueryHandlerTest
    {
        private const int id = 1;

        private readonly Mock<IPartRepository> partRepository;
        private readonly Mock<IMapper> mapper;

        private readonly GetPartQuery query;
        private readonly GetPartQueryHandler queryHandler;
        private readonly Part part;
        private readonly GetPartDto partDto;

        public GetPartQueryHandlerTest()
        {
            partRepository = new Mock<IPartRepository>();
            mapper = new Mock<IMapper>();
            query = new GetPartQuery(id);
            queryHandler = new GetPartQueryHandler(partRepository.Object, mapper.Object);
            part = new Part { Id = id };
            partDto = new GetPartDto { Id = id };
        }

        [Fact]
        public async Task GetPartQueryHandler_Success()
        {
            //Arrange
            partRepository.Setup(x
                => x.GetByConditionWithIncludeFirst(It.IsAny<Func<Part, bool>>(), It.IsAny<Expression<Func<Part, ICollection<ProductsParts>>>>()))
                .Returns(Task.FromResult(part));

            mapper.Setup(x => x.Map<GetPartDto>(part)).Returns(partDto);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(id, action.Id);
        }
    }
}
