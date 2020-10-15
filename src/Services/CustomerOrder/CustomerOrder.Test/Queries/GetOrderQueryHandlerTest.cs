using AutoMapper;
using CustomerOrder.Application.Mapping;
using CustomerOrder.Application.Queries;
using CustomerOrder.Application.Queries.Handlers;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerOrder.Test.Queries
{
    public class GetOrderQueryHandlerTest
    {
        private const int orderId = 1;

        private readonly Mock<IOrderRepository> orderRepository;
        private readonly Mock<IMapper> mapper;

        private readonly GetOrderQuery query;
        private readonly GetOrderQueryHandler queryHandler;
        private readonly Order order;
        private readonly GetOrderDto orderDto;

        public GetOrderQueryHandlerTest()
        {
            orderRepository = new Mock<IOrderRepository>();
            mapper = new Mock<IMapper>();
            query = new GetOrderQuery(orderId);
            queryHandler = new GetOrderQueryHandler(orderRepository.Object, mapper.Object);
            order = new Order { OrderId = orderId };
            orderDto = new GetOrderDto { OrderId = orderId };
        }

        [Fact]
        public async Task GetOrderQueryHandler_Success()
        {
            //Arrange
            orderRepository.Setup(x => x.GetByConditionWithIncludeFirst(It.IsAny<Func<Order, bool>>(),
                It.IsAny<Expression<Func<Order, ICollection<OrderItem>>>>())).Returns(Task.FromResult(order));

            mapper.Setup(x => x.Map<GetOrderDto>(order)).Returns(orderDto);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(orderId, action.OrderId);
        }
    }
}
