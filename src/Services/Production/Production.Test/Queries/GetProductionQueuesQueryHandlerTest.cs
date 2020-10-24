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
        private readonly Mock<ISearchProductionQueuesService> searchProductionQueuesService;
        private readonly Mock<IMapper> mapper;

        private readonly ProductionQueueFilteringData filteringData;
        private readonly GetProductionQueuesQuery query;
        private readonly GetProductionQueuesQueryHandler queryHandler;
        private readonly List<ProductionQueue> productionQueues;
        private readonly List<GetProductionQueuesDto> productionQueuesDto;

        public GetProductionQueuesQueryHandlerTest()
        {
            searchProductionQueuesService = new Mock<ISearchProductionQueuesService>();
            mapper = new Mock<IMapper>();
            query = new GetProductionQueuesQuery(filteringData);
            queryHandler = new GetProductionQueuesQueryHandler(searchProductionQueuesService.Object, mapper.Object);
            productionQueues = new List<ProductionQueue> { new ProductionQueue(), new ProductionQueue() };
            productionQueuesDto = new List<GetProductionQueuesDto> { new GetProductionQueuesDto(), new GetProductionQueuesDto() };
        }

        [Fact]
        public async Task GetProductionQueuesQueryHandler_Success()
        {
            //Arrange
            searchProductionQueuesService.Setup(x => x.SearchProductionQueues(filteringData)).Returns(Task.FromResult(productionQueues));
            mapper.Setup(x => x.Map<IEnumerable<GetProductionQueuesDto>>(productionQueues)).Returns(productionQueuesDto);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(productionQueuesDto.Count, action.Count());
        }
    }
}
