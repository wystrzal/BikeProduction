using AutoMapper;
using Catalog.Application.Mapping;
using Catalog.Application.Queries;
using Catalog.Application.Queries.Handlers;
using Catalog.Core.Exceptions;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Core.SearchSpecification;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test
{
    public class QueriesTest
    {
        private readonly Mock<IMapper> mapper;
        private readonly Mock<ISearchProductService> searchProductService;
        private readonly Mock<IProductRepository> productRepository;

        public QueriesTest()
        {
            mapper = new Mock<IMapper>();
            searchProductService = new Mock<ISearchProductService>();
            productRepository = new Mock<IProductRepository>();
        }

        [Fact]
        public async Task GetProductsQueryHandler_Success()
        {
            //Arrange
            var query = new GetProductsQuery(new FilteringData());

            var products = new List<Product> { new Product { Id = 1 }, new Product { Id = 2 } };
            var productsDto = new List<GetProductsDto> { new GetProductsDto { Id = 1 }, new GetProductsDto { Id = 2 } };

            searchProductService.Setup(x => x.GetProducts(It.IsAny<FilteringData>())).Returns(Task.FromResult(products));

            mapper.Setup(x => x.Map<List<GetProductsDto>>(products)).Returns(productsDto);

            var queryHandler = new GetProductsQueryHandler(mapper.Object, searchProductService.Object);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(2, action.Count());
            Assert.Equal(1, action.Select(x => x.Id).First());
        }

        [Fact]
        public async Task GetProductQueryHandler_Success()
        {
            //Arrange
            int productId = 1;
            string productName = "test";

            var product = new Product { Id = productId, ProductName = productName };
            var productDto = new GetProductDto { ProductName = productName };

            var query = new GetProductQuery(productId);

            productRepository.Setup(x =>
                x.GetByConditionWithIncludeFirst(It.IsAny<Func<Product, bool>>(), It.IsAny<Expression<Func<Product, Brand>>>()))
                .Returns(Task.FromResult(product));

            mapper.Setup(x => x.Map<GetProductDto>(product)).Returns(productDto);

            var queryHandler = new GetProductQueryHandler(productRepository.Object, mapper.Object);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(productName, action.ProductName);
        }

        [Fact]
        public async Task GetProductQueryHandler_ProductNotFoundException()
        {
            //Arrange
            int productId = 1;

            var query = new GetProductQuery(productId);

            productRepository.Setup(x =>
                x.GetByConditionWithIncludeFirst(It.IsAny<Func<Product, bool>>(), It.IsAny<Expression<Func<Product, Brand>>>()))
                .Returns(Task.FromResult((Product)null));

            var queryHandler = new GetProductQueryHandler(productRepository.Object, mapper.Object);

            //Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => queryHandler.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
