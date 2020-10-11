using BikeSortFilter;
using CustomerOrder.Core.Models;
using CustomerOrder.Core.SearchSpecification;
using CustomerOrder.Core.SearchSpecification.FilterClasses;
using CustomerOrder.Core.SearchSpecification.SortClasses;
using CustomerOrder.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerOrder.Test.Services
{
    public class ServicesTest
    {
        private readonly Mock<ISearchSortFilterService<Order, FilteringData>> sortFilterService;

        public ServicesTest()
        {
            sortFilterService = new Mock<ISearchSortFilterService<Order, FilteringData>>();
        }

        [Fact]
        public async Task GetOrders_Success()
        {
            //Arrange
            var userId = "1";
            var orders = new List<Order> { new Order(), new Order() };
            var filteringData = new FilteringData { UserId = userId };

            sortFilterService.Setup(x => x.Search(It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(orders));

            var service = new SearchOrderService(sortFilterService.Object);

            //Act
            var action = await service.GetOrders(filteringData);

            //Assert
            sortFilterService.Verify(x => x.SetConcreteSort<SortByDate, DateTime>(), Times.Once);
            sortFilterService.Verify(x => x.SetConcreteFilter<FilterByUserId>(filteringData), Times.Once);
            Assert.Equal(orders.Count, action.Count);
        }
    }
}
