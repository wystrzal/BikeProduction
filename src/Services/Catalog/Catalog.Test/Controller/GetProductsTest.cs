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

        public GetProductsTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CatalogController>>();
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
    }
}
