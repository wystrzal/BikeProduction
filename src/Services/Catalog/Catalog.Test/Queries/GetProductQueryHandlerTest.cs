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
        private const string productName = "test";

        private readonly Mock<IMapper> mapper;
        private readonly Mock<IProductRepository> productRepository;

        private readonly GetProductQuery query;
        private readonly GetProductQueryHandler queryHandler;
        private readonly Product product;
        private readonly GetProductDto productDto;

        public GetProductQueryHandlerTest()
        {
            mapper = new Mock<IMapper>();
            productRepository = new Mock<IProductRepository>();
            query = new GetProductQuery(It.IsAny<int>());
            queryHandler = new GetProductQueryHandler(productRepository.Object, mapper.Object);
            product = new Product { ProductName = productName };
            productDto = new GetProductDto { ProductName = productName };
        }

        [Fact]
        public async Task GetProductQueryHandler_Success()
        {
            //Arrange
            productRepository.Setup(x =>
                x.GetByConditionWithIncludeFirst(It.IsAny<Func<Product, bool>>(), It.IsAny<Expression<Func<Product, Brand>>>()))
                .Returns(Task.FromResult(product));

            mapper.Setup(x => x.Map<GetProductDto>(product)).Returns(productDto);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(productName, action.ProductName);
        }
    }
}
