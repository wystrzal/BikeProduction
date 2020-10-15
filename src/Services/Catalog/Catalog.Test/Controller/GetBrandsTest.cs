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

namespace Catalog.Test.Controller
{
    public class GetBrandsTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<CatalogController>> logger;

        private readonly CatalogController controller;
        private readonly List<GetBrandsDto> brandsDto;

        public GetBrandsTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CatalogController>>();
            controller = new CatalogController(mediator.Object, logger.Object);
            brandsDto = new List<GetBrandsDto> { new GetBrandsDto(), new GetBrandsDto() };
        }

        [Fact]
        public async Task GetBrands_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetBrandsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(brandsDto));       

            //Act
            var action = await controller.GetBrands() as OkObjectResult;
            var value = action.Value as List<GetBrandsDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(brandsDto.Count, value.Count);
        }
    }
}
