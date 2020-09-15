using AutoMapper;
using Catalog.Application.Mapping;
using Catalog.Application.Queries;
using Catalog.Application.Queries.Handlers;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
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

            var brands = new List<Brand> { new Brand { Id = 1 }, new Brand { Id = 2 } };

            var brandsDto = new List<GetBrandsDto> { new GetBrandsDto { Id = 1 }, new GetBrandsDto { Id = 2 } };

            brandRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(brands));

            mapper.Setup(x => x.Map<List<GetBrandsDto>>(brands)).Returns(brandsDto);

            var queryHandler = new GetBrandsQueryHandler(brandRepository.Object, mapper.Object);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(2, action.Count());
            Assert.Equal(1, action.Select(x => x.Id).First());
        }
    }
}
