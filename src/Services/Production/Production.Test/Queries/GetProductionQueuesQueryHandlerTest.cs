using AutoMapper;
using Moq;
using Production.Application.Mapping;
using Production.Application.Queries;
using Production.Application.Queries.Handlers;
using Production.Core.Interfaces;
using Production.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Production.Test.Queries
{
    public class GetProductionQueuesQueryHandlerTest
    {
        private readonly Mock<IProductionQueueRepo> productionQueueRepo;
        private readonly Mock<IMapper> mapper;

        public GetProductionQueuesQueryHandlerTest()
        {
            productionQueueRepo = new Mock<IProductionQueueRepo>();
            mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetProductionQueuesQueryHandler_Success()
        {
            //Arrange
            var productionQueues = new List<ProductionQueue> { new ProductionQueue(), new ProductionQueue() };
            var productionQueuesDto = new List<GetProductionQueuesDto> { new GetProductionQueuesDto(), new GetProductionQueuesDto() };
            var query = new GetProductionQueuesQuery();

            productionQueueRepo.Setup(x => x.GetAll()).Returns(Task.FromResult(productionQueues));
            mapper.Setup(x => x.Map<IEnumerable<GetProductionQueuesDto>>(productionQueues)).Returns(productionQueuesDto);

            var queryHandler = new GetProductionQueuesQueryHandler(productionQueueRepo.Object, mapper.Object);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(productionQueuesDto.Count, action.Count());
        }
    }
}
