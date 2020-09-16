﻿using Catalog.API.Controllers;
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

        public GetBrandsTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<CatalogController>>();
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
    }
}