using BikeSortFilter;
using CustomerOrder.Core.Models;
using CustomerOrder.Core.SearchSpecification;
using CustomerOrder.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace CustomerOrder.Test.Services
{
    public class ServicesTest
    {
        private const string userId = "1";

        private readonly Mock<ISearchSortFilterService<Order, FilteringData>> sortFilterService;

        private readonly SearchOrderService service;
        private readonly List<Order> orders;
        private readonly FilteringData filteringData;

        public ServicesTest()
        {
            sortFilterService = new Mock<ISearchSortFilterService<Order, FilteringData>>();
            service = new SearchOrderService(sortFilterService.Object);
            orders = new List<Order> { new Order(), new Order() };
            filteringData = new FilteringData { UserId = userId };
        }

        [Fact]
        public async Task GetOrders_Success()
        {
            //Arrange
            sortFilterService.Setup(x => x.Search(It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(orders));

            //Act
            var action = await service.GetOrders(filteringData);

            //Assert
            sortFilterService.Verify(x => x.SetConcreteSort(It.IsAny<Expression<Func<Order, DateTime>>>()), Times.Once);
            sortFilterService.Verify(x => x.SetConcreteFilter(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
            Assert.Equal(orders.Count, action.Count);
        }
    }
}
