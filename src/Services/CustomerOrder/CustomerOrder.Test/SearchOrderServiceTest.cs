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

namespace CustomerOrder.Test
{
    public class ServicesTest
    {
        private readonly Mock<ISortFilterService<Order, FilteringData>> sortFilterService;

        public ServicesTest()
        {
            sortFilterService = new Mock<ISortFilterService<Order, FilteringData>>();
        }

        [Fact]
        public async Task GetOrders_Success()
        {
            //Arrange
            var userId = "1";
            var orders = new List<Order> { new Order { OrderId = 1, UserId = userId }, new Order { OrderId = 2, UserId = userId } };
            var filteringData = new FilteringData { UserId = userId };

            sortFilterService.Setup(x => x.SetConcreteSort<SortByDate, DateTime>()).Verifiable();
            sortFilterService.Setup(x => x.SetConcreteFilter<FilterByUserId>(filteringData)).Verifiable();

            sortFilterService.Setup(x => x.Search(It.IsAny<bool>(), 0, 0)).Returns(Task.FromResult(orders));

            var service = new SearchOrderService(sortFilterService.Object);

            //Act
            var action = await service.GetOrders(filteringData);

            //Assert
            sortFilterService.Verify(x => x.SetConcreteSort<SortByDate, DateTime>(), Times.Once);
            sortFilterService.Verify(x => x.SetConcreteFilter<FilterByUserId>(filteringData), Times.Once);
            Assert.Equal(2, action.Count);
            Assert.Equal(1, action.Select(x => x.OrderId).First());
        }
    }
}
