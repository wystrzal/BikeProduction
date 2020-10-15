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

        private readonly GetBrandsQuery query;
        private readonly GetBrandsQueryHandler queryHandler;
        private readonly List<Brand> brands;
        private readonly List<GetBrandsDto> brandsDto;

        public GetBrandsQueryHandlerTest()
        {
            mapper = new Mock<IMapper>();
            brandRepository = new Mock<IBrandRepository>();
            query = new GetBrandsQuery();
            queryHandler = new GetBrandsQueryHandler(brandRepository.Object, mapper.Object);
            brands = new List<Brand> { new Brand(), new Brand() };
            brandsDto = new List<GetBrandsDto> { new GetBrandsDto(), new GetBrandsDto() };
        }

        [Fact]
        public async Task GetBrandsQueryHandler_Success()
        {
            //Arrange         
            brandRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(brands));

            mapper.Setup(x => x.Map<List<GetBrandsDto>>(brands)).Returns(brandsDto);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(brandsDto.Count, action.Count());
        }
    }
}
