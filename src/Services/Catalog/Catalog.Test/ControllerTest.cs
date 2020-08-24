using Castle.Core.Logging;
using Catalog.API.Controllers;
using Catalog.Application.Commands;
using Catalog.Application.Mapping;
using Catalog.Application.Queries;
using Catalog.Core.SearchSpecification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Catalog.Core.Models.Enums.HomeProductEnum;

namespace Catalog.Test
{
    public class ControllerTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<CatalogController>> logger;

        public ControllerTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CatalogController>>();
        }

        [Fact]
        public async Task AddProduct_OkResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<AddProductCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.AddProduct(It.IsAny<AddProductCommand>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<AddProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_OkResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.DeleteProduct(It.IsAny<int>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.DeleteProduct(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task GetProducts_OkObjectResult()
        {
            //Arrange
            IEnumerable<GetProductsDto> productsDto = new List<GetProductsDto>
                { new GetProductsDto { Id = 1 }, new GetProductsDto { Id = 2 } };

            mediator.Setup(x => x.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(productsDto));

            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetProducts(It.IsAny<FilteringData>()) as OkObjectResult;
            var value = action.Value as List<GetProductsDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(2, value.Count);
            Assert.Equal(1, value.Select(x => x.Id).First());
        }

        [Fact]
        public async Task GetProduct_OkObjectResult()
        {
            //Arrange
            var productName = "test";
            var productDto = new GetProductDto { ProductName = productName };

            mediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(productDto));

            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetProduct(It.IsAny<int>()) as OkObjectResult;
            var value = action.Value as GetProductDto;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(productName, value.ProductName);
        }

        [Fact]
        public async Task GetProduct_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetProduct(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task GetBrands_OkObjectResult()
        {
            //Arrange
            var brandsDto = new List<GetBrandsDto> { new GetBrandsDto { Id = 1 }, new GetBrandsDto { Id = 2 } };

            mediator.Setup(x => x.Send(It.IsAny<GetBrandsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(brandsDto));

            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetBrands() as OkObjectResult;
            var value = action.Value as List<GetBrandsDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(2, value.Count);
            Assert.Equal(1, value.Select(x => x.Id).First());
        }

        [Fact]
        public async Task GetHomePageProducts_OkObjectResult()
        {
            //Arrange
            var homeProductsDto = new List<GetHomeProductsDto>
                { new GetHomeProductsDto { Id = 1 }, new GetHomeProductsDto { Id = 2 } };

            mediator.Setup(x => x.Send(It.IsAny<GetHomeProductsQuery>(), It.IsAny<CancellationToken>())).
                Returns(Task.FromResult(homeProductsDto));

            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetHomePageProducts(It.IsAny<HomeProduct>()) as OkObjectResult;
            var value = action.Value as List<GetHomeProductsDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(2, value.Count);
            Assert.Equal(1, value.Select(x => x.Id).First());
        }
    }
}
