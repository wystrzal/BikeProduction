using BikeSortFilter;
using Moq;
using Production.Core.Models;
using Production.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using static Production.Core.Models.Enums.ProductionStatusEnum;

namespace Production.Test.Services
{
    public class SearchProductionQueuesServiceTest
    {
        private readonly Mock<ISearchSortFilterService<ProductionQueue, ProductionQueueFilteringData>> sortFilterService;

        private readonly ProductionQueueFilteringData filteringData;
        private readonly SearchProductionQueuesService service;
        private readonly List<ProductionQueue> productionQueues;

        public SearchProductionQueuesServiceTest()
        {
            sortFilterService = new Mock<ISearchSortFilterService<ProductionQueue, ProductionQueueFilteringData>>();
            filteringData = new ProductionQueueFilteringData { ProductionStatus = ProductionStatus.Waiting };
            service = new SearchProductionQueuesService(sortFilterService.Object);
            productionQueues = new List<ProductionQueue> { new ProductionQueue(), new ProductionQueue() };
        }

        [Fact]
        public async Task SearchProductionQueuesService_Success()
        {
            //Arrange
            sortFilterService.Setup(x => x.Search(It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(productionQueues));

            //Act
            var action = await service.SearchProductionQueues(filteringData);

            //Assert
            Assert.Equal(productionQueues.Count, action.Count);
            sortFilterService.Verify(x => x.SetConcreteSort(It.IsAny<Expression<Func<ProductionQueue, ProductionStatus>>>()), Times.Once);
            sortFilterService.Verify(x => x.SetConcreteFilter(It.IsAny<Expression<Func<ProductionQueue, bool>>>()), Times.Once);
        }
    }
}
