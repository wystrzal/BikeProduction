﻿using BikeExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.API.Controllers;
using Warehouse.Application.Commands;
using Xunit;

namespace Warehouse.Test.Controller
{
    public class DeletePartTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<WarehouseController>> logger;
        private readonly WarehouseController controller;

        public DeletePartTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<WarehouseController>>();
            controller = new WarehouseController(mediator.Object, logger.Object);
        }

        [Fact]
        public async Task DeletePart_OkResult()
        {
            //Act
            var action = await controller.DeletePart(It.IsAny<int>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<DeletePartCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }


        [Fact]
        public async Task DeletePart_BadRequestObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<DeletePartCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var action = await controller.DeletePart(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.NotNull(action.Value);
            Assert.Equal(400, action.StatusCode);
            logger.VerifyLogging(LogLevel.Error);
        }
    }
}
