using AutoMapper;
using Catalog.Application.Mapping;
using Catalog.Application.Queries;
using Catalog.Application.Queries.Handlers;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test.Queries
{
    public class GetBrandsQueryHandlerTest
    {
        private readonly Mock<IMapper> mapper;
        private readonly Mock<IBrandRepository> brandRepository;

        public GetBrandsQueryHandlerTest()
        {
            mapper = new Mock<IMapper>();
            brandRepository = new Mock<IBrandRepository>();
        }

        [Fact]
        public async Task GetBrandsQueryHandler_Success()
        {
            //Arrange
            var query = new GetBrandsQuery();

            var brands = new List<Brand> { new Brand(), new Brand() };
            var brandsDto = new List<GetBrandsDto> { new GetBrandsDto(), new GetBrandsDto() };

            brandRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(brands));

            mapper.Setup(x => x.Map<List<GetBrandsDto>>(brands)).Returns(brandsDto);

            var queryHandler = new GetBrandsQueryHandler(brandRepository.Object, mapper.Object);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(brandsDto.Count, action.Count());
        }
    }
}
