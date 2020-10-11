using AutoMapper;
using Catalog.Application.Mapping;
using Catalog.Application.Queries;
using Catalog.Application.Queries.Handlers;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Core.SearchSpecification;
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
    public class GetProductsQueryHandlerTest
    {
        private readonly Mock<IMapper> mapper;
        private readonly Mock<ISearchProductService> searchProductService;

        public GetProductsQueryHandlerTest()
        {
            mapper = new Mock<IMapper>();
            searchProductService = new Mock<ISearchProductService>();
        }

        [Fact]
        public async Task GetProductsQueryHandler_Success()
        {
            //Arrange
            var query = new GetProductsQuery(new FilteringData());

            var products = new List<Product> { new Product(), new Product() };
            var productsDto = new List<GetProductsDto> { new GetProductsDto(), new GetProductsDto() };

            searchProductService.Setup(x => x.GetProducts(It.IsAny<FilteringData>())).Returns(Task.FromResult(products));

            mapper.Setup(x => x.Map<List<GetProductsDto>>(products)).Returns(productsDto);

            var queryHandler = new GetProductsQueryHandler(mapper.Object, searchProductService.Object);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(productsDto.Count, action.Count());
        }
    }
}
