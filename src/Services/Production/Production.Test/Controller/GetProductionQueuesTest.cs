using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Production.API.Controllers;
using Production.Application.Mapping;
using Production.Application.Queries;
using System;
using System.Collections.Generic;
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

        public GetProductionQueuesTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<ProductionQueueController>>();
        }

        [Fact]
        public async Task GetProductionQueues_OkObjectResult()
        {
            //Arrange
            IEnumerable<GetProductionQueuesDto> productionQueuesDto
                = new List<GetProductionQueuesDto> { new GetProductionQueuesDto(), new GetProductionQueuesDto() };

            mediator.Setup(x => x.Send(It.IsAny<GetProductionQueuesQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(productionQueuesDto));

            var controller = new ProductionQueueController(mediator.Object, logger.Object);

            //Act
            var action = await controller.GetProductionQueues() as OkObjectResult;
            var value = action.Value as List<GetProductionQueuesDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(2, value.Count);
        }
    }
}
