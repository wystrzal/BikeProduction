using AutoMapper;
using Catalog.Application.Mapping;
using Catalog.Application.Queries;
using Catalog.Application.Queries.Handlers;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test.Queries
{
    public class GetProductQueryHandlerTest
    {
        private readonly Mock<IMapper> mapper;
        private readonly Mock<IProductRepository> productRepository;

        public GetProductQueryHandlerTest()
        {
            mapper = new Mock<IMapper>();
            productRepository = new Mock<IProductRepository>();
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
    }
}
