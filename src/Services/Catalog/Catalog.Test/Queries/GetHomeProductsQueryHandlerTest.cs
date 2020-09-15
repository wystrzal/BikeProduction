using AutoMapper;
using Catalog.Application.Mapping;
using Catalog.Application.Queries;
using Catalog.Application.Queries.Handlers;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Core.Models.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Catalog.Core.Models.Enums.HomeProductEnum;

namespace Catalog.Test.Queries
{
    public class GetHomeProductsQueryHandlerTest
    {
        private readonly Mock<IMapper> mapper;
        private readonly Mock<IProductRepository> productRepository;

        public GetHomeProductsQueryHandlerTest()
        {
            mapper = new Mock<IMapper>();
            productRepository = new Mock<IProductRepository>();
        }

        [Fact]
        public async Task GetHomeProductsQueryHandler_Success()
        {
            //Arrange
            var homeProduct = HomeProduct.NewProduct;

            var products = new List<Product>() { new Product { Id = 1 }, new Product { Id = 2 } };
            var homeProductsDto = new List<GetHomeProductsDto> { new GetHomeProductsDto { Id = 1 }, new GetHomeProductsDto { Id = 2 } };

            var query = new GetHomeProductsQuery(homeProduct);

            productRepository.Setup(x => x.GetHomePageProducts(homeProduct)).Returns(Task.FromResult(products));

            mapper.Setup(x => x.Map<List<GetHomeProductsDto>>(products)).Returns(homeProductsDto);

            var queryHandler = new GetHomeProductsQueryHandler(productRepository.Object, mapper.Object);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(2, action.Count());
            Assert.Equal(1, action.Select(x => x.Id).First());
        }
    }
}
