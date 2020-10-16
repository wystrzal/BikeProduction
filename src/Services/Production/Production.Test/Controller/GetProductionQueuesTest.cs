using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Production.API.Controllers;
using Production.Application.Mapping;
using Production.Application.Queries;
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
        private readonly Mock<ILogger<ProductionQueueController>> logger;

        private readonly ProductionQueueController controller;
        private readonly IEnumerable<GetProductionQueuesDto> productionQueuesDto;

        public GetProductionQueuesTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<ProductionQueueController>>();
            controller = new ProductionQueueController(mediator.Object, logger.Object);
            productionQueuesDto = new List<GetProductionQueuesDto> { new GetProductionQueuesDto(), new GetProductionQueuesDto() };
        }

        [Fact]
        public async Task GetProductionQueues_OkObjectResult()
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetProductionQueuesQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(productionQueuesDto));

            //Act
            var action = await controller.GetProductionQueues() as OkObjectResult;
            var value = action.Value as List<GetProductionQueuesDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(productionQueuesDto.Count(), value.Count);
        }
    }
}
