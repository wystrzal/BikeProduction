using Catalog.API.Controllers;
using Catalog.Application.Commands;
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

namespace Catalog.Test.Controller
{
    public class AddProductTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<CatalogController>> logger;

        public AddProductTest()
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
            mediator.Verify(x => x.Send(It.IsAny<AddProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }
    }
}
