using Catalog.API.Controllers;
using Catalog.Application.Mapping;
using Catalog.Application.Queries;
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

namespace Catalog.Test.Controller
{
    public class GetHomePageProductsTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<CatalogController>> logger;

        public GetHomePageProductsTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CatalogController>>();
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
