﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Production.API.Controllers;
using Production.Application.Mapping;
using Production.Application.Queries;
using Production.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Production.Test.Controller
{
    public class GetProductionQueuesTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly Mock<ILogger<ProductionController>> logger;

        private readonly ProductionController controller;
        private readonly IEnumerable<GetProductionQueuesDto> productionQueuesDto;
        private readonly ProductionQueueFilteringData filteringData;

        public GetProductionQueuesTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<ProductionController>>();
            controller = new ProductionController(mediator.Object, logger.Object);
            productionQueuesDto = new List<GetProductionQueuesDto> { new GetProductionQueuesDto(), new GetProductionQueuesDto() };
            filteringData = new ProductionQueueFilteringData();
        }

        [Fact]
        public async Task GetProductionQueues_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetProductionQueuesQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(productionQueuesDto));

            //Act
            var action = await controller.GetProductionQueues(filteringData) as OkObjectResult;
            var value = action.Value as List<GetProductionQueuesDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(productionQueuesDto.Count(), value.Count);
        }
    }
}
