using AutoMapper;
using Catalog.Application.Mapping;
using Catalog.Application.Queries;
using Catalog.Application.Queries.Handlers;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Core.SearchSpecification;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test.Queries
{
    public class GetProductsQueryHandlerTest
    {
        private readonly Mock<IMapper> mapper;
        private readonly Mock<ISearchProductsService> searchProductService;

        private readonly GetProductsQuery query;
        private readonly GetProductsQueryHandler queryHandler;
        private readonly List<Product> products;
        private readonly List<GetProductsDto> productsDto;

        public GetProductsQueryHandlerTest()
        {
            mapper = new Mock<IMapper>();
            searchProductService = new Mock<ISearchProductsService>();
            query = new GetProductsQuery(new FilteringData());
            queryHandler = new GetProductsQueryHandler(mapper.Object, searchProductService.Object);
            products = new List<Product> { new Product(), new Product() };
            productsDto = new List<GetProductsDto> { new GetProductsDto(), new GetProductsDto() };
        }

        [Fact]
        public async Task GetProductsQueryHandler_Success()
        {
            //Arrange
            searchProductService.Setup(x => x.GetProducts(It.IsAny<FilteringData>())).Returns(Task.FromResult(products));

            mapper.Setup(x => x.Map<List<GetProductsDto>>(products)).Returns(productsDto);
            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(productsDto.Count, action.Count());
        }
    }
}
