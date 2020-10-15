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

        private readonly CatalogController controller;
        private readonly List<GetHomeProductsDto> homeProductsDto;

        public GetHomePageProductsTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CatalogController>>();
            controller = new CatalogController(mediator.Object, logger.Object);
            homeProductsDto = new List<GetHomeProductsDto> { new GetHomeProductsDto(), new GetHomeProductsDto() };
        }

        [Fact]
        public async Task GetHomePageProducts_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetHomeProductsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(homeProductsDto));

            //Act
            var action = await controller.GetHomePageProducts(It.IsAny<HomeProduct>()) as OkObjectResult;
            var value = action.Value as List<GetHomeProductsDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(homeProductsDto.Count, value.Count);
        }
    }
}
