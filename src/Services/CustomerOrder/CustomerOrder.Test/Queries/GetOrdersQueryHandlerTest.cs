using AutoMapper;
using CustomerOrder.Application.Mapping;
using CustomerOrder.Application.Queries;
using CustomerOrder.Application.Queries.Handlers;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using CustomerOrder.Core.SearchSpecification;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerOrder.Test.Queries
{
    public class GetOrdersQueryHandlerTest
    {
        private readonly Mock<IMapper> mapper;
        private readonly Mock<ISearchOrderService> searchOrderService;

        public GetOrdersQueryHandlerTest()
        {
            mapper = new Mock<IMapper>();
            searchOrderService = new Mock<ISearchOrderService>();
        }

        [Fact]
        public async Task GetOrdersQueryHandler_Success()
        {
            //Arrange
            var filteringData = new FilteringData();
            var query = new GetOrdersQuery(filteringData);
            var orders = new List<Order> { new Order { OrderId = 1 }, new Order { OrderId = 2 } };
            var ordersDto = new List<GetOrdersDto> { new GetOrdersDto { OrderId = 1 }, new GetOrdersDto { OrderId = 2 } };

            searchOrderService.Setup(x => x.GetOrders(filteringData)).Returns(Task.FromResult(orders));

            mapper.Setup(x => x.Map<List<GetOrdersDto>>(orders)).Returns(ordersDto);

            var queryHandler = new GetOrdersQueryHandler(searchOrderService.Object, mapper.Object);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(2, action.Count());
            Assert.Equal(1, action.Select(x => x.OrderId).First());
        }
    }
}
