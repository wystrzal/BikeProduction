using AutoMapper;
using CustomerOrder.Application.Mapping;
using CustomerOrder.Application.Queries;
using CustomerOrder.Application.Queries.Handlers;
using CustomerOrder.Core.Exceptions;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using CustomerOrder.Core.SearchSpecification;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerOrder.Test
{
    public class QueriesTest
    {
        private readonly Mock<IOrderRepository> orderRepository;
        private readonly Mock<IMapper> mapper;
        private readonly Mock<ISearchOrderService> searchOrderService;

        public QueriesTest()
        {
            orderRepository = new Mock<IOrderRepository>();
            mapper = new Mock<IMapper>();
            searchOrderService = new Mock<ISearchOrderService>();
        }

        [Fact]
        public async Task GetOrderQueryHandler_ThrowOrderNotFoundException()
        {
            //Arrange
            orderRepository.Setup(x => x.GetByConditionWithIncludeFirst(It.IsAny<Func<Order, bool>>(),
                It.IsAny<Expression<Func<Order, ICollection<OrderItem>>>>()))
                .Returns(Task.FromResult((Order)null));

            var queryHandler = new GetOrderQueryHandler(orderRepository.Object, mapper.Object);

            //Assert
            await Assert.ThrowsAsync<OrderNotFoundException>(() => 
                queryHandler.Handle(It.IsAny<GetOrderQuery>(), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task GetOrderQueryHandler_Success()
        {
            //Arrange
            var id = 1;
            var order = new Order { OrderId = id };
            var orderDto = new GetOrderDto { OrderId = id };

            orderRepository.Setup(x => x.GetByConditionWithIncludeFirst(It.IsAny<Func<Order, bool>>(),
                It.IsAny<Expression<Func<Order, ICollection<OrderItem>>>>()))
                .Returns(Task.FromResult(order));

            mapper.Setup(x => x.Map<GetOrderDto>(order)).Returns(orderDto);

            var queryHandler = new GetOrderQueryHandler(orderRepository.Object, mapper.Object);

            //Act
            var action = await queryHandler.Handle(It.IsAny<GetOrderQuery>(), It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(id, action.OrderId);
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
