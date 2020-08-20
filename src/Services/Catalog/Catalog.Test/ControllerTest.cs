using Castle.Core.Logging;
using Catalog.API.Controllers;
using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Core.SearchSpecification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
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
            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.AddProduct(It.IsAny<AddProductCommand>()) as OkResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_OkResult()
        {
            //Arrange
            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.DeleteProduct(It.IsAny<int>()) as OkResult;

            //Assert
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
            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetProducts(It.IsAny<FilteringData>()) as OkObjectResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task GetProduct_OkObjectResult()
        {
            //Arrange
            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetProduct(It.IsAny<int>()) as OkObjectResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
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
            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetBrands() as OkObjectResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task GetHomePageProducts_OkObjectResult()
        {
            //Arrange
            var controller = new CatalogController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetHomePageProducts(It.IsAny<HomeProduct>()) as OkObjectResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
        }
    }
}
