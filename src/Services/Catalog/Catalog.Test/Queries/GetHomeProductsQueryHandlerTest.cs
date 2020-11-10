using AutoMapper;
using Catalog.Application.Mapping;
using Catalog.Application.Queries;
using Catalog.Application.Queries.Handlers;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Catalog.Core.Models.Enums.HomeProductEnum;

namespace Catalog.Test.Queries
{
    public class GetHomeProductsQueryHandlerTest
    {
        private const HomeProduct homeProduct = HomeProduct.NewProduct;

        private readonly Mock<IMapper> mapper;
        private readonly Mock<IProductRepository> productRepository;

        private readonly GetHomeProductsQuery query;
        private readonly GetHomeProductsQueryHandler queryHandler;
        private readonly List<Product> products;
        private readonly List<GetHomeProductsDto> homeProductsDto;

        public GetHomeProductsQueryHandlerTest()
        {
            mapper = new Mock<IMapper>();
            productRepository = new Mock<IProductRepository>();
            query = new GetHomeProductsQuery(homeProduct);
            queryHandler = new GetHomeProductsQueryHandler(productRepository.Object, mapper.Object);
            products = new List<Product>() { new Product(), new Product() };
            homeProductsDto = new List<GetHomeProductsDto> { new GetHomeProductsDto(), new GetHomeProductsDto() };
        }

        [Fact]
        public async Task GetHomeProductsQueryHandler_Success()
        {
            //Arrange
            productRepository.Setup(x => x.GetHomePageProducts(homeProduct)).Returns(Task.FromResult(products));

            mapper.Setup(x => x.Map<List<GetHomeProductsDto>>(products)).Returns(homeProductsDto);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(homeProductsDto.Count, action.Count());
        }
    }
}
