using Catalog.API.Controllers;
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

namespace Catalog.Test.Controller
{
    public class GetProductsTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<CatalogController>> logger;

        private readonly CatalogController controller;
        private readonly IEnumerable<GetProductsDto> productsDto;

        public GetProductsTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CatalogController>>();
            controller = new CatalogController(mediator.Object, logger.Object);
            productsDto = new List<GetProductsDto> { new GetProductsDto(), new GetProductsDto() };
        }

        [Fact]
        public async Task GetProducts_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(productsDto));

            //Act
            var action = await controller.GetProducts(It.IsAny<FilteringData>()) as OkObjectResult;
            var value = action.Value as List<GetProductsDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(productsDto.Count(), value.Count);
        }
    }
}
