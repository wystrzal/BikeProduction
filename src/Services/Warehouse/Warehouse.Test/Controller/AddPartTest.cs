﻿using Castle.Core.Logging;
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
    public class AddPartTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<WarehouseController>> logger;

        public AddPartTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<WarehouseController>>();
        }

        [Fact]
        public async Task AddPart_OkResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<AddPartCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            var controller = new WarehouseController(mediator.Object, logger.Object);

            //Act
            var action = await controller.AddPart(It.IsAny<AddPartCommand>()) as OkResult;

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<AddPartCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(200, action.StatusCode);
        }
    }
}
